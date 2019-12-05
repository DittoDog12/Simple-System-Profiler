using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Simple_System_Profiler.Interfaces;

namespace Simple_System_Profiler.Hardware
{
    class LAN : AComponentProfiler
    {
        #region Variables
        private int _CurrentDevice = 0; // Create and Initialize a Network Device Counter
        #endregion

        /// <summary>
        /// Network Device profiler
        /// </summary>
        public LAN()
        {
            // Initialise searcher to scan for Network Adapter information
            _Searcher = new ManagementObjectSearcher("select * from Win32_NetworkAdapter");
            _Name = "LAN";
        }

        #region Methods
        /// <summary>
        /// Main Profiler Rountine
        /// </summary>
        /// <param name="pSpecs"> Master dictionary of Specifications </param>
        /// <returns> Updated dictionary of Specifications </returns>
        public override IDictionary<string, string> GetDetails(IDictionary<string, string> pSpecs)
        {
            // For each object in the searcher
            foreach (ManagementObject obj in _Searcher.Get())
            {
                string mGUID = "";
                // First Check the current adapter is a real one and not a virtual WAN or VPN
                // DO this as two steps incase PhysicalAdapter property is suddenly missing
                if (obj["PhysicalAdapter"] != null)
                {
                    if (obj["PhysicalAdapter"].ToString() == "True")
                    {
                        // Locate Name
                        if (obj["Name"] != null)
                            pSpecs.Add("Network_Device_" + _CurrentDevice.ToString(), obj["Name"].ToString());

                        // Locate Speed
                        if (obj["Speed"] != null)
                            pSpecs.Add("Network_Device_" + _CurrentDevice.ToString() + "_Speed", Convertbits(obj["Speed"].ToString()));
                        
                        // Locate IP Address
                        // First get the GUID of the connection
                        if (obj["GUID"] != null)
                            mGUID = obj["GUID"].ToString();
                        
                        // Next Create StringArray 1 long to hold IP Address, set first entry to "DHCP"
                        string[] mRegKey = new string[1]{ "DHCP" };
                        // Check EnableDHCP Key, if 0 (Disabled), overwrite mRegKey[0] with the IP Address found in registry via object casting
                        // GetValue() returns object, cast to string[]
                        if (Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\services\\Tcpip\\Parameters\\Interfaces\\" + mGUID, "EnableDHCP", null).ToString() == "0")
                            mRegKey = Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\services\\Tcpip\\Parameters\\Interfaces\\" + mGUID, "IPAddress", "DHCP") as string[];
           
                        // Save mRegKey to the Specs Dictionary, only return the first IP address in the Array
                        pSpecs.Add("Network_Device_" + _CurrentDevice.ToString() + "_Current_IP", mRegKey[0]);
                    }
                    // Increment current device
                    _CurrentDevice++;
                }
            }
            return pSpecs;
        }
        /// <summary>
        /// Converts Bits into Megabits
        /// </summary>
        /// <param name="pBits"> Bit value to convert </param>
        /// <returns> Equivialent Megabits </returns>
        private string Convertbits(string pBits)
        {
            // Declare Variables to hold Kilo, MegaByte variables
            int mBits = 0;
            int mMBits = 0;

            // Try convert the string to an int
            // If successful then begin unit conversion
            if (Int32.TryParse(pBits, out mBits))
            {
                mMBits = mBits / 1000000;
            }

            // Finally Return converted number as string only if not 0
            if (mMBits > 0)
                return mMBits.ToString() + "Mbits";
            else
                return null;
        }
        #endregion
    }
}

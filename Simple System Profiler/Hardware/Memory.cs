using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Simple_System_Profiler.Interfaces;


namespace Simple_System_Profiler.Hardware
{
    /// <summary>
    /// Gets System memory information
    /// </summary>
    class Memory : AComponentProfiler
    {
        #region Variables
        // All for SPD 
        //public IDictionary<int, string> _ModuleType { get; private set; }
        //public IDictionary<int, string> _ModuleCapacity { get; private set; }
        //private int _CurrModule;
        //private ManagementObjectSearcher _Searcher2;

        #endregion
        /// <summary>
        /// Memory profiler
        /// Currently gets Total Visible RAM
        /// Working on SPD information
        /// </summary>
        public Memory()
        {
            // Initialise searcher to scan for OS information
            _Searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            // Initialise searcher to scan for Memory Information
            //_Searcher2 = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");

            // Create Array for storing each Module Capacity and type
            //_ModuleType = new Dictionary<int, string>();
            //_ModuleCapacity = new Dictionary<int, string>();
            //_CurrModule = 0;
            _Name = "Memory";
        }

        #region Methods
        /// <summary>
        /// Main Profiler Rountine
        /// </summary>
        /// <param name="pSpecs"> Master dictionary of Specifications </param>
        /// <returns> Updated dictionary of Specifications </returns>
        public override IDictionary<string, string> GetDetails(IDictionary<string, string> pSpecs)
        {
            // For each object in the searcher, save Total Visible Memory
            foreach (ManagementObject obj in _Searcher.Get())
                // Get total memory Size
                if (obj["TotalVisibleMemorySize"] != null)
                    pSpecs.Add("Total_Memory", ConvertBytes(obj["TotalVisibleMemorySize"].ToString()));

            #region SPD - Not Working/Required
            //// For each object in the second seracher, save the memory module configuration for each detected module
            //foreach (ManagementObject obj2 in _Searcher2.Get())
            //{
            //    string mMemoryType = "";
            //    string mMemoryCap = "";

            //    if (obj2["MemoryType"] != null)
            //    {
            //        mMemoryType += obj2["MemoryType"].ToString();
            //    }
            //    if (obj2["Speed"] != null)
            //    {
            //        mMemoryType += " - " + obj2["Speed"].ToString();
            //    }
            //    if (obj2["Capacity"] != null)
            //    {
            //        mMemoryCap = ConvertBytes(obj2["Capacity"].ToString());
            //    }

            //    _ModuleType.Add(_CurrModule, mMemoryType);
            //    _ModuleCapacity.Add(_CurrModule, mMemoryCap);

            //    _CurrModule++;

            //    if (obj2["DeviceLocator"] != null)
            //    {
            //        Console.WriteLine(obj2["DeviceLocator"].ToString());
            //    }
            //}

            //foreach (ManagementObject obj3 in _Searcher3.Get())
            //{

            //}
            #endregion

            return pSpecs;
        }
        /// <summary>
        /// Kilobyte to Megabyte Converter
        /// </summary>
        /// <param name="pKB"> Kilobyte value to convert </param>
        /// <returns> Equivilent Megabytes </returns>
        private string ConvertBytes(string pKB)
        {
            // Declare Variables to hold Kilo, MegaByte variables
            int mKB = 0;
            int mMB = 0;

            // Try convert the string to an int
            // If successful then begin unit conversion
            if (Int32.TryParse(pKB, out mKB))
                mMB = mKB / 1024;
            

            // Finally Return converted number as string only if not 0
            if (mMB > 0)
                return mMB.ToString() + "MB";
            else
                return null;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_System_Profiler.Interfaces;
using System.Management;

namespace Simple_System_Profiler.Hardware
{
    class HDD : AComponentProfiler
    {
        ManagementObjectSearcher _Searcher2; // Second Searcher for Partition Information
        private int _CurrentDevice = 0; // Create and Initialize a Storage Device Counter

        /// <summary>
        /// Hard Disk Drive Profiler
        /// Returns Disk Drive Model Names
        /// And Parition Size/Free Space
        /// </summary>
        public HDD()
        {
            // Initialise searchersto scan for Disk Drive and Disk Partition information
            _Searcher = new ManagementObjectSearcher("select * from Win32_DiskDrive");
            _Searcher2 = new ManagementObjectSearcher("select * from Win32_DiskPartition");
            _Name = "HDD";
        }
        #region Methods
        /// <summary>
        /// Main Profiler Rountine
        /// </summary>
        /// <param name="pSpecs"> Master dictionary of Specifications </param>
        /// <returns> Updated dictionary of Specifications </returns>
        public override IDictionary<string, string> GetDetails(IDictionary<string, string> pSpecs)
        {
            // For each object in the searcher, find the Hard Drive name and Partition Size
            foreach (ManagementObject obj in _Searcher.Get())
            {
                // Locate HDD Model
                if (obj["Model"] != null)pSpecs.Add("Disk_" + _CurrentDevice.ToString() + "_Name", obj["Model"].ToString());
                
                //// Locate Partition Count
                //if (obj["Partitions"] != null)
                //{
                //    pSpecs.Add("Disk " + _CurrentDevice.ToString() + " Partitions", obj["Partitions"].ToString());
                //}
                // Locate Size
                if (obj["Size"] != null)
                    pSpecs.Add("Disk_" + _CurrentDevice.ToString() + "_Size", ConvertBytes(obj["Size"].ToString()));

                _CurrentDevice++;
            }           
            //// For each object in the searcher, find the partition size
            //foreach (ManagementObject obj in _Searcher2.Get())
            //{
            //    // Locate Partition sizes
            //    if (obj["Size"] != null)
            //    {
            //        pSpecs.Add(obj["Name"].ToString() + " Size", ConvertBytes(obj["Size"].ToString()));
            //    }
            //}
            return pSpecs;
        }
        /// <summary>
        /// Byte to Gigabyte Converter
        /// </summary>
        /// <param name="pKB"> Kilobyte value to convert </param>
        /// <returns> Equivilent Gigabytes </returns>
        private string ConvertBytes(string pB)
        {
            // Declare Variables to hold Byte, Kilo, MegaByte variables
            long mB = 0;
            long mKB = 0;
            long mMB = 0;
            long mGB = 0;

            // Try convert the string to an int
            // If successful then begin unit conversion
            if (Int64.TryParse(pB, out mB))
            {
                mKB = mB / 1024;
                mMB = mKB / 1024;
                mGB = mMB / 1024;
            }

            // Finally Return converted number as string only if not 0
            if (mGB > 0)
                return mGB.ToString() + "GB";
            else if (mMB > 0)
                return mMB.ToString() + "MB";
            else
                return null;
        }
        #endregion
    }
}

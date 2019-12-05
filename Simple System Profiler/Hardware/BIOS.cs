using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.IO;
using Simple_System_Profiler.Interfaces;

namespace Simple_System_Profiler.Hardware
{
    class BIOS : AComponentProfiler
    {
        /// <summary>
        /// BIOS Profiler
        /// Gets the System Manufacturer
        /// </summary>
        public BIOS()
        {
            // Initialise searcher to scan for BIOS information
            _Searcher = new ManagementObjectSearcher("select * from Win32_BIOS");
            _Name = "BIOS";
        }

        #region Methods
        /// <summary>
        /// Main Profiler Rountine
        /// </summary>
        /// <param name="pSpecs"> Master dictionary of Specifications </param>
        /// <returns> Updated dictionary of Specifications </returns>
        public override IDictionary<string, string> GetDetails(IDictionary<string, string> pSpecs)
        {

            // For each object in the searcher, find Manufacturer name
            foreach (ManagementObject obj in _Searcher.Get())
                // Locate Manufacturer
                if (obj["Manufacturer"] != null)
                    pSpecs.Add("System_Manufacturer", obj["Manufacturer"].ToString());
                       
            return pSpecs;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Simple_System_Profiler.Interfaces;

namespace Simple_System_Profiler.Hardware
{
    class CPU : AComponentProfiler
    {
        /// <summary>
        /// CPU Profiler
        /// Gets the exact name detected
        /// </summary>
        public CPU()
        {
            // Initialise searcher to scan for CPU information
            _Searcher = new ManagementObjectSearcher("select * from Win32_Processor");
            _Name = "CPU";
        }

        #region Methods
        /// <summary>
        /// Main Profiler Rountine
        /// </summary>
        /// <param name="pSpecs"> Master dictionary of Specifications </param>
        /// <returns> Updated dictionary of Specifications </returns>
        public override IDictionary<string, string> GetDetails(IDictionary<string, string> pSpecs)
        {
            // For each object in the searcher, save CPU name
            foreach (ManagementObject obj in _Searcher.Get())
                if (obj["Name"] != null) pSpecs.Add("CPU_Name", obj["Name"].ToString());


            return pSpecs;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Simple_System_Profiler.Interfaces;

namespace Simple_System_Profiler.Hardware
{
    class GPU : AComponentProfiler
    {
        public GPU()
        {
            // Initialise searcher to scan for GPU information
            _Searcher = new ManagementObjectSearcher("select * from Win32_VideoController");
            _Name = "GPU";
        }
        #region Methods
        /// <summary>
        /// Main Profiler Rountine
        /// </summary>
        /// <param name="pSpecs"> Master dictionary of Specifications </param>
        /// <returns> Updated dictionary of Specifications </returns>
        public override IDictionary<string, string> GetDetails(IDictionary<string, string> pSpecs)
        {
            // For each object in the searcher, save GPU Namew
            foreach (ManagementObject obj in _Searcher.Get())
                if (obj["Caption"] != null)
                    pSpecs.Add("GPU_Name", obj["Caption"].ToString());
                
            
            return pSpecs;
        }
        #endregion
    }
}

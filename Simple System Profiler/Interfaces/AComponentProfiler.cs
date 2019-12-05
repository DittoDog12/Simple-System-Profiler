using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace Simple_System_Profiler.Interfaces
{
    abstract class AComponentProfiler : IComponentProfiler
    {
        protected string _Name; // Component Name

        /// <summary>
        /// Name of the component
        /// </summary>
        public string Name
        {
            get { return _Name; }
        }
        protected ManagementObjectSearcher _Searcher; // Object Searcher for all Profilers
        /// <summary>
        /// Main Profiler Rountine
        /// </summary>
        /// <param name="pSpecs"> Master dictionary of Specifications </param>
        /// <returns> Updated dictionary of Specifications </returns>
        public abstract IDictionary<string, string> GetDetails(IDictionary<string, string> pSpecs);
    }
}

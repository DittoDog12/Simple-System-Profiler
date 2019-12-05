using System;
using System.Collections.Generic;
using System.Text;

namespace Simple_System_Profiler.Interfaces
{
    interface IComponentProfiler
    {

        /// <summary>
        /// Name of the component
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Main Profiler Rountine
        /// </summary>
        /// <param name="pSpecs"> Master dictionary of Specifications </param>
        /// <returns> Updated dictionary of Specifications </returns>
        IDictionary<string, string> GetDetails(IDictionary<string, string> pSpecs);
    }
}

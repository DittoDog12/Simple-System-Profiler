using System;
using System.Collections.Generic;
using System.Text;

namespace Simple_System_Profiler.Interfaces
{
    /// <summary>
    /// Interface for all Output Components
    /// </summary>
    interface IWriter
    {
        /// <summary>
        /// Main File write routine
        /// </summary>
        /// <param name="pSpecs"> Dictionary of all recorded Specs </param>
        /// <param name="pPath"> Output Path </param>
        void Write(IDictionary<string, string> pSpecs, string pPath);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Simple_System_Profiler.Interfaces
{

    /// <summary>
    /// Abstract class for all Outputters
    /// </summary>
    public abstract class AWriter : IWriter
    {
        /// Output Path
        protected string _Path;

        /// <summary>
        /// Main File write routine
        /// </summary>
        /// <param name="pSpecs"> Dictionary of all recorded Specs </param>
        public abstract void Write(IDictionary<String, String> pSpecs, string pPath);
    }

}

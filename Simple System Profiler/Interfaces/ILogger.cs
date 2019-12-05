using System;
using System.Collections.Generic;
using System.Text;

namespace Simple_System_Profiler.Interfaces
{
    /// <summary>
    /// Interface for logger subsystem
    /// </summary>
    interface ILogger
    {
        /// <summary>
        /// Log Counter
        /// </summary>
        int LogCount { get; }

        /// <summary>
        /// Write a progress report to the log
        /// </summary>
        /// <param name="pEntry"> Report to write </param>
        void WriteProgress(string pEntry);

        /// <summary>
        /// Write an error report to the log
        /// Also print to screen
        /// </summary>
        /// <param name="pEntry"> Report to write </param>
        /// <param name="pDetails"> Exception details to record </param>
        void WriteComponentFault(string pEntry, string pDetails);

        /// <summary>
        /// For the Kernel to save the arguments to the logfile
        /// </summary>
        /// <param name="pModes"> list of arguments </param>
        void SetModes(string[] pModes);
    }
}

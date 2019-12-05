using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_System_Profiler.Interfaces;
using Microsoft.Win32;

namespace Simple_System_Profiler.Software
{
    class Office : AComponentProfiler
    {
        /// <summary>
        /// CPU Profiler
        /// Gets the exact name detected
        /// </summary>
        public Office()
        {
            // Doesnt use the System.Management searcher
            // Just a regquery
            _Name = "Office";
        }

        #region Methods
        /// <summary>
        /// Main Profiler Rountine
        /// </summary>
        /// <param name="pSpecs"> Master dictionary of Specifications </param>
        /// <returns> Updated dictionary of Specifications </returns>
        public override IDictionary<string, string> GetDetails(IDictionary<string, string> pSpecs)
        {
            string mOfficeVer = "";
            // Read the Default key in the location below
            string mRegkey = Registry.GetValue("HKEY_CLASSES_ROOT\\Word.Application\\CurVer", "", "No Office").ToString();
            // Identify the Office Name by the version number
            switch (mRegkey)
            {
                case "Word.Application.16":
                    mOfficeVer = "2016/2019/365";
                    break;
                case "Word.Application.15":
                    mOfficeVer = "2013";
                    break;
                case "Word.Application.14":
                    mOfficeVer = "2010";
                    break;
                case "Word.Application.12":
                    mOfficeVer = "2007";
                    break;
                case "No Office":
                    mOfficeVer = "No Office Detected";
                    break;
                default:
                    mOfficeVer = "Office not recognised, perhaps older than 2007?";
                    break;
            }
            // Save the identified name
            pSpecs.Add("Office_Version", mOfficeVer);
            return pSpecs;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Microsoft.Win32;
using Simple_System_Profiler.Interfaces;

namespace Simple_System_Profiler.Software
{
    /// <summary>
    /// Get's the OS information
    /// </summary>
    class OS : AComponentProfiler
    {
        /// <summary>
        /// OS Profiler
        /// Gets OS Name, Architecture and build time
        /// Windows 10 Also locates build number
        /// </summary>
        public OS()
        {
            // Initialise searcher to scan for OS information
            _Searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            _Name = "OS";
        }

        #region Methods
        /// <summary>
        /// Main Profiler Rountine
        /// </summary>
        /// <param name="pSpecs"> Master dictionary of Specifications </param>
        /// <returns> Updated dictionary of Specifications </returns>
        public override IDictionary<string, string> GetDetails(IDictionary<string, string> pSpecs)
        {
            // For each object in the searcher, save OS name and Build Number if possible
            foreach (ManagementObject obj in _Searcher.Get())
            {
                // Locate OS Name
                if (obj["Caption"] != null)
                {
                    // Store OS Name in _Name
                    _Name = obj["Caption"].ToString();
                    // Check if Windows 10
                    if (_Name == "Microsoft Windows 10 Home" || _Name == "Microsoft Windows 10 Pro" || _Name == "Microsoft Windows 10")
                        // Read the registry for the ReleaseId Key, should be the build number, return Null if key doesnt exist
                        // Append to the _Name when found
                        _Name += " " + Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ReleaseId", null).ToString();

                    // Save the final _Name to the spec list
                    pSpecs.Add("Windows_Version", _Name);
                }
                // Locate Architecture
                if (obj["OSArchitecture"] != null)
                    pSpecs.Add("Windows_Architecture", obj["OSArchitecture"].ToString());

                // Locate Install Date
                if (obj["InstallDate"] != null)
                    pSpecs.Add("Windows_Install_Date", ConvertTime(obj["InstallDate"].ToString()));

                // Locate Hostname
                if (obj["CSName"] != null)
                    pSpecs.Add("Windows_Hostname", obj["CSName"].ToString());

            }
            return pSpecs;
        }
        private string ConvertTime(string pTime)
        {
            pTime = pTime.Remove(13, 11);
            DateTime mTime;
            DateTime.TryParseExact(pTime, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out mTime);
            return mTime.ToString();
        }
        #endregion
    }
}

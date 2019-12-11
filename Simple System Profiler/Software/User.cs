using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_System_Profiler.Interfaces;
using System.Management;

namespace Simple_System_Profiler.Software
{
    class User : AComponentProfiler
    {
        //int _CurrentUser = 0; // User Account counter
        /// <summary>
        /// User Profiler
        /// Gets the Usernames of each acoount
        /// </summary>
        public User()
        {
            // Initialise searcher to scan for User information
            _Searcher = new ManagementObjectSearcher("select * from Win32_UserAccount");
            _Name = "User";
        }

        #region Methods
        /// <summary>
        /// Main Profiler Rountine
        /// </summary>
        /// <param name="pSpecs"> Master dictionary of Specifications </param>
        /// <returns> Updated dictionary of Specifications </returns>
        public override IDictionary<string, string> GetDetails(IDictionary<string, string> pSpecs)
        {
            // NEW METHOD - Uses Environment
            string mUser = Environment.UserName;

            pSpecs.Add("User_Account", mUser);

            // OLD METHOD - Uses System.Management

            //// For each object in the searcher, save Username
            //foreach (ManagementObject obj in _Searcher.Get())
            //{
            //    if (obj["Name"] != null && obj["Domain"] != null)
            //        pSpecs.Add("User_Account_" + _CurrentUser.ToString(), obj["Domain"].ToString() + "\\" + obj["Name"].ToString());

            //    _CurrentUser++;
            //}         
            
            return pSpecs;
        }
        #endregion
    }
}

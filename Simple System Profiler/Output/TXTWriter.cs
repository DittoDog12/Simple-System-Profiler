using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_System_Profiler.Interfaces;

namespace Simple_System_Profiler.Output
{
    class TXTWriter : AWriter
    {
        public override void Write(IDictionary<string, string> pSpecs, string pPath)
        {
            // Create a file in the Kernel Specifed Path with the current machine Hostname as the filename
            _Path = pPath + "\\" + pSpecs["Windows_Hostname"].ToString() + ".txt";

            // For each spec in the list
            // Create a node with the Spec name
            // Create a string value inside the node with the Spec Value
            foreach (KeyValuePair<string, string> mKeyPair in pSpecs)
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(_Path, true))
                {
                    try
                    {
                        file.WriteLine(mKeyPair.Key + ": " + mKeyPair.Value);
                    }
                    catch
                    {
                        throw new Hex("Unable to save to Text File: " + _Path);
                    }
                }
        }
    }
}

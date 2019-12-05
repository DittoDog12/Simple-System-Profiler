using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Simple_System_Profiler.Interfaces;

namespace Simple_System_Profiler.Output
{
    /// <summary>
    /// XML File Writer
    /// </summary>
    class XMLWriter : AWriter
    {
        XmlWriter _Writer;

        public XMLWriter()
        {
        }

        public override void Write(IDictionary<string, string> pSpecs, string pPath)
        {
            try
            {


                // Create a file in the Kernel Specifed Path with the current machine Hostname as the filename
                _Path = pPath + "\\" + pSpecs["Windows_Hostname"].ToString() + ".xml";
                _Writer = XmlWriter.Create(_Path);

                _Writer.WriteStartDocument(); // Open Document
                _Writer.WriteStartElement("Computer_Profile"); // Create Main Node

                // For each spec in the list
                // Create a node with the Spec name
                // Create a string value inside the node with the Spec Value
                foreach (KeyValuePair<string, string> mKeyPair in pSpecs)
                {
                    _Writer.WriteStartElement(mKeyPair.Key);
                    _Writer.WriteString(mKeyPair.Value);
                    _Writer.WriteEndElement();
                }

                // Close XML
                _Writer.WriteEndDocument();
                _Writer.Close();
            }
            catch
            {
                throw new Hex("Unable to write to XML file: " + _Path);
            }
        }
    }
}

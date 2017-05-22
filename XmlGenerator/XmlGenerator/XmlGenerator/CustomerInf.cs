using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using PropertyGridEx;

namespace XmlGenerator
{
    partial class Form1 
    {
        private void custominf()
        {
            string CustomerInfPath = "F:\\Emulator\\IDE\\SinoSunIDE\\XmlGenerator\\XmlGenerator\\XmlGenerator\\bin\\Debug\\Customer.xml";

             try
             {
                 XDocument CustomerInfDox = new XDocument(
                 new XDeclaration("1.00", "utf-8", "yes"),
                 new XComment("customer information config"),
                new XComment("date : 2013.03.15"),
                new XElement("WinScope", new XAttribute("Name", "Information"), new XAttribute("Version", "1.00"),
                 new XAttribute("Author", "Mike.Mo"),

               new XElement("About",
                   new XAttribute("name", "SINOMCU"),
                   new XAttribute("web", "http://www.sinomcu.com"),
                   new XAttribute("tel", "021-38682906"),
                   new XAttribute("mail", "support@sinomcu.com"),
                   new XAttribute("Txt", "这是一个刚开发出来的IDE平台，在试用期间如发现任何问题请随时和我们联系。联系电话：021-38682906 Email: Support@sinomcu.com")

                   ),
               new XElement("Windown",
                   new XAttribute("name","WinScope IDE bt v0.01"),
                   new XAttribute("velcom","欢迎使用WinScope IDE!"),
                   new XAttribute("web","http://www.sinomcu.com")

                   )
                )

                );

                 CustomerInfDox.Save(CustomerInfPath);

             }
             catch (System.Exception ex)
             {
                 MessageBox.Show(ex.ToString());
             }
   
        }

    }


}
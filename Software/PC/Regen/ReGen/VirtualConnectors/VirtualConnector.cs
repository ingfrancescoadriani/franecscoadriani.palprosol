using Sintec.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VirtualConnectors
{
    public abstract class VirtualConnector
    {
        public abstract String getPreview(string workDataXmlString);
        public abstract String getIstruction(string workDataXmlString);
        public abstract void send(string workDataXmlString);
        public abstract void sendTag(string tag, string workDataXmlString);
        public abstract void init(string workDataXmlString);
        public void init(string workDataXmlString, String languagesPath)
        {
            init(workDataXmlString);
            setLanguage(languagesPath);
        }
        public abstract System.Windows.Forms.Panel getSettingsPanel(string workDataXmlString);
        public abstract void refreshLanguage();


        private IniFile iniLang;
        public String languagesPath = "";

        public bool setLanguage(String languagesPath)
        {
            try
            {
                if (iniLang == null)
                    iniLang = new IniFile(languagesPath);
                else
                    iniLang.setPath(languagesPath);
            }
            catch (Exception e)
            {
                return false;
            }
            refreshLanguage();
            return true;
        }

        public String translate(String phrase, String defaultString)
        {
            String ret = "" + iniLang.IniReadValue("translation", phrase, defaultString);
            if (ret.Equals("") && !defaultString.Equals(""))
            {
                ret = phrase;
            }
            return (ret);
        }

        public String translate(String phrase)
        {
            return (translate(phrase, ""));
        }

    }
}

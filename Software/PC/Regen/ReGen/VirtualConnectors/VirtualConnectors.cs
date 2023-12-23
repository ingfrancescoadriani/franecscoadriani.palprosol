using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace VirtualConnectors
{
    public class VirtualConnectors
    {
        public static List<String> getTagContents(String xmlFile, String tag)
        {
            List<String> res=new List<string>();

            XmlDocument xmlDoc = new XmlDocument(); //* create an xml document object.
            xmlDoc.LoadXml(xmlFile); //* load the XML document from the specified file.
            XmlNodeList appInfo = xmlDoc.GetElementsByTagName("applicationInfo");
            foreach (XmlElement e in appInfo[0].ChildNodes)
            {
                if (e.Name == "Regen")
                {
                    foreach (XmlAttribute a in e.Attributes)
                    {
                        if (a.Name == "version")
                        {
                            if (tag == "version" || tag=="*") 
                            {
                                res.Add(a.InnerText );
                            }
                        }

                    }
                }
            }
            XmlNodeList workData = xmlDoc.GetElementsByTagName("workData");
            int pstrat = 0;
            foreach (XmlElement e in workData[0].ChildNodes)
            {
                if (e.Name == "payloadStrategy")
                {
                    foreach (XmlElement f in e.ChildNodes)
                    {
                        if (f.Name == "name")
                        {
                            foreach (XmlAttribute a in f.Attributes)
                            {
                                if (a.Name == "id")
                                {
                                    if (tag == "payloadStrategy" + pstrat.ToString() + "_name_id")
                                        res.Add(a.InnerText);
                                    if (tag == "*")
                                        res.Add("payloadStrategy" + pstrat.ToString() + "_name_id_" + a.InnerText);
                                }
                            }
                            if (tag == "payloadStrategy" + pstrat.ToString() + "_name")
                                res.Add(f.InnerText);
                            if (tag == "*")
                                res.Add("payloadStrategy" + pstrat.ToString() + "_name_" + f.InnerText);
                        }
                        if (f.Name == "data")
                        {
                            if (tag == "payloadStrategy" + pstrat.ToString() + "_data")
                                res.Add(f.InnerText);
                            if (tag == "*")
                                res.Add("payloadStrategy" + pstrat.ToString() + "_data_" + f.InnerText);
                        }
                        if (f.Name == "toolData")
                        {
                            if (tag == "payloadStrategy" + pstrat.ToString() + "_toolData")
                                res.Add(f.InnerText);
                            if (tag == "*")
                                res.Add("payloadStrategy" + pstrat.ToString() + "_toolData_" + f.InnerText);
                        }
                        if (f.Name == "initialCenter")
                        {
                            if (tag == "payloadStrategy" + pstrat.ToString() + "_initialCenter")
                                res.Add(f.InnerText);
                            if (tag == "*")
                                res.Add("payloadStrategy" + pstrat.ToString() + "_initialCenter_" + f.InnerText);
                        }

                        if (f.Name == "size")
                        {
                            String w = "";
                            String l = "";
                            String h = "";
                            foreach (XmlElement b in f.ChildNodes)
                            {
                                if (b.Name == "width")
                                {
                                    w = b.InnerText;
                                    if (tag == "payloadStrategy" + pstrat.ToString() + "_width" || tag == "payloadStrategy" + pstrat.ToString() + "_size")
                                    {
                                        res.Add(b.InnerText);
                                    }
                                }
                                if (b.Name == "length")
                                {
                                    l = b.InnerText;
                                    if (tag == "payloadStrategy" + pstrat.ToString() + "_length" || tag == "payloadStrategy" + pstrat.ToString() + "_size")
                                    {
                                        res.Add(b.InnerText);
                                    }
                                }
                                if (b.Name == "height")
                                {
                                    h = b.InnerText;
                                    if (tag == "payloadStrategy" + pstrat.ToString() + "_height" || tag == "payloadStrategy" + pstrat.ToString() + "_size")
                                    {
                                        res.Add(b.InnerText);
                                    }
                                }
                            }
                            if (tag == "payloadStrategy" + pstrat.ToString() + "_size")
                                res.Add("(" + w + ";" + l + ";" + h + ")");
                            if (tag == "*")
                                res.Add("payloadStrategy" + pstrat.ToString() + "_size_(" + w + ";" + l + ";" + h + ")");
                        }
                    }
                    pstrat++;
                }
                if (e.Name == "extraData")
                {
                    if (tag == "extraData")
                        res.Add(e.InnerText);
                    if (tag == "*")
                        res.Add("extraData_" + e.InnerText);
                }
                if (e.Name == "palletOnSystem")
                {
                    int l = 0;

                    foreach (XmlAttribute attr in e.Attributes)
                    {
                        if (attr.Name == "interLayerThickness")
                        {
                            if (tag == "*")
                                res.Add("palletOnSystem_interLayerThickness_" + attr.InnerText);
                            if (tag == "palletOnSystem_interLayerThickness")
                                res.Add(attr.InnerText);
                        }
                        if (attr.Name == "interLayer")
                        {
                            if (tag == "*")
                                res.Add("palletOnSystem_interLayer_" + attr.InnerText);
                            if (tag == "palletOnSystem_interLayer")
                                res.Add(attr.InnerText);
                        }
                        if (attr.Name == "panelOverPalletThickness")
                        {
                            if (tag == "*")
                                res.Add("palletOnSystem_panelOverPalletThickness_" + attr.InnerText);
                            if (tag == "palletOnSystem_panelOverPalletThickness")
                                res.Add(attr.InnerText);
                        }
                        if (attr.Name == "panelOverPallet")
                        {
                            if (tag == "*")
                                res.Add("palletOnSystem_panelOverPallet_" + attr.InnerText);
                            if (tag == "palletOnSystem_panelOverPallet")
                                res.Add(attr.InnerText);
                        }
                    }


                    foreach (XmlElement f in e.ChildNodes)
                    {
                        if (f.Name == "otherInfo")
                        {
                            if (tag == "*")
                                res.Add("palletOnSystem_otherInfo_" + f.InnerText);
                            if (tag == "palletOnSystem_otherInfo")
                                res.Add(f.InnerText);
                        }
                        if (f.Name == "angleOfRanker")
                        {
                            if (tag == "*")
                                res.Add("palletOnSystem_angleOfRanker_" + f.InnerText);
                            if (tag == "palletOnSystem_angleOfRanker")
                                res.Add(f.InnerText);
                        }
                        if (f.Name == "approachLenght")
                        {
                            if (tag == "*")
                                res.Add("palletOnSystem_approachLenght_" + f.InnerText);
                            if (tag == "palletOnSystem_approachLenght")
                                res.Add(f.InnerText);
                        }
                        if (f.Name == "positionOfZero")
                        {
                            if (tag == "*")
                                res.Add("palletOnSystem_positionOfZero_" + f.InnerText);
                            if (tag == "palletOnSystem_positionOfZero")
                                res.Add(f.InnerText);
                        }
                        if (f.Name == "extraBorder")
                        {
                            if (tag == "*")
                                res.Add("palletOnSystem_extraBorder_" + f.InnerText);
                            if (tag == "palletOnSystem_extraBorder")
                                res.Add(f.InnerText);
                        }
                        if (f.Name == "size")
                        {
                            if (tag == "*")
                                res.Add("palletOnSystem_size" + f.InnerText);
                            if (tag == "palletOnSystem_size")
                                res.Add(f.InnerText);
                        }
                        if (f.Name == "layer")
                        {
                            int pg = 0;
                            foreach (XmlAttribute attr in f.Attributes)
                            {
                                if (attr.Name == "interLayer")
                                {
                                    if (tag == "*")
                                        res.Add("layer" + l + "_interlayer_" + attr.InnerText);
                                    if (tag == "layer" + l + "_interlayer")
                                        res.Add(attr.InnerText);
                                }
                                if (attr.Name == "interLayerThickness")
                                {
                                    if (tag == "*")
                                        res.Add("layer" + l + "_interLayerThickness_" + attr.InnerText);
                                    if (tag == "layer" + l + "_interLayerThickness")
                                        res.Add(attr.InnerText);
                                }
                            }
                            foreach (XmlElement child in f.ChildNodes)
                            {
                                if (child.Name == "payloadGroups")
                                {
                                    foreach (XmlAttribute att in child.Attributes)
                                    {
                                        if (att.Name == "layer")
                                        {
                                            if (tag == "*")
                                                res.Add("layer" + l + "_payloadGroup" + pg + "_layer_" + att.InnerText);
                                            if (tag == "layer" + l + "_payloadGroup" + pg + "_layer")
                                                res.Add(att.InnerText);
                                        }
                                        if (att.Name == "payloadGroup")
                                        {
                                            if (tag == "*")
                                                res.Add("layer" + l + "_payloadGroup" + pg + "_payloadGroup_" + att.InnerText);
                                            if (tag == "layer" + l + "_payloadGroup" + pg + "_payloadGroup")
                                                res.Add(att.InnerText);
                                        }
                                        if (att.Name == "number")
                                        {
                                            if (tag == "*")
                                                res.Add("layer" + l + "_payloadGroup" + pg + "_number_" + att.InnerText);
                                            if (tag == "layer" + l + "_payloadGroup" + pg + "_number")
                                                res.Add(att.InnerText);
                                        }
                                    }
                                    foreach (XmlElement child4 in child.ChildNodes)
                                    {
                                        if (child4.Name == "payloadGroup")
                                        {
                                            int p = 0;
                                            foreach (XmlElement ch in child4.ChildNodes)
                                            {
                                                if (ch.Name == "pointCatching")
                                                {
                                                    if (tag == "*")
                                                        res.Add("layer" + l + "_payloadGroup" + pg + "_pointCatching_" + ch.InnerText);
                                                    if (tag == "layer" + l + "_payloadGroup" + pg + "_pointCatching")
                                                        res.Add(ch.InnerText);
                                                }
                                                if (ch.Name == "center")
                                                {
                                                    if (tag == "*")
                                                        res.Add("layer" + l + "_payloadGroup" + pg + "_center_" + ch.InnerText);
                                                    if (tag == "layer" + l + "_payloadGroup" + pg + "_center")
                                                        res.Add(ch.InnerText);
                                                }
                                                if (ch.Name == "quadrant")
                                                {
                                                    if (tag == "*")
                                                        res.Add("layer" + l + "_payloadGroup" + pg + "_quadrant_" + ch.InnerText);
                                                    if (tag == "layer" + l + "_payloadGroup" + pg + "_quadrant")
                                                        res.Add(ch.InnerText);
                                                }
                                                if (ch.Name == "progressiveNumber")
                                                {
                                                    if (tag == "*")
                                                        res.Add("layer" + l + "_payloadGroup" + pg + "_progressiveNumber_" + ch.InnerText);
                                                    if (tag == "layer" + l + "_payloadGroup" + pg + "_progressiveNumber")
                                                        res.Add(ch.InnerText);
                                                }
                                                if (ch.Name == "approach")
                                                {
                                                    if (tag == "*")
                                                        res.Add("layer" + l + "_payloadGroup" + pg + "_approach_" + ch.InnerText);
                                                    if (tag == "layer" + l + "_payloadGroup" + pg + "_approach")
                                                        res.Add(ch.InnerText);
                                                }
                                                if (ch.Name == "payloadsPlaced")
                                                {
                                                    foreach (XmlElement ch2 in ch.ChildNodes)
                                                    {
                                                        if (ch2.Name == "payloadPlaced")
                                                        {
                                                            foreach (XmlAttribute att in ch2.Attributes)
                                                            {
                                                                if (att.Name == "strategy")
                                                                {
                                                                    if (tag == "*")
                                                                        res.Add("layer" + l + "_payloadGroup" + pg + "_payload" + p + "_strategy_" + att.InnerText);
                                                                    if (tag == "layer" + l + "_payloadGroup" + pg + "_payload" + p + "_strategy")
                                                                        res.Add(att.InnerText);
                                                                }
                                                                if (att.Name == "id")
                                                                {
                                                                    if (tag == "*")
                                                                        res.Add("layer" + l + "_payloadGroup" + pg + "_payload" + p + "_name_id_" + att.InnerText);
                                                                    if (tag == "layer" + l + "_payloadGroup" + pg + "_payload" + p + "_name_id")
                                                                        res.Add(att.InnerText);
                                                                }
                                                            }
                                                            foreach (XmlElement chi in ch2.ChildNodes)
                                                            {
                                                                if (chi.Name == "center")
                                                                {
                                                                    if (tag == "*")
                                                                        res.Add("layer" + l + "_payloadGroup" + pg + "_payload" + p + "_center_" + chi.InnerText);
                                                                    if (tag == "layer" + l + "_payloadGroup" + pg + "_payload" + p + "_center")
                                                                        res.Add(chi.InnerText);
                                                                }
                                                                if (chi.Name == "quadrant")
                                                                {
                                                                    if (tag == "*")
                                                                        res.Add("layer" + l + "_payloadGroup" + pg + "_payload" + p + "_quadrant_" + chi.InnerText);
                                                                    if (tag == "layer" + l + "_payloadGroup" + pg + "_payload" + p + "_quadrant")
                                                                        res.Add(chi.InnerText);
                                                                }
                                                            }
                                                            p++;
                                                            if (tag == "*" || tag == "payloadsPlaced")
                                                                res.Add("-");
                                                            if ((tag == "layer" + l + "_payloadGroup" + pg + "_payloadsPlaced"))
                                                                res.Add("-");
                                                        }
                                                    }
                                                }
                                            }
                                            if (tag == "*" || tag == "payloadGroups")
                                                res.Add("-");
                                            if ((tag=="layer" + l + "_payloadGroups"))
                                                res.Add("-");
                                            pg++;
                                        }
                                    }
                                }
                            }
                            if ((tag.IndexOf("layers") > -1) || tag == "*")
                                res.Add("-");
                            l++;
                        }
                    }
                }
            }
            end: return res;
        }
        public static List<Type> getTypeConnectors()
        {
            List<Type> _getTypeConnectors = new List<Type>();
            foreach (string a in Directory.GetFiles(System.IO.Path.GetDirectoryName(Application.ExecutablePath)))
            {
                FileInfo dInfo = new FileInfo(a);
                if (dInfo.Extension == ".dll")
                {
                    try
                    {
                        String sTemp = Path.GetTempFileName();
                        File.Delete(sTemp);
                        File.Copy(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + dInfo.Name, sTemp);
                        Assembly assembly = Assembly.LoadFrom(sTemp);
                        foreach (var type in assembly.GetTypes())
                        {
                            if (type.IsClass)
                            {
                                if (type.BaseType == typeof(VirtualConnector))
                                {
                                    _getTypeConnectors.Add(type);
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            return _getTypeConnectors;
            //Assembly assembly = Assembly.LoadFrom("MyNice.dll");

            //Type type = assembly.GetType("MyType");

            //object instanceOfMyType = Activator.CreateInstance(type);
        }
    }
}

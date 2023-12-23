/*
 * Created by SharpDevelop.
 * User: Rice Cipriani
 * Date: 26/09/2012
 * Time: 01:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.IO;
using Sintec.Tool;
using System.Drawing;
using System.Diagnostics;
using WcfClientServer;
using System.Collections.Generic;

namespace ReGen
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
    internal sealed class Program
	{
        public static bool debugState = false;
        public static bool infoRequesting = false;
        public static float deltaAligment =12; //millimeters

        public static bool withDoEvents = true;
        public static String imagePath = System.Windows.Forms.Application.UserAppDataPath + "\\img";
        public static String thumbPath = imagePath + "\\thumb";
        public static String languagesPath = System.Windows.Forms.Application.UserAppDataPath + "\\languages";
        public static Color colorDraggingPayloadOnDepot = Color.DodgerBlue;
        public static Color colorDraggingPayloadOnPlatform = Color.DodgerBlue;
        public static Color colorSelectedPayload = Color.DodgerBlue;
        public static Color colorInterlayer = Color.Brown;
        private static Dictionary<string, Color> _colorPlacedPayload = new Dictionary<string, Color>();
        private static Color baseColorPlacedPayload = Color.Green;//Color.DarkKhaki;
        private static Random r = new Random();
        public static Color colorPlacedPayload(String name)
        {
            if (!_colorPlacedPayload.ContainsKey(name))
            {
                HSLColor hslColor = new HSLColor(baseColorPlacedPayload);
                hslColor.Luminosity *= (0.5 + (1.0 * r.NextDouble())); // 0,5 to 1,5
                hslColor.Hue *= (0.5 + (1.0 * r.NextDouble())); // 0,5 to 1,5
                Color darkenColor = hslColor;
                _colorPlacedPayload.Add(name,darkenColor);
            }
            return _colorPlacedPayload[name];
        }

        public static Color colorIncorrectlyPlacedPayload = Color.Red;
        public static Color colorOfSurplus = Color.DarkBlue;
        public static Color colorOfArrow = Color.Orange;
        /// <summary>
        /// Color Wheat
        /// </summary>
        public static Color colorOfPallet = Color.Wheat;
        //TODO campi per la pinza tolti
        //
        public static Color colorOfFinger = Color.Yellow;
        /// <summary>
        /// Larghezza delle barre che rappresentano la pinza
        /// </summary>
        public static double widthFinger = 22;
        /// <summary>
        /// Altezza delle barre che rappresentano la pinza
        /// </summary>
        public static double heigthFinger = 410;
        public static double startSeriesFinger = 49;
        public static int numberFinger = 11;
        public static double distanceBetweenFingers = 46;
        //
        public static Graphics g1;
        public static Log log;
        public static int xWeightPower = 4;
            /// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
            String h = System.Windows.Forms.Application.UserAppDataPath + "\\log";
            if (!Directory.Exists(h))
                Directory.CreateDirectory(h);
            //Program.log = new Sintec.Tool.Log(DateTime.Today.Day.ToString("00"), Log.getLogPathAndFix(h));
            //Program.log.LogThis("App started", eloglevel.verbose);
            Process p = processOpen(Application.ProductName);
            if (p != null)
            {
                //Program.log.LogThis("process found: " + p.ProcessName + " - " + Application.ProductName, eloglevel.verbose);
                //MessageBox.Show(p.Id + ":" + p.ProcessName + "    --    " + Process.GetCurrentProcess().Id + ":" + Process.GetCurrentProcess().ProcessName);
                if (args.Length > 0 && File.Exists(args[0]))
                    new WcfClient().Send(args[0]);
                else
                    new WcfClient().Send("");

                //Program.log.LogThis("before exit", eloglevel.verbose);
                Environment.Exit(0);
            }
			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            fixSubDirectory();
            //Program.log.LogThis("fixSubDirectory", eloglevel.verbose);
            MainForm.instance = new MainForm();
            if (args.Length > 0 && File.Exists(args[0]))
                MainForm.instance.fileToOpen = args[0];
            //Program.log.LogThis("reading args", eloglevel.verbose);
            g1 = Graphics.FromImage(new Bitmap((int)MainForm.instance.work.sizeWorkArea.X, (int)MainForm.instance.work.sizeWorkArea.Y));
            //Program.log.LogThis("Graphics.FromImage", eloglevel.verbose); 
            Application.Run(MainForm.instance);
		}

        public static void fixSubDirectory()
        {
            if (!Directory.Exists(languagesPath))
                Directory.CreateDirectory(languagesPath);
            if (!Directory.Exists(imagePath))
                Directory.CreateDirectory(imagePath);
            if (!Directory.Exists(thumbPath))
                Directory.CreateDirectory(thumbPath);
        }

        public static double getRobotRotation(double rotation)
        {
            //double res = -rotation;
            //res = (res + 180.0+ (360.0 * 2.0)) % 360.0;
            //if (res > 180.0)
            //    res = res - 360.0;
            double res = rotation;
            res = (res + (360.0*2.0)) % 360.0;
            return res;
        }

        private static Process processOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName == name && clsProcess.Id != Process.GetCurrentProcess().Id)
                {
                    return clsProcess;
                }
            }
            return null;
        }

        private static String lingua;
        private static IniFile iniLang;

        public static String currentLanguage()
        {
            return (lingua);
        }

        public static List<String> LanguageList()
        {
            List<String> tmp = new List<String>();
            DirectoryInfo di = new DirectoryInfo(languagesPath);
            FileInfo[] rgFiles = di.GetFiles("*.lng");
            foreach (FileInfo fi in rgFiles)
            {
                tmp.Add(fi.Name.Substring(0, fi.Name.Length - 4));
            }
            return (tmp);
        }
        
        public static bool setLanguage(String newLanguage)
        {
            try
            {
                if (iniLang == null)
                    iniLang = new IniFile(languagesPath +"\\"+ newLanguage + ".lng");
                else
                    iniLang.setPath(languagesPath + "\\" + newLanguage + ".lng");
                lingua = newLanguage;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static String translate(String phrase, String defaultString)
        {
            String ret = "" + iniLang.IniReadValue("translation", phrase, defaultString);
            if (String.IsNullOrEmpty(ret) && String.IsNullOrEmpty(defaultString))
                ret = phrase;
            return (ret);
        }

        public static String translate(String phrase)
        {
            return (translate(phrase, ""));
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using UBotPlugin;
using System.Linq;
using System.Windows;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Security.Cryptography;
using System.Configuration;
using System.Media;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Net;
using System.Management;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Data.OleDb;

namespace CSVtoHTML
{

    // API KEY HERE
    public class PluginInfo
    {
        public static string HashCode { get { return "d010ec4feae48a93d670a90d5b18f8aa310af14d"; } }
    }

    // ---------------------------------------------------------------------------------------------------------- //
    //
    // ---------------------------------               COMMANDS               ----------------------------------- //
    //
    // ---------------------------------------------------------------------------------------------------------- //

    //
    //
    // USE HMA 
    //
    //
    public class ChangeHMA_Wait : IUBotCommand
    {

        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public ChangeHMA_Wait()
        {
            var WTDHMA = new UBotParameterDefinition("HMA what to do?", UBotType.String);
            WTDHMA.Options = new[] { "", "Connect", "Disconnect", "Change ip" };//, "Get my ip"
            _parameters.Add(WTDHMA);

            var PathToHMA = new UBotParameterDefinition("HMA Folder Location", UBotType.String);
            PathToHMA.Options = new[] { "", "Program Files", "Program Files (x86)" };
            _parameters.Add(PathToHMA);

            //_parameters.Add(new UBotParameterDefinition("Your current IP", UBotType.UBotVariable));
        }

        public string Category
        {
            get { return "Settings Commands"; }
        }

        public string CommandName
        {
            get { return "hma tools"; }
        }


        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            string whatToDoHMA = parameters["HMA what to do?"];
            string pathToHMA = parameters["HMA Folder Location"];
            //string myCurrentIp = parameters["Your current IP"];
            
            if (whatToDoHMA == "Connect")
            {
                ConnectHMA(pathToHMA);
            }
            else if (whatToDoHMA == "Disconnect")
            {
                DisconnectHMA(pathToHMA);
            }
            else if (whatToDoHMA == "Change ip")
            {
                ChangeHMAIp(pathToHMA);

            }
            else{}

                       
        }
        
        public void ChangeHMAIp(string inputPath)
        {
            string pathToHma = inputPath;
            
            ExecuteCommandSync("C:\\" + inputPath + "\\HMA! Pro VPN\\bin\\HMA! Pro VPN.exe", "-changeip");
            
        }

        public void ConnectHMA(string inputPath)
        {
            string pathToHma = inputPath;

            ExecuteCommandSync("C:\\" + inputPath + "\\HMA! Pro VPN\\bin\\HMA! Pro VPN.exe", "-connect");
               
        }

        public void DisconnectHMA(string inputPath)
        {
            string pathToHma = inputPath;

            ExecuteCommandSync("C:\\" + inputPath + "\\HMA! Pro VPN\\bin\\HMA! Pro VPN.exe", "-disconnect");
                           
        }

        public void ExecuteCommandSync(string path, string parm)
        {
            try
            {
                //Declarations
                string filename = path; //The .exe to run | Example: C:\\Windows\\system32\\cmd.exe
                string parameter = parm; //The parameters | Example: msg * test
                //ShellExecute
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(filename);
                info.UseShellExecute = true;
                info.Verb = "open";
                info.Arguments = parameter;
                System.Diagnostics.Process.Start(info);
            }
            catch (Exception objException)
            {
                Console.WriteLine(objException); // Log the exception
            }
        }


        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }


    }

    //
    //
    // USE HMA FULL PATH
    //
    //
    public class ChangeHMA_Wait_FULLPath : IUBotCommand
    {

        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public ChangeHMA_Wait_FULLPath()
        {
            var WTDHMA = new UBotParameterDefinition("HMA what to do?", UBotType.String);
            WTDHMA.Options = new[] { "", "Connect", "Disconnect", "Change ip" };//, "Get my ip"
            _parameters.Add(WTDHMA);

            _parameters.Add(new UBotParameterDefinition("Full Path to HMA exe", UBotType.String));

            //_parameters.Add(new UBotParameterDefinition("Your current IP", UBotType.UBotVariable));
        }

        public string Category
        {
            get { return "Settings Commands"; }
        }

        public string CommandName
        {
            get { return "hma tools full path"; }
        }


        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            string whatToDoHMA = parameters["HMA what to do?"];
            string pathToHMA = parameters["Full Path to HMA exe"];
            //string myCurrentIp = parameters["Your current IP"];

            if (whatToDoHMA == "Connect")
            {
                ConnectHMA(pathToHMA);
            }
            else if (whatToDoHMA == "Disconnect")
            {
                DisconnectHMA(pathToHMA);
            }
            else if (whatToDoHMA == "Change ip")
            {
                ChangeHMAIp(pathToHMA);

            }
            else { }


        }

        public void ChangeHMAIp(string inputPath)
        {
            string pathToHma = inputPath;

            ExecuteCommandSync(inputPath, "-changeip");

        }

        public void ConnectHMA(string inputPath)
        {
            string pathToHma = inputPath;

            ExecuteCommandSync(inputPath, "-connect");

        }

        public void DisconnectHMA(string inputPath)
        {
            string pathToHma = inputPath;

            ExecuteCommandSync(inputPath, "-disconnect");

        }

        public void ExecuteCommandSync(string path, string parm)
        {
            try
            {
                //Declarations
                string filename = path; //The .exe to run | Example: C:\\Windows\\system32\\cmd.exe
                string parameter = parm; //The parameters | Example: msg * test
                //ShellExecute
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(filename);
                info.UseShellExecute = true;
                info.Verb = "open";
                info.Arguments = parameter;
                System.Diagnostics.Process.Start(info);
            }
            catch (Exception objException)
            {
                Console.WriteLine(objException); // Log the exception
            }
        }


        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }


    }



}

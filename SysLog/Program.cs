using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using static System.Configuration.ConfigurationSettings;

namespace SysLog
{
    class Program
    {
        struct Fields
        {
            public string DateTime;
            public string Ip;
            public string Source;
            public string Header;
            public string Community;
            public string RawMessage;
            public string TargetIp;
        }
        // Load variables from App.config
        private static readonly string InputFile = AppSettings["InputFile"];
        private static readonly string OutputFile = AppSettings["OutputFile"];
        // Load regulars from App.config
        private static readonly Regex ParseDateTime = new Regex(AppSettings["ParseDateTime"]);
        private static readonly Regex ParseIp = new Regex(AppSettings["ParseIp"]);
        private static readonly Regex ParseSource = new Regex(AppSettings["ParseSource"]);
        private static readonly Regex ParseHeader = new Regex(AppSettings["ParseHeader"]);
        private static readonly Regex ParseCommunity = new Regex(AppSettings["ParseCommunity"]);
        private static readonly Regex ParseRawMessage = new Regex(AppSettings["ParseRawMessage"]);
        private static readonly Regex ParseTargetIp = new Regex(AppSettings["ParseTargetIp"]);
        // Main program
        static void Main()
        {
            // Parse enrties from log to list
            List<Fields> data = new List<Fields>();
            using (var r = new StreamReader(InputFile))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    var logEntries = new Fields
                    {
                        DateTime = ParseDateTime.Match(line).Success ? ParseDateTime.Match(line).ToString() : null,
                        Ip = ParseIp.Match(line).Success ? ParseIp.Match(line).ToString() : null,
                        Source = ParseSource.Match(line).Success ? ParseSource.Match(line).ToString() : null,
                        Header = ParseHeader.Match(line).Success ? ParseHeader.Match(line).ToString() : null,
                        Community = ParseCommunity.Match(line).Success ? ParseCommunity.Match(line).ToString() : null,
                        RawMessage = ParseRawMessage.Match(line).Success ? ParseRawMessage.Match(line).ToString() : null,
                        TargetIp = ParseTargetIp.Match(line).Success ? ParseTargetIp.Match(line).ToString() : null
                    };
                    data.Add(logEntries); //Add filled structure to list
                }
            }
            foreach (var h in data)
            {
                // Serialize to json
                var json = new JavaScriptSerializer().Serialize(h);
                // Append to file
                File.AppendAllText(OutputFile, json + Environment.NewLine);
            }
        }
    }
}
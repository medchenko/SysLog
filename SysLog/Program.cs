using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

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

        // Variables
        private static string InputFile = @"D:\Downloads\syslog.log";
        private static string OutputFile = @"D:\result.json";

        // Load regulars
        private static readonly Regex ParseDateTime = new Regex(@"(^.{0,15})");
        private static readonly Regex ParseIp = new Regex(@"([0-9][0-9].[0-9].[0-9].[0-9][0-9])");
        private static readonly Regex ParseSource = new Regex(@"([\w]+\[(.*?)\])");
        private static readonly Regex ParseHeader = new Regex(@"([A-Z]\w+_[A-Z]\w+:)|([a-z]\w+_[a-z]\w+:)");
        private static readonly Regex ParseCommunity = new Regex(@"");
        private static readonly Regex ParseRawMessage = new Regex(@"");
        private static readonly Regex ParseTargetIp = new Regex(@"");


        // Main program
        static void Main()
        {
            List<Fields> data = new List<Fields>(); //тут типа наши события распарсенные жить будут

            using (StreamReader r = new StreamReader(InputFile))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    Fields logEntries = new Fields();
                    logEntries.DateTime = ParseDateTime.Match(line).Success ? ParseDateTime.Match(line).ToString() : null;
                    logEntries.Ip = ParseIp.Match(line).Success ? ParseIp.Match(line).ToString() : null;
                    logEntries.Source = ParseSource.Match(line).Success ? ParseSource.Match(line).ToString() : null;
                    logEntries.Header = ParseHeader.Match(line).Success ? ParseHeader.Match(line).ToString() : null;
                    logEntries.Community = ParseCommunity.Match(line).Success ? ParseCommunity.Match(line).ToString() : null;
                    logEntries.RawMessage = ParseRawMessage.Match(line).Success ? ParseRawMessage.Match(line).ToString() : null;
                    logEntries.TargetIp = ParseTargetIp.Match(line).Success ? ParseTargetIp.Match(line).ToString() : null;

                    data.Add(logEntries); //в конце добавляем заполненную структуру в лист и переходим к парсингу следующей строки
                }
            }
            foreach (Fields h in data)
            {
                var json = new JavaScriptSerializer().Serialize(h); //опачки!!!
                                                                    // ну и тут аппендим этот json в файл йопта
                File.AppendAllText(OutputFile, json + Environment.NewLine);
            }
        }
    }
}
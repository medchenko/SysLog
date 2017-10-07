using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SysLog
{
    class Program
    {
        // Variables
        private static string InputFile = @"D:\Downloads\syslog.log";
        private static string OutputFile = @"D:\result.json";
        private static int _counter;

        // Patterns
        private const string DateTimeRegEx = @"(^.{0,15})";
        private const string IpRegEx = @"([0-9][0-9].[0-9].[0-9].[0-9][0-9])";
        private const string SourceRegEx = @"([\w]+\[(.*?)\])";
        private const string HeaderRegEx = @"([A-Z]\w+_[A-Z]\w+:)|([a-z]\w+_[a-z]\w+:)";
        private const string CommunityRegEx = @"";
        private const string RawMessageRegEx = @"";
        private const string TargetIpRegEx = @"";

        // Load regulars
        private static readonly Regex ParseDateTime = new Regex(DateTimeRegEx);
        private static readonly Regex ParseIp = new Regex(IpRegEx);
        private static readonly Regex ParseSource = new Regex(SourceRegEx);
        private static readonly Regex ParseHeader = new Regex(HeaderRegEx);
        private static readonly Regex ParseCommunity = new Regex(CommunityRegEx);
        private static readonly Regex ParseRawMessage = new Regex(RawMessageRegEx);
        private static readonly Regex ParseTargetIp = new Regex(TargetIpRegEx);

        // Main program
        static void Main()
        {
            // 1
            // Declare new List.
            List<string> lines = new List<string>();

            // 2
            // Use using StreamReader for disposing.
            using (StreamReader r = new StreamReader(InputFile))
            {
                // 3
                // Use while != null pattern for loop.
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    // 4
                    // Insert logic here.
                    // ...
                    // The "line" value is a line in the file.
                    // Add it to our List.
                    lines.Add(line);
                }
            }

            // 5
            // Use RegEx to compare all the lines and write in output file.
            while (_counter != lines.Count)
            {
                foreach (string s in lines)
                {
                    //// Read each line until EOF.
                    // Try to match each line against the Regex.
                    Match m1 = ParseDateTime.Match(s);
                    // DateTime
                    if (m1.Success)
                    {
                        File.AppendAllText(OutputFile, @"{" + Environment.NewLine + "DateTime: " + m1 + "," + Environment.NewLine);
                    }
                    // IP
                    Match m2 = ParseIp.Match(s);
                    if (m2.Success)
                    {
                        File.AppendAllText(OutputFile, @"IP: " + m2 + "," + Environment.NewLine);
                    }
                    // Source
                    Match m3 = ParseSource.Match(s);
                    if (m3.Success)
                    {
                        File.AppendAllText(OutputFile, @"Source: " + m3 + "," + Environment.NewLine);
                    }
                    // Header
                    Match m4 = ParseHeader.Match(s);
                    if (m4.Success)
                    {
                        File.AppendAllText(OutputFile, @"Header: " + m4.ToString().Replace(":", "") + "," + Environment.NewLine);
                    }
                    // Community
                    Match m5 = ParseCommunity.Match(s);
                    if (m5.Success)
                    {
                        File.AppendAllText(OutputFile, @"Community: " + m5 + "," + Environment.NewLine);
                    }
                    // RawMessage
                    Match m6 = ParseRawMessage.Match(s);
                    if (m6.Success)
                    {
                        File.AppendAllText(OutputFile, @"RawMessage: " + m6 + "," + Environment.NewLine);
                    }
                    // TargetIp
                    Match m7 = ParseTargetIp.Match(s);
                    if (m7.Success)
                    {
                        File.AppendAllText(OutputFile, @"TargetIp: " + m7 + Environment.NewLine + "}" + Environment.NewLine);
                    }
                    _counter++;
                }
            }
        }
    }
}
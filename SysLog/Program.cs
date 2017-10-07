using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SysLog
{
    internal class Program
    {
        // Variables
        private const string InputFile = @"D:\Downloads\syslog.log";
        private const string OutputFile = @"D:\result.txt";
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
        private static void Main()
        {
            // Declare new List
            var lines = new List<string>();

            // Use using StreamReader for disposing
            using (var r = new StreamReader(InputFile))
            {
                // Use while != null pattern for loop
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    // The "line" value is a line in the file
                    // Add it to our List
                    lines.Add(line);
                }
            }

            // Use RegEx to compare all the lines and write in output file.
            while (_counter != lines.Count)
            {
                foreach (var s in lines)
                {
                    // Try to match each line against the Regex.

                    // DateTime
                    var m1 = ParseDateTime.Match(s);
                    if (m1.Success)
                    {
                        File.AppendAllText(OutputFile, @"DateTime: " + m1 + "," + Environment.NewLine);
                    }
                    // IP
                    var m2 = ParseIp.Match(s);
                    if (m2.Success)
                    {
                        File.AppendAllText(OutputFile, @"IP: " + m2 + "," + Environment.NewLine);
                    }
                    // Source
                    var m3 = ParseSource.Match(s);
                    if (m3.Success)
                    {
                        File.AppendAllText(OutputFile, @"Source: " + m3 + "," + Environment.NewLine);
                    }
                    // Header
                    var m4 = ParseHeader.Match(s);
                    if (m4.Success)
                    {
                        File.AppendAllText(OutputFile, @"Header: " + m4.ToString().Replace(":", "") + "," + Environment.NewLine);
                    }
                    // Community
                    var m5 = ParseCommunity.Match(s);
                    if (m5.Success)
                    {
                        File.AppendAllText(OutputFile, @"Community: " + m5 + "," + Environment.NewLine);
                    }
                    // RawMessage
                    var m6 = ParseRawMessage.Match(s);
                    if (m6.Success)
                    {
                        File.AppendAllText(OutputFile, @"RawMessage: " + m6 + "," + Environment.NewLine);
                    }
                    // TargetIp
                    var m7 = ParseTargetIp.Match(s);
                    if (m7.Success)
                    {
                        File.AppendAllText(OutputFile, @"TargetIp: " + m7 + Environment.NewLine + Environment.NewLine);
                    }

                    // Increment _counter
                    _counter++;
                }
            }
        }
    }
}
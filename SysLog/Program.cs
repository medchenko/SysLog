using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace SysLog
{
    internal class Program
    {
        // Main program
        private static void Main()
        {
            // Load config
            Dictionary<string, string> data = XDocument.Load("config.xml").Root?.Elements().ToDictionary(element => element.Name.ToString(), element => element.Value);

            // Load file
            using (var r = new StreamReader(data["InputFile"]))
            {
                string line;
                // Load every string in file
                while ((line = r.ReadLine()) != null)
                {
                    // Apply every regular epression in config
                    foreach (KeyValuePair<string, string> keyValue in data)
                    {
                        var logEnrty = new Regex(keyValue.Value).Match(line).Success ? new Regex(keyValue.Value).Match(line).ToString() : null;
                        if (logEnrty != null)
                        {
                            // Append results to file
                            var json = JsonConvert.SerializeObject(logEnrty,
                                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                            File.AppendAllText(data["OutputFile"], keyValue.Key + ":" + json.Replace("\"", " ") + Environment.NewLine);
                        }
                    }
                    File.AppendAllText(data["OutputFile"], Environment.NewLine);
                }
            }
        }
    }
}
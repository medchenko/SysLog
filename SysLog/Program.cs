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
            List<XElement> data = XDocument.Load("config.xml").Root?.Elements().ToList();
            // Load file
            if (data != null)
                using (var r = new StreamReader(data[0].Value))
                {
                    string line;
                    // Load every string in file
                    while ((line = r.ReadLine()) != null)
                    {
                        // Apply every regular epression in config
                        foreach (XElement keyValue in data)
                        {
                            MatchCollection matches = Regex.Matches(line, keyValue.Value);
                            int matchNum;
                            // Append results to file
                            if (keyValue.HasAttributes)
                            {
                                if (keyValue.FirstAttribute.Value == "last")
                                    if (matches.Count > 1)
                                        matchNum = matches.Count;
                                    else
                                        matchNum = -1;
                                else
                                {
                                    matchNum = int.Parse(keyValue.FirstAttribute.Value);
                                    if (matchNum > matches.Count)
                                        matchNum = -1;
                                }
                            }
                            else
                            {
                                matchNum = 1;
                            }
                            if (matchNum > -1 && matches.Count > 0)
                            {
                                var json = JsonConvert.SerializeObject(
                                    matches[matchNum - 1].ToString(),
                                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
                                File.AppendAllText(data[1].Value,
                                    keyValue.Name + ":" + json.Replace("\"", " ") + Environment.NewLine);
                            }
                        }
                        File.AppendAllText(data[1].Value, Environment.NewLine);
                    }
                }
            else
                Console.WriteLine("Error loading file!");
        }
    }
}
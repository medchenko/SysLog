# SysLog
Parsing log-file applying regular expressions and converting output to JSON file

# Basics

# Input file example

Feb 11 15:33:41 10.0.4.10 rpd[1402]: bgp_listen_accept: accept(0.0.0.0+179): Too many open files in system
Feb 11 15:33:41 10.0.4.10 rpd[1402]: task_accept: task BGP.0.0.0.0+179 socket 58 addr 0.0.0.0+179 : Too many open files in system
Feb 11 15:33:42 10.0.4.10 snmpd[1439]: SNMPD_AUTH_FAILURE: nsa_log_community: unauthorized SNMP community from 79.133.116.48 to unknown community name (public)
Feb 11 15:33:43 10.0.4.10 /kernel: kern.maxfiles limit exceeded by uid 0, please see tuning(7).

# Output file example

{"DateTime":"Feb 11 15:33:41","Ip":"10.0.4.10","Source":"rpd[1402]","Header":"bgp_listen_accept","Community":null,"RawMessage":"Too many open files in system","TargetIp":null}
{"DateTime":"Feb 11 15:33:41","Ip":"10.0.4.10","Source":"rpd[1402]","Header":"task_accept","Community":null,"RawMessage":"Too many open files in system","TargetIp":null}
{"DateTime":"Feb 11 15:33:42","Ip":"10.0.4.10","Source":"snmpd[1439]","Header":"SNMPD_AUTH_FAILURE","Community":"nsa_log_community","RawMessage":"unauthorized SNMP community from 79.133.116.48 to unknown community name (public)","TargetIp":"79.133.116.48"}
{"DateTime":"Feb 11 15:33:43","Ip":"10.0.4.10","Source":null,"Header":null,"Community":null,"RawMessage":"kern.maxfiles limit exceeded by uid 0, please see tuning(7).","TargetIp":null}

# Configurations

# How to change input file
  Simply change <add key="InputFile" value="D:\Downloads\syslog.log"/> to any yours path/filename in App.config

# How to change output file
  Change <add key="OutputFile" value="D:\Downloads\result.json"/> to any yours path/filename in App.config

# How to change regular expressions
  RegEx's are stored in App.config within keys:
    <add key="ParseDateTime" value="(^([A-Z][a-z]{2} \d{1,2} \d{2}:\d{2}:\d{2}))"/>
    <add key="ParseIp" value="([0-9][0-9].[0-9].[0-9].[0-9][0-9])"/>
    <add key="ParseSource" value="([\w]+\[(.*?)\])"/>
    <add key="ParseHeader" value="([A-Z]\w+_[A-Z]\w+:)|([a-z]\w+_[a-z]\w+:)"/>
    <add key="ParseCommunity" value="(nsa_log_community)"/>
    <add key="ParseRawMessage" value="(:(?!.*:) ((.+\s(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}).*)|.*))"/>
    <add key="ParseTargetIp" value="([0-9][0-9].[0-9][0-9][0-9].[0-9][0-9][0-9].[0-9][0-9])"/>

# How to disable some of regular expressions
  Just clear all chars between double commas like this: <add key="ParseDateTime" value="(^([A-Z][a-z]{2} \d{1,2} \d{2}:\d{2}:\d{2}))"/> to <add key="ParseDateTime" value=""/>

# How to add new regex

1. Add new key and value to App.config
 <add key="NewKey" value="NewValue"/>

2. Add new object to "struct" to Program.cs
  private struct Fields
        {
            public string NewKey;
            public string DateTime;
            // etc...
        }

3. Load new key from configuration
        private static readonly Regex ParseDateTime = new Regex(AppSettings["ParseDateTime"]);
        private static readonly Regex NewKey = new Regex(AppSettings["NewKey"]);
        // etc...

4. Add method to cycle
        while ((line = r.ReadLine()) != null)
                {
                    var logEntries = new Fields
                    {
                        DateTime = ParseDateTime.Match(line).Success ? ParseDateTime.Match(line).ToString().Replace(": ", "") : null,
                        NewKey = NewKey.Match(line).Success ? NewKey.Match(line).ToString() : null,
                        // etc...
                    };
                    data.Add(logEntries); //Add filled structure to list
                }

Enjoy!

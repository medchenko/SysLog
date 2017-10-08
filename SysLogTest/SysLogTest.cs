using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SysLogTest
{
    [TestClass]
    public class ProgramTests
    {
        // Testing structure
        [TestMethod]
        public void TestDateTime()
        {
            string DateTime = "Feb 11 15:33:41";
            string expected = "Feb 11 15:33:41";
            Assert.AreEqual(expected, DateTime);
        }
        [TestMethod]
        public void TestIp()
        {
            string Ip = "10.0.4.10";
            string expected = "10.0.4.10";
            Assert.AreEqual(expected, Ip);
        }
        [TestMethod]
        public void TestSource()
        {
            string Source = "rpd[1402]";
            string expected = "rpd[1402]";
            Assert.AreEqual(expected, Source);
        }
        [TestMethod]
        public void TestHeader()
        {
            string Header = "bgp_listen_accept";
            string expected = "bgp_listen_accept";
            Assert.AreEqual(expected, Header);
        }
        [TestMethod]
        public void TestCommunity()
        {
            string Community = "nsa_log_community";
            string expected = "nsa_log_community";
            Assert.AreEqual(expected, Community);
        }
        [TestMethod]
        public void TestRawMessage()
        {
            string RawMessage = "Too many open files in system";
            string expected = "Too many open files in system";
            Assert.AreEqual(expected, RawMessage);
        }
        [TestMethod]
        public void Test()
        {
            string TargetIp = "79.133.116.48";
            string expected = "79.133.116.48";
            Assert.AreEqual(expected, TargetIp);
        }
        // Testing RegEx
        [TestMethod]
        public void ParseDateTime()
        {
            string DateTimeRegEx = @"(^([A-Z][a-z]{2} \d{1,2} \d{2}:\d{2}:\d{2}))";
            string line = @"Feb 11 15:33:42 10.0.4.10 snmpd[1439]: SNMPD_AUTH_FAILURE: nsa_log_community: unauthorized SNMP community from 79.133.116.48 to unknown community name (public)";
            Regex parseDateTime = new Regex(DateTimeRegEx);
            string result = parseDateTime.Match(line).ToString();
            Assert.AreEqual(result, "Feb 11 15:33:42");
        }
        [TestMethod]
        public void ParseIp()
        {
            string Ip = @"([0-9][0-9].[0-9].[0-9].[0-9][0-9])";
            string line = @"Feb 11 15:33:42 10.0.4.10 snmpd[1439]: SNMPD_AUTH_FAILURE: nsa_log_community: unauthorized SNMP community from 79.133.116.48 to unknown community name (public)";
            Regex parseIp = new Regex(Ip);
            string result = parseIp.Match(line).ToString();
            Assert.AreEqual(result, "10.0.4.10");
        }
        [TestMethod]
        public void ParseSource()
        {
            string Source = @"([\w]+\[(.*?)\])";
            string line = @"Feb 11 15:33:42 10.0.4.10 snmpd[1439]: SNMPD_AUTH_FAILURE: nsa_log_community: unauthorized SNMP community from 79.133.116.48 to unknown community name (public)";
            Regex parseSource = new Regex(Source);
            string result = parseSource.Match(line).ToString();
            Assert.AreEqual(result, "snmpd[1439]");
        }
        [TestMethod]
        public void ParseHeader()
        {
            string Header = @"([A-Z]\w+_[A-Z]\w+:)|([a-z]\w+_[a-z]\w+:)";
            string line = @"Feb 11 15:33:42 10.0.4.10 snmpd[1439]: SNMPD_AUTH_FAILURE: nsa_log_community: unauthorized SNMP community from 79.133.116.48 to unknown community name (public)";
            Regex ParseHeader = new Regex(Header);
            string result = ParseHeader.Match(line).ToString().Replace(":", "");
            Assert.AreEqual(result, "SNMPD_AUTH_FAILURE");
        }
        [TestMethod]
        public void ParseCommunity()
        {
            string Community = @"(nsa_log_community)";
            string line = @"Feb 11 15:33:42 10.0.4.10 snmpd[1439]: SNMPD_AUTH_FAILURE: nsa_log_community: unauthorized SNMP community from 79.133.116.48 to unknown community name (public)";
            Regex ParseCommunity = new Regex(Community);
            string result = ParseCommunity.Match(line).ToString();
            Assert.AreEqual(result, "nsa_log_community");
        }
        [TestMethod]
        public void ParseRawMessage()
        {
            string RawMessage = @"(:(?!.*:) ((.+\s(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}).*)|.*))";
            string line = @"Feb 11 15:33:42 10.0.4.10 snmpd[1439]: SNMPD_AUTH_FAILURE: nsa_log_community: unauthorized SNMP community from 79.133.116.48 to unknown community name (public)";
            Regex ParseRawMessage = new Regex(RawMessage);
            string result = ParseRawMessage.Match(line).ToString().Replace(": ", "");
            Assert.AreEqual(result, "unauthorized SNMP community from 79.133.116.48 to unknown community name (public)");
        }
        [TestMethod]
        public void ParseTargetIp()
        {
            string TargetIp = @"([0-9][0-9].[0-9][0-9][0-9].[0-9][0-9][0-9].[0-9][0-9])";
            string line = @"Feb 11 15:33:42 10.0.4.10 snmpd[1439]: SNMPD_AUTH_FAILURE: nsa_log_community: unauthorized SNMP community from 79.133.116.48 to unknown community name (public)";
            Regex ParseTargetIp = new Regex(TargetIp);
            string result = ParseTargetIp.Match(line).ToString();
            Assert.AreEqual(result, "79.133.116.48");
        }
    }
}

# SysLog
Parsing log-file applying regular expressions and converting output to JSON file

## Basics

###### Input file example
```
Feb 11 15:33:41 10.0.4.10 rpd[1402]: bgp_listen_accept: accept(0.0.0.0+179): Too many open files in system
Feb 11 15:33:41 10.0.4.10 rpd[1402]: task_accept: task BGP.0.0.0.0+179 socket 58 addr 0.0.0.0+179 : Too many open files in system
Feb 11 15:33:41 10.0.4.10 rpd[1402]: bgp_listen_accept: accept(0.0.0.0+179): Too many open files in system
Feb 11 15:33:41 10.0.4.10 rpd[1402]: task_get_socket: domain AF_Route  type SOCK_SEQPACKET  protocol 0: Too many open files in system
Feb 11 15:33:41 10.0.4.10 rpd[1402]: RPD_ABORT: abort rpd[1402] version 10.4R5.5 built by builder on 2011-06-14 01:34:42 UTC: Too many open files in system
Feb 11 15:33:42 10.0.4.10 snmpd[1439]: SNMPD_AUTH_FAILURE: nsa_log_community: unauthorized SNMP community from 79.133.116.48 to unknown community name (public)
```
###### Output file example
```
DateTime: Feb 11 15:33:41 
IP: 10.0.4.10  
Source: rpd[1402] 
Header: bgp_listen_accept: 
Details: accept(0.0.0.0+179) 
RawMessage: : Too many open files in system 

DateTime: Feb 11 15:33:41 
IP: 10.0.4.10  
Source: rpd[1402] 
Accept: task_accept 
Details: task BGP.0.0.0.0+179 socket 58 addr 0.0.0.0+179 
RawMessage: : Too many open files in system 

DateTime: Feb 11 15:33:41 
IP: 10.0.4.10  
Source: rpd[1402] 
Header: bgp_listen_accept: 
Details: accept(0.0.0.0+179) 
RawMessage: : Too many open files in system 

DateTime: Feb 11 15:33:41 
IP: 10.0.4.10  
Source: rpd[1402] 
Header: task_get_socket: 
RawMessage: : Too many open files in system 

DateTime: Feb 11 15:33:41 
IP: 10.0.4.10  
Source: rpd[1402] 
Header: RPD_ABORT: 
RawMessage: : Too many open files in system 

DateTime: Feb 11 15:33:42 
IP: 10.0.4.10  
Source: snmpd[1439] 
Header: SNMPD_AUTH_FAILURE: 
Community: nsa_log_community 
RawMessage: : unauthorized SNMP community from 79.133.116.48 to unknown community name (public) 
TargetIp: 79.133.116.48  
```
## Configuration

###### How to change input file
Simply edit ```config.xml```

## Enjoy!

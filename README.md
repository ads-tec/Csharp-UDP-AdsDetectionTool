# Csharp-UDP-AdsDetectionTool
C# Reference Implementation for the ads-tec Device Detection using UDP broadcast

The ads-tec detection protocol allows device discovery by UDP broadcasts. It is meant for initial rollout of the ads-tec devices.
It supports changing the IP address of the target device to a matching address of the local network configuration.

# Security notice:
Please note that the adsdp UDP service is unencrypted and should only be used for initial configuration of the IP adress using the factory default credentials or only over a trusted network connection.
Afterwards one should use the ads-tec JSON-RPC API via HTTPS to change the credentials to the final values.

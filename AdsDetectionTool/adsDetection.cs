// Copyright (c) 2024, ads-tec IIT GmbH
// All rights reserved.
// This source code is licensed under the BSD 2-Clause License.
// Please see the LICENSE file in the root directory of this source tree for more details.

using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace adsDetectionTool
{
    //static class adsDetectionTool
    public class AdsDetectionProtocol
    {
        private const byte PROTO_VERS_MAJOR = 1;
        private const byte PROTO_VERS_MINOR = 0;
        private const byte IPV4_LEN_OLD = 4;
        private const byte IPV6_LEN_OLD = 6;
        private const byte IPV4_LEN = 8;
        private const byte IPV6_LEN = 12;
        private const byte SERIALNO_LEN = 16;

        private const int UDP_PORT = 890;


        private static int ACK_OKAY = 1;
        private static int REQ_DEVINFO = 1;
        private static int ADSDPDU_TYPE_SERIALNO = 2;
        private static int ADSDPDU_TYPE_IPV4 = 3;
        private static int ADSDPDU_TYPE_END = 0;
        private static int ADSDPDU_TYPE_PORT = 19;

        private bool m_bSetIPOnlyOverBroadcast = true;
        private enum Requests : byte
        {
            ADS_MMSREQ_DEVINFO = 1,
            ADS_MMSREQ_SETIP
        };

        private enum Acknowledges : byte
        {
            ADS_MMSACK_OKAY = 1,
            ADS_MMSERR_UNKNOWN,
            ADS_MMSERR_BADHEADER,
            ADS_MMSERR_BADREQUEST,
            ADS_MMSERR_BADREPLY,
            ADS_MMSERR_BADPASSWD,
            ADS_MMSERR_BADIP,
            ADS_MMSERR_SETIPFAILED
        };

        private enum Deviceinfo : byte
        {
            ADSDPDU_TYPE_END = 0,
            ADSDPDU_TYPE_MAC,
            ADSDPDU_TYPE_SERIALNO,
            ADSDPDU_TYPE_IPV4,
            ADSDPDU_TYPE_IPV6,
            ADSDPDU_TYPE_PASSWD,
            ADSDPDU_TYPE_REASON,
            ADSDPDU_TYPE_ENCPASSWD,
            ADSDPDU_TYPE_TLVCFGVAR,
            ADSDPDU_TYPE_TLVSTAT,
            ADSDPDU_TYPE_USERNAME,
            ADSDPDU_TYPE_TLVTABINS,
            ADSDPDU_TYPE_TLVTABDEL,
            ADSDPDU_TYPE_FILE,
            ADSDPDU_TYPE_FILEPARAM,
            ADSDPDU_TYPE_FILENAME,
            ADSDPDU_TYPE_RAWDATA,
            ADSDPDU_TYPE_TLVTABGET,
            ADSDPDU_TYPE_CASCADE,
            ADSDPDU_TYPE_PORT
        };

        private enum AdpHeader : byte
        {
            VERSMAJOR = 0,//14
            VERSMINOR,
            REQUEST,
            ACKNOWL
        }

        List<string> ipAddressList = new List<string>();

        public AdsDetectionProtocol()
        {
            string strHostName = Dns.GetHostName();

            //***********************************************************************************
            // Find host by name
            //***********************************************************************************
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            //***********************************************************************************
            // Enumerate local IP addresses and save them in the corresponding list
            //***********************************************************************************
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                if (ipaddress.AddressFamily != AddressFamily.InterNetwork)
                    continue;

                string ip = ipaddress.ToString();
                ipAddressList.Add(ip);
            }
        }
        
        public bool DetectAdsDevice(ref List<adstecDevice> adsDevices)
        {
            string strSN = "", strIP = "", strSubnet = "";

            foreach (string localIP in ipAddressList)
            {
                //***********************************************************************************
                // For debugging purposes over a certain ip interface
                //***********************************************************************************
                //if (!localIP.Equals("192.168.0.121"))
                //continue;

                int timeout = 200; // in miliseconds

                if (sendAndReceiveDevInfo(localIP, timeout, ref strSN, ref strIP, ref strSubnet))
                {
                    adstecDevice dev = new adstecDevice(strSN, strIP, strSubnet, localIP);
                    adsDevices.Add(dev);
                }
            }
            return true;
        }

        public bool DetectAdsDeviceAsync(adsDetectionToolForm  parent)
        {
            foreach (string localIP in ipAddressList)
            {
                //***********************************************************************************
                // For debugging purposes over a certain ip interface
                //***********************************************************************************
                //if (!localIP.Equals("192.168.0.121"))
                //    continue;

                sendDevInfoRequest(parent, localIP);
            }
            return true;
        }

        private bool receiveDevinfo(byte[] pBuf, ref string strSN, ref string strIP, ref string strSubnet)
        {
            bool bRetValue = true;

            switch (pBuf[(byte)AdpHeader.ACKNOWL])
            {
                case (byte)Acknowledges.ADS_MMSACK_OKAY:
                    { 
                    strSN = Encoding.Default.GetString(pBuf, 6, pBuf[5]).TrimEnd('\0');
                        if (pBuf[22] == (byte)Deviceinfo.ADSDPDU_TYPE_IPV4)
                        {
                            strIP = pBuf[24] + "." + pBuf[25] + "." + pBuf[26] + "." + pBuf[27];

                            if (pBuf[23] == 8)
                            {
                                strSubnet = pBuf[28] + "." + pBuf[29] + "." + pBuf[30] + "." + pBuf[31];
                            }
                        }
                        else if (pBuf[22] == (byte)Deviceinfo.ADSDPDU_TYPE_IPV6)
                        {
                            strIP = pBuf[24] + "." + pBuf[25] + "." + pBuf[26] + "." + pBuf[27] + "." + pBuf[28] + "." + pBuf[29];

                            if (pBuf[23] == 10)
                            {
                                strSubnet = pBuf[28] + "." + pBuf[29] + "." + pBuf[30] + "." + pBuf[31];
                            }
                        }
                        else
                        {
                            bRetValue = false;
                        }
                        if (pBuf[23] == 8)
                        {
                            strSubnet = pBuf[28] + "." + pBuf[29] + "." + pBuf[30] + "." + pBuf[31];
                        }
                    }
                    break;

                default:
                    bRetValue = false;
                    break;
            }
            return bRetValue;
        }
        public bool receiveSetip(byte[] pBuf, ref string strIP)
        {
            byte nRetValue = (byte)Acknowledges.ADS_MMSACK_OKAY;

            switch (pBuf[(byte)AdpHeader.ACKNOWL])
            {
                case (byte)Acknowledges.ADS_MMSACK_OKAY:
                    if (pBuf[4] == (byte)Deviceinfo.ADSDPDU_TYPE_IPV4)
                        strIP = pBuf[6] + "." + pBuf[7] + "." + pBuf[8] + "." + pBuf[9];
                    else if (pBuf[4] == (byte)Deviceinfo.ADSDPDU_TYPE_IPV6)
                        strIP = pBuf[6] + "." + pBuf[7] + "." + pBuf[8] + "." +
                                pBuf[9] + "." + pBuf[10] + "." + pBuf[11];
                    break;
                case (byte)Acknowledges.ADS_MMSERR_UNKNOWN:
                case (byte)Acknowledges.ADS_MMSERR_BADHEADER:
                case (byte)Acknowledges.ADS_MMSERR_BADREQUEST:
                case (byte)Acknowledges.ADS_MMSERR_BADREPLY:
                case (byte)Acknowledges.ADS_MMSERR_BADPASSWD:
                case (byte)Acknowledges.ADS_MMSERR_BADIP:
                case (byte)Acknowledges.ADS_MMSERR_SETIPFAILED:
                    if ((pBuf[4] == (byte)Deviceinfo.ADSDPDU_TYPE_REASON) && (pBuf[5] > 0))
                        strIP = Encoding.Default.GetString(pBuf, 6, pBuf[5]).TrimEnd('\0');
                    nRetValue = pBuf[(byte)AdpHeader.ACKNOWL];
                    break;
            }

            return (nRetValue == (byte)Acknowledges.ADS_MMSACK_OKAY);
        }
        public bool SendAndReceiveSetIPRequestAsync(
            ChangeIPAddressForm parent,
            string strLocalIPAddress, string strCurrentIPAddress,
            string strUser, string strPasswd, string strSerialNo,
            ref string strNewIP, string strNewSubnet)
        {
            SendSetIpRequest(parent, strLocalIPAddress, strCurrentIPAddress, strUser, strPasswd, strSerialNo, ref strNewIP, strNewSubnet);
            return true;
        }
        public bool SendAndReceiveSetIPRequest(
            string strLocalIPAddress, string strCurrentIPAddress,
            string strUser, string strPasswd, string strSerialNo,
            ref string strNewIP, string strNewSubnet)
        {
            int timeout = 20000;

            byte[] arrUser = Encoding.Default.GetBytes(strUser);
            byte[] arrPasswd = Encoding.Default.GetBytes(strPasswd);
            byte[] arrSerialNo = Encoding.Default.GetBytes(strSerialNo);
            byte[] arrIPAddress = Encoding.Default.GetBytes(strNewIP);
            byte[] arrSubnetMask = Encoding.Default.GetBytes(strNewSubnet);

            byte[] arrAdpPacket = new byte[256];

            arrAdpPacket[(byte)AdpHeader.VERSMAJOR] = PROTO_VERS_MAJOR;
            arrAdpPacket[(byte)AdpHeader.VERSMINOR] = PROTO_VERS_MINOR;
            arrAdpPacket[(byte)AdpHeader.REQUEST] = (byte)Requests.ADS_MMSREQ_SETIP;
            arrAdpPacket[(byte)AdpHeader.ACKNOWL] = 0;

            int nNextPos = (int)AdpHeader.ACKNOWL + 1;

            arrAdpPacket[nNextPos++] = (byte)Deviceinfo.ADSDPDU_TYPE_USERNAME;
            byte nUserLen = (byte)arrUser.Length;
            arrAdpPacket[nNextPos++] = nUserLen;
            Array.Copy(arrUser, 0, arrAdpPacket, nNextPos, nUserLen);

            nNextPos += nUserLen;

            byte nPasswdLen = (byte)arrPasswd.Length;
            arrAdpPacket[nNextPos++] = (byte)Deviceinfo.ADSDPDU_TYPE_PASSWD;
            arrAdpPacket[nNextPos++] = nPasswdLen;
            Array.Copy(arrPasswd, 0, arrAdpPacket, nNextPos, nPasswdLen);

            nNextPos += nPasswdLen;

            arrAdpPacket[nNextPos++] = (byte)Deviceinfo.ADSDPDU_TYPE_IPV4;

            if (((arrSubnetMask.Length == 1) && (arrSubnetMask[0] == 0)) || (strNewSubnet.Length == 0))
            {
                // Old format without subnet mask
                arrAdpPacket[nNextPos++] = IPV4_LEN_OLD;

                byte[] arrIPAddressBytes = IPAddress.Parse(strNewIP).GetAddressBytes();


                arrAdpPacket[nNextPos++] = arrIPAddressBytes[0];
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[1];
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[2];
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[3];
            }
            else
            {
                // New format with subnet mask
                int iIpLenOffset = nNextPos;
                arrAdpPacket[nNextPos++] = IPV4_LEN;

                byte[] arrIPAddressBytes = IPAddress.Parse(strNewIP).GetAddressBytes();

                arrAdpPacket[nNextPos++] = arrIPAddressBytes[0];
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[1];
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[2];
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[3];

                byte[] arrSubnetMaskBytes = IPAddress.Parse(strNewSubnet).GetAddressBytes();
                arrAdpPacket[nNextPos++] = arrSubnetMaskBytes[0];
                arrAdpPacket[nNextPos++] = arrSubnetMaskBytes[1];
                arrAdpPacket[nNextPos++] = arrSubnetMaskBytes[2];
                arrAdpPacket[nNextPos++] = arrSubnetMaskBytes[3];
            }
           

            //*********************************************************************************************
            // Send the target serial number of the device for which we want to set the new ip address
            //*********************************************************************************************
            arrAdpPacket[nNextPos++] = (byte)Deviceinfo.ADSDPDU_TYPE_SERIALNO;
            byte nSerNoLen = (byte)arrSerialNo.Length;
            arrAdpPacket[nNextPos++] = SERIALNO_LEN;
            Array.Copy(arrSerialNo, 0, arrAdpPacket, nNextPos, nSerNoLen);
            int diff = SERIALNO_LEN - nSerNoLen;

            // Device is expecting the serial number as fixed 16 bytes
            // We fill the remaining bytes with zero´s
            for (int i = 0; i < diff; i++)
                arrAdpPacket[nNextPos + nSerNoLen + i] = 0;
            nNextPos += SERIALNO_LEN;
            //***********************************************************************************

            //***********************************************************************************
            // Close the message with ending format
            //***********************************************************************************
            arrAdpPacket[nNextPos++] = (byte)Deviceinfo.ADSDPDU_TYPE_END;
            arrAdpPacket[nNextPos++] = 0;
            //***********************************************************************************

            UdpClient udpClient = new UdpClient();

            udpClient.Client.Bind(new IPEndPoint(IPAddress.Parse(strLocalIPAddress), 0));


            udpClient.Client.SendTimeout = timeout;
            udpClient.Client.ReceiveTimeout = timeout;

            //*********************************************************************************************
            // Currently SETIP command is only supported with broadcast in combination with serial number
            //*********************************************************************************************
            if (m_bSetIPOnlyOverBroadcast || strCurrentIPAddress.Equals("0.0.0.0"))
                udpClient.Send(arrAdpPacket, nNextPos, "255.255.255.255", UDP_PORT);
            else
                udpClient.Send(arrAdpPacket, nNextPos, strCurrentIPAddress, UDP_PORT);

            var from = new IPEndPoint(0, 0);


            try
            {
                var recvBuffer = udpClient.Receive(ref from);

                if (recvBuffer.Length > 0)
                {
                    if (receiveSetip(recvBuffer, ref strNewIP))
                        return true;
                }
            }
            catch (Exception ex)
            {
                // Receive failed 
                string strErr = ex.ToString();
                Trace.WriteLine("SendAndReceiveSetIPRequest> ERROR: " + strErr);
            }

            return false;
        }

        public bool sendAndReceiveDevInfo(string strLocalIPAddress, int timeout, ref string strSN, ref string strIP, ref string strSubnet)
        {
            byte[] arrAdpPacket = new byte[6];

            arrAdpPacket[(byte)AdpHeader.VERSMAJOR] = PROTO_VERS_MAJOR;
            arrAdpPacket[(byte)AdpHeader.VERSMINOR] = PROTO_VERS_MINOR;
            arrAdpPacket[(byte)AdpHeader.REQUEST] = (byte)Requests.ADS_MMSREQ_DEVINFO;
            arrAdpPacket[4] = 0;
            arrAdpPacket[5] = 0;

            UdpClient udpClient = new UdpClient();

            udpClient.Client.Bind(new IPEndPoint(IPAddress.Parse(strLocalIPAddress), 0));


            udpClient.Client.SendTimeout = timeout;
            udpClient.Client.ReceiveTimeout = timeout;
            
            udpClient.Send(arrAdpPacket, arrAdpPacket.Length, "255.255.255.255", UDP_PORT);

            var from = new IPEndPoint(0, 0);
            

            try
            {
                var recvBuffer = udpClient.Receive(ref from);

                if (recvBuffer.Length > 0)
                {
                    if (receiveDevinfo(recvBuffer, ref strSN, ref strIP, ref strSubnet))
                        return true;
                }
            }
            catch (Exception ex)
            {
                // Receive failed 
                string strErr = ex.ToString();
                Trace.WriteLine("sendAndReceiveDevInfo> ERROR: " + strErr);
            }
            
            return false;

        }
        public struct UdpState
        {
            public UdpClient udpClient;
            public IPEndPoint endPoint;
            public Form parent;
            public string localIPAddress;
        }
        

        public void ReceiveDevInfoRequestCallback(IAsyncResult ar)
        {
            UdpClient udpClient = ((UdpState)(ar.AsyncState)).udpClient;
            IPEndPoint endPoint = ((UdpState)(ar.AsyncState)).endPoint;

            string localIPAddress = ((UdpState)(ar.AsyncState)).localIPAddress;

            byte[] receiveBytes = udpClient.EndReceive(ar, ref endPoint);
            string receiveString = Encoding.ASCII.GetString(receiveBytes);

            Trace.WriteLine($"ReceiveDevInfoRequestCallback> Received: {receiveString}");

            if (receiveBytes.Length > 0)
            {
                string strSN = "", strIP = "", strSubnet = "";

                if (receiveDevinfo(receiveBytes, ref strSN, ref strIP, ref strSubnet))
                {
                    adsDetectionToolForm parent = (adsDetectionToolForm)((UdpState)(ar.AsyncState)).parent;
                    if (parent != null)
                    {
                        adstecDevice device = new adstecDevice(strSN, strIP, strSubnet, localIPAddress);
                        parent.AddDevice(device);
                    }
                }
            }
        }
        public void ReceiveSetIpRequestCallback(IAsyncResult ar)
        {
            UdpClient udpClient = ((UdpState)(ar.AsyncState)).udpClient;
            IPEndPoint endPoint = ((UdpState)(ar.AsyncState)).endPoint;

            string localIPAddress = ((UdpState)(ar.AsyncState)).localIPAddress;

            byte[] receiveBytes = udpClient.EndReceive(ar, ref endPoint);
            string receiveString = Encoding.ASCII.GetString(receiveBytes);

            Trace.WriteLine($"ReceiveSetIpRequestCallback> Received: {receiveString}");

            if (receiveBytes.Length > 0)
            {
                string strNewIP = "";

                if (receiveSetip(receiveBytes, ref strNewIP))
                {
                    ChangeIPAddressForm parent = (ChangeIPAddressForm)((UdpState)(ar.AsyncState)).parent;
                    if (parent != null)
                    {
                        //Inform parent 
                        parent.IpChangeOk(strNewIP);
                    }
                }
                else
                {
                    ChangeIPAddressForm parent = (ChangeIPAddressForm)((UdpState)(ar.AsyncState)).parent;
                    if (parent != null)
                    {
                        //Inform parent 
                        parent.IpChangeError(receiveString);
                    }
                }
            }
        }
        
        public void SendSetIpRequest(ChangeIPAddressForm parent,
            string strLocalIPAddress, string strCurrentIPAddress,
            string strUser, string strPasswd, string strSerialNo,
            ref string strNewIP, string strNewSubnet)
        {
            int timeout = 5000;

            byte[] arrUser = Encoding.Default.GetBytes(strUser);
            byte[] arrPasswd = Encoding.Default.GetBytes(strPasswd);
            byte[] arrSerialNo = Encoding.Default.GetBytes(strSerialNo);
            byte[] arrIPAddress = Encoding.Default.GetBytes(strNewIP);
            byte[] arrSubnetMask = Encoding.Default.GetBytes(strNewSubnet);

            byte[] arrAdpPacket = new byte[256];

            arrAdpPacket[(byte)AdpHeader.VERSMAJOR] = PROTO_VERS_MAJOR;
            arrAdpPacket[(byte)AdpHeader.VERSMINOR] = PROTO_VERS_MINOR;
            arrAdpPacket[(byte)AdpHeader.REQUEST] = (byte)Requests.ADS_MMSREQ_SETIP;
            arrAdpPacket[(byte)AdpHeader.ACKNOWL] = 0;

            int nNextPos = (int)AdpHeader.ACKNOWL + 1;

            arrAdpPacket[nNextPos++] = (byte)Deviceinfo.ADSDPDU_TYPE_USERNAME;
            byte nUserLen = (byte)arrUser.Length;
            arrAdpPacket[nNextPos++] = nUserLen;
            Array.Copy(arrUser, 0, arrAdpPacket, nNextPos, nUserLen);

            nNextPos += nUserLen;

            byte nPasswdLen = (byte)arrPasswd.Length;
            arrAdpPacket[nNextPos++] = (byte)Deviceinfo.ADSDPDU_TYPE_PASSWD;
            arrAdpPacket[nNextPos++] = nPasswdLen;
            Array.Copy(arrPasswd, 0, arrAdpPacket, nNextPos, nPasswdLen);

            nNextPos += nPasswdLen;
            
            arrAdpPacket[nNextPos++] = (byte)Deviceinfo.ADSDPDU_TYPE_IPV4;

            if (((arrSubnetMask.Length == 1) && (arrSubnetMask[0] == 0)) || (strNewSubnet.Length == 0))
            {
                // Old format without subnet mask
                arrAdpPacket[nNextPos++] = IPV4_LEN_OLD;
                
                byte[] arrIPAddressBytes = IPAddress.Parse(strNewIP).GetAddressBytes();


                arrAdpPacket[nNextPos++] = arrIPAddressBytes[0];
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[1];
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[2];
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[3];
            }
            else
            {
                // New format with subnet mask
                int iIpLenOffset = nNextPos;
                arrAdpPacket[nNextPos++] = IPV4_LEN;
               
                byte[] arrIPAddressBytes = IPAddress.Parse(strNewIP).GetAddressBytes();
                
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[0];
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[1];
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[2];
                arrAdpPacket[nNextPos++] = arrIPAddressBytes[3];
                
                byte[] arrSubnetMaskBytes = IPAddress.Parse(strNewSubnet).GetAddressBytes();
                arrAdpPacket[nNextPos++] = arrSubnetMaskBytes[0];
                arrAdpPacket[nNextPos++] = arrSubnetMaskBytes[1];
                arrAdpPacket[nNextPos++] = arrSubnetMaskBytes[2];
                arrAdpPacket[nNextPos++] = arrSubnetMaskBytes[3];
            }
            
            //*********************************************************************************************
            // Send the target serial number of the device for which we want to set the new ip address
            //*********************************************************************************************
            arrAdpPacket[nNextPos++] = (byte)Deviceinfo.ADSDPDU_TYPE_SERIALNO;
            byte nSerNoLen = (byte)arrSerialNo.Length;
            arrAdpPacket[nNextPos++] = SERIALNO_LEN;
            Array.Copy(arrSerialNo, 0, arrAdpPacket, nNextPos, nSerNoLen);
            int diff = SERIALNO_LEN - nSerNoLen;

            // Device is expecting the serial number as fixed 16 bytes
            // We fill the remaining bytes with zero´s
            for (int i = 0; i < diff; i++)
                arrAdpPacket[nNextPos + nSerNoLen+i] = 0;
            nNextPos += SERIALNO_LEN;
            //*********************************************************************************************

            //***********************************************************************************
            // Close the message with ending format
            //***********************************************************************************
            arrAdpPacket[nNextPos++] = (byte)Deviceinfo.ADSDPDU_TYPE_END;
            arrAdpPacket[nNextPos++] = 0;
            //***********************************************************************************

            UdpClient udpClient = new UdpClient();

            IPEndPoint e = new IPEndPoint(IPAddress.Parse(strLocalIPAddress), 0);
            udpClient.Client.Bind(e);


            udpClient.Client.SendTimeout = timeout;
            udpClient.Client.ReceiveTimeout = timeout;

            //*********************************************************************************************
            // Currently SETIP command is only supported with broadcast in combination with serial number
            //*********************************************************************************************
            if (m_bSetIPOnlyOverBroadcast || strCurrentIPAddress.Equals("0.0.0.0"))
                udpClient.Send(arrAdpPacket, nNextPos, "255.255.255.255", UDP_PORT);
            else
                udpClient.Send(arrAdpPacket, nNextPos, strCurrentIPAddress, UDP_PORT);

            UdpState udpState = new UdpState();
            udpState.endPoint = e;
            udpState.udpClient = udpClient;
            udpState.parent = parent;
            udpState.localIPAddress = strLocalIPAddress;

            udpClient.BeginReceive(new AsyncCallback(ReceiveSetIpRequestCallback), udpState);
            return;
        }
        private void sendDevInfoRequest(adsDetectionToolForm parent, string strLocalIPAddress)
        {
            byte[] arrAdpPacket = new byte[6];
            
            arrAdpPacket[(byte)AdpHeader.VERSMAJOR] = PROTO_VERS_MAJOR;
            arrAdpPacket[(byte)AdpHeader.VERSMINOR] = PROTO_VERS_MINOR;
            arrAdpPacket[(byte)AdpHeader.REQUEST] = (byte)Requests.ADS_MMSREQ_DEVINFO;
            arrAdpPacket[4] = 0;
            arrAdpPacket[5] = 0;

            UdpClient udpClient = new UdpClient();

            IPEndPoint e = new IPEndPoint(IPAddress.Parse(strLocalIPAddress), 0);

            udpClient.Client.Bind(e);
            
            udpClient.Send(arrAdpPacket, arrAdpPacket.Length, "255.255.255.255", UDP_PORT);
            
            UdpState udpState = new UdpState();
            udpState.endPoint = e;
            udpState.udpClient = udpClient;
            udpState.parent = parent;
            udpState.localIPAddress = strLocalIPAddress;

            udpClient.BeginReceive(new AsyncCallback(ReceiveDevInfoRequestCallback), udpState);
            return;
        }

    }
}

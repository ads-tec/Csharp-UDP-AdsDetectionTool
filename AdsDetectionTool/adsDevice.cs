// Copyright (c) 2024, ads-tec IIT GmbH
// All rights reserved.
// This source code is licensed under the BSD 2-Clause License.
// Please see the LICENSE file in the root directory of this source tree for more details.

namespace adsDetectionTool
{
    public class adstecDevice
    {
        string m_serialNumber;
        string m_ipAddressDevice;
        string m_ipAddressLocal;
        string m_subnet;

        public adstecDevice(string strSerno, string strIP, string strSubnet, string localIPAddress)
        {
            m_serialNumber = strSerno;
            m_ipAddressDevice = strIP;
            m_subnet = strSubnet;
            m_ipAddressLocal = localIPAddress;
        }

        public string SerialNumber
        {
            get { return m_serialNumber; }
        }
        public string IPAddressDevice
        {
            get { return m_ipAddressDevice; }
        }
        public string IPAddressLocal
        {
            get { return m_ipAddressLocal; }
        }
        public string SubnetMask
        {
            get { return m_subnet; }
        }
    }
}

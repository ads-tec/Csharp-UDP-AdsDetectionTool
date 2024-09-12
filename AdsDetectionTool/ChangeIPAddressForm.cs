// Copyright (c) 2024, ads-tec IIT GmbH
// All rights reserved.
// This source code is licensed under the BSD 2-Clause License.
// Please see the LICENSE file in the root directory of this source tree for more details.
namespace adsDetectionTool
{
    public partial class ChangeIPAddressForm : Form
    {
        string m_strNewIP = "";
        string m_strNewSubnet = "";
        string m_strLocalIP = "";
        bool m_bAsyncMode = true;

        public ChangeIPAddressForm()
        {
            InitializeComponent();

            btnChange.Enabled = false;
        }
        public void SetData(string strUser, string strPwd, string strSerialNo, string strLocalIP, string strCurrentIP, string strCurrentSubnet, bool bAsyncMode)
        {
            m_strLocalIP = strLocalIP;
            m_bAsyncMode = bAsyncMode;
            this.textBoxPwd.Text = strPwd;
            this.textBoxUser.Text = strUser;
            this.textSerialNo.Text = strSerialNo;
            this.tbCurrentIPAddress.Text = strCurrentIP;      
            this.tbCurrentSubnetMask.Text = strCurrentSubnet;
        }

        public void IpChangeOk(string strNewIP)
        {
            this.Invoke((MethodInvoker)delegate
            {
                btnChange.Text = "Set new ip to " + strNewIP + "!";
                btnChange.Enabled = true;
            });
        }
        public void IpChangeError(string strError)
        {
            this.Invoke((MethodInvoker)delegate
            {
                btnChange.Text = "Error: " + strError;
                btnChange.Enabled = true;
            });
        }
       
        private void resetChangeButtonState()
        {
            btnChange.Text = "Change";
            btnChange.Enabled = true;
        }
        private void btnChange_Click(object sender, EventArgs e)
        {
            m_strNewIP = this.tbNewIPAddress.Text;
            m_strNewSubnet = this.tbNewSubnetMask.Text;

            btnChange.Enabled = false;
            btnChange.Text = "Please wait...";

            AdsDetectionProtocol adp = new AdsDetectionProtocol();

            string strCurrentIP = this.tbCurrentIPAddress.Text;
            string strNewSubnet = this.tbNewSubnetMask.Text;

            string strNewIP = m_strNewIP;

            bool bResult = false;

            if (textSerialNo.Text.Length > 16)
            {
                MessageBox.Show("Max. size serial number is 16!");
                resetChangeButtonState();
                return;
            }

            if (m_bAsyncMode)
            {
                //***********************************************************************************
                // Async methode: After sending our request, our callback function will be called, as soon as data is available
                //***********************************************************************************
                bResult = adp.SendAndReceiveSetIPRequestAsync(this,
                                            m_strLocalIP, strCurrentIP,
                                            textBoxUser.Text, textBoxPwd.Text, textSerialNo.Text,
                                            ref strNewIP, strNewSubnet);
            }
            else
            {
                //***********************************************************************************
                // Sync methode: After sending our request, we call receive function in blocking mode with some timeout
                //***********************************************************************************
                bResult = adp.SendAndReceiveSetIPRequest(
                                            m_strLocalIP, strCurrentIP,
                                            textBoxUser.Text, textBoxPwd.Text, textSerialNo.Text,
                                            ref strNewIP, strNewSubnet);
            }
            
            if (m_bAsyncMode)
            {
                btnChange.Text = "SETIP command sent! Waiting for response...";
                btnChange.Enabled = true;
            }
            else
            {
                if (bResult)
                {
                    m_strNewIP = strNewIP;
                    btnChange.Text = "IP set!...";
                    btnChange.Enabled = true;
                }
                else
                {
                    btnChange.Text = "Error occured!...";
                    btnChange.Enabled = true;
                }
            }
        }

        private bool isValidIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        private void checkIPFields()
        {
            string strNewIP = this.tbNewIPAddress.Text;
            string strNewSubnet = this.tbNewSubnetMask.Text;

            if (isValidIPv4(strNewIP) && isValidIPv4(strNewSubnet))
                btnChange.Enabled = true;
        }
        private void tbNewIPAddress_TextChanged(object sender, EventArgs e)
        {
            checkIPFields();
        }

        private void tbNewSubnetMask_TextChanged(object sender, EventArgs e)
        {
            checkIPFields();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

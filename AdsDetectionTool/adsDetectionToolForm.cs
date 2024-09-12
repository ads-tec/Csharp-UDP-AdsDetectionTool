// Copyright (c) 2024, ads-tec IIT GmbH
// All rights reserved.
// This source code is licensed under the BSD 2-Clause License.
// Please see the LICENSE file in the root directory of this source tree for more details.

namespace adsDetectionTool
{
    
    public partial class adsDetectionToolForm : Form
    {

        List<adstecDevice> m_adsDevices = new List<adstecDevice>();
        bool m_bUseAsyncMode = true;//false;

        public adsDetectionToolForm()
        {
            InitializeComponent();

            radioButtonSyncMode.Checked = false;
            radioButtonAsyncMode.Checked = true;
        }
        public int ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 500;
            prompt.Height = 100;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            NumericUpDown inputBox = new NumericUpDown() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.ShowDialog();
            return (int)inputBox.Value;
        }
        private string getLocalIPAddresFromSerialNumber(string strSerNo)
        {
            foreach (adstecDevice dev in m_adsDevices)
            {
                if (dev.SerialNumber.Equals(strSerNo))
                {
                    // This device exists in the detected device list
                    // Return local ip address
                    return dev.IPAddressLocal;
                }
            }
            return "";
        }
        private void ChangeIPAddressClick(object sender)
        {
            ListView ListViewControl = sender as ListView;

            foreach (ListViewItem tmpLstView in ListViewControl.SelectedItems)
            {
                Console.WriteLine(tmpLstView.Text);
                
                ChangeIPAddressForm prompt = new ChangeIPAddressForm();

                string strUser = "admin";
                string strPwd = "admin";
                string strSerNo = tmpLstView.SubItems[0].Text;
                string strCurrentIP = tmpLstView.SubItems[1].Text;
                string strCurrentSubnet = tmpLstView.SubItems[2].Text;

                prompt.SetData(strUser, strPwd, strSerNo, getLocalIPAddresFromSerialNumber(strSerNo), strCurrentIP, strCurrentSubnet, m_bUseAsyncMode);
                prompt.ShowDialog();
            }

        }
        private void lvFoundDevices_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lvFoundDevices.FocusedItem != null && lvFoundDevices.FocusedItem.Bounds.Contains(e.Location))
                {
                    ContextMenuStrip m = new ContextMenuStrip();
                    ToolStripMenuItem changeIPAddressMenuItem = new ToolStripMenuItem("Change IP Address");
                    changeIPAddressMenuItem.Click += (sender2, e2) => ChangeIPAddressClick(sender);
                    m.Items.Add(changeIPAddressMenuItem);

                    m.Show(lvFoundDevices, e.Location);
                }
            }
        }

        private void btnChangeIPAddress_Click(object sender, EventArgs e)
        {
            ChangeIPAddressClick(lvFoundDevices);
        }

        private void lvFoundDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFoundDevices.SelectedItems.Count < 1)
            {
                btnChangeIPAddress.Enabled = false;
                return;
            }

            if (lvFoundDevices.FocusedItem != null)
            {
                btnChangeIPAddress.Enabled = true;
            }
            else
            {
                btnChangeIPAddress.Enabled = false;
            }

        }

        private bool existsInDeviceList(adstecDevice device)
        {
            foreach (adstecDevice dev in m_adsDevices)
            {
                if (device.SerialNumber.Equals(dev.SerialNumber))
                {
                    // This device exists already in the detected device list
                    return true;
                }
            }
            return false;
        }

        public void AddDevice(adstecDevice device)
        {
            // Check if this device already exists in the detected device list

            if (existsInDeviceList(device))
            {
                // This device exists already in the detected device list
                return;
            }

            m_adsDevices.Add(device);

            var dev2Add = new ListViewItem(new[] { device.SerialNumber, device.IPAddressDevice, device.SubnetMask, device.IPAddressLocal });
            
            this.Invoke((MethodInvoker) delegate
                {
                    lvFoundDevices.Items.Add(dev2Add);
                });
            
        }

        

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnChangeIPAddress.Enabled = false;
            AdsDetectionProtocol adp = new AdsDetectionProtocol();

            clearList();

            if (m_bUseAsyncMode)
            {
                //
                // Asyncron mode is active
                // We use callback function for receive operation
                // The callback function adds the found device to the list
                //
                adp.DetectAdsDeviceAsync(this);
            }   
            else
            {
                //
                // Syncron mode is active
                // We use blocking mode udp sockets with timeout in order not to block indefinetely
                //
                List<adstecDevice> adsDevices = new List<adstecDevice>();

                adp.DetectAdsDevice(ref adsDevices);

                foreach (adstecDevice device in adsDevices)
                {
                    if (existsInDeviceList(device))
                        continue;

                    m_adsDevices.Add(device);

                    var dev = new ListViewItem(new[] { device.SerialNumber, device.IPAddressDevice, device.SubnetMask, device.IPAddressLocal });
                    lvFoundDevices.Items.Add(dev);
                }
            }
        }
        private void clearList()
        {
            m_adsDevices.Clear();
            lvFoundDevices.Items.Clear();
        }
    
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnChangeIPAddress.Enabled = false;
            clearList();
        }


        private void radioButtonAsyncMode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAsyncMode.Checked)
            {
                radioButtonSyncMode.Checked = false;
                m_bUseAsyncMode = true;
            }
        }

        private void radioButtonSyncMode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSyncMode.Checked)
            {
                radioButtonAsyncMode.Checked = false;
                m_bUseAsyncMode = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    
}

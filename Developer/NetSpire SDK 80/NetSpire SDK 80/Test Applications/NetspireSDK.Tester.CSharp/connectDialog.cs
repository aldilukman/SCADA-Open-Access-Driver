using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetspireSDKTester
{
    public partial class connectDialog : Form
    {
        public String serverAddressStr = "";
        public Boolean connectToCXSServer = true;
        public String endPtPortNoStr = "";
        public String hostDestinationListStr = "";
        public String logLevel = "1";
        public String debugFileName = "sdkDebug.log";
        public String errorFileName = "sdkError.log";
        public Boolean okButtonClicked = false;

        /*******************************************************************************************************/
        public connectDialog()
        {
            InitializeComponent();
            this.okButtonClicked = false;
            this.textBoxPortNo.Text = "20770";
            this.listBoxServerType.Text = "CXS DVA Server";
            this.comboBoxLogLevel.Text = "1";
            this.textBoxDebugFileName.Text = "sdkDebug.log";
            this.textBoxErrorFileName.Text = "sdkError.log";
        }

        /*******************************************************************************************************/
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.serverAddressStr = this.textBoxServerAddressList.Text;
            this.connectToCXSServer = this.listBoxServerType.Text == "CXS DVA Server" ? true : false;
            this.endPtPortNoStr = this.textBoxPortNo.Text;
            this.hostDestinationListStr = this.textBoxHostDestinationList.Text;
            this.logLevel = this.comboBoxLogLevel.Text;
            this.debugFileName = this.textBoxDebugFileName.Text;
            this.errorFileName = this.textBoxErrorFileName.Text;
            this.okButtonClicked = true;
            this.Close();
        }

        /*******************************************************************************************************/
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

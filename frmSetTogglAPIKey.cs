using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Environment = System.Environment;

namespace QuickToggl
{
    public partial class frmSetTogglAPIKey : Form
    {
        private bool isThereAKeySaved = false;

        public frmSetTogglAPIKey()
        {
            InitializeComponent();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.toggl.com/app/profile");
            linkLabel1.LinkVisited = true;
        }

        private void tmrSaveColorFeedback_Tick(object sender, EventArgs e)
        {
            tmrSaveColorFeedback.Enabled = false;
            btnSave.BackColor = DefaultBackColor;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.SetEnvironmentVariable("TOGGL_COM_API_KEY", txtKey.Text.Trim(), EnvironmentVariableTarget.User);

                if (Environment.GetEnvironmentVariable("TOGGL_COM_API_KEY", EnvironmentVariableTarget.User).Length == 0)
                {
                    throw new Exception("Blank key saved - fill in your key based on what your Toggl.com profile says.");
                }

                btnSave.BackColor = Color.Green;
                tmrSaveColorFeedback.Enabled = true;
                isThereAKeySaved = true;
            }
            catch (Exception exception)
            {
                btnSave.BackColor = Color.Red;
                tmrSaveColorFeedback.Enabled = true;
            }
        }

        private void frmSetTogglAPIKey_Load(object sender, EventArgs e)
        {
            txtKey.Text = Environment.GetEnvironmentVariable("TOGGL_COM_API_KEY", EnvironmentVariableTarget.User);
            if (txtKey.Text.Trim().Length > 0)
            {
                isThereAKeySaved = true;
            }
        }

        private void frmSetTogglAPIKey_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isThereAKeySaved)
            {
                this.DialogResult = DialogResult.No;
            }
        }
    }
}

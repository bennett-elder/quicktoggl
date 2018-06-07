using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickToggl;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string key = txtKey.Text.Trim();
                KeyVerification.VerifyKeyAndSaveIt(key);
                lblSaveFeedback.ForeColor = Color.Green;
                lblSaveFeedback.Text = "Key saved.";
                isThereAKeySaved = true;
            }
            catch (Exception exception)
            {
                lblSaveFeedback.ForeColor = Color.Red;
                lblSaveFeedback.Text = exception.Message;
            }
        }

        private void frmSetTogglAPIKey_Load(object sender, EventArgs e)
        {
            try
            {
                txtKey.Text = Environment.GetEnvironmentVariable("TOGGL_COM_API_KEY", EnvironmentVariableTarget.User);
                KeyVerification.VerifyKeyAndSaveIt(txtKey.Text);
                lblSaveFeedback.ForeColor = Color.Green;
                lblSaveFeedback.Text = "Key is verified.";
                isThereAKeySaved = true;
            }
            catch (Exception exception)
            {
                lblSaveFeedback.ForeColor = Color.Red;
                lblSaveFeedback.Text = "Invalid key or issue saving it.";
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

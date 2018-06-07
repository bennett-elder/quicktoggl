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
                VerifyKeyAndSaveIt(key);
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

        private static void VerifyKeyAndSaveIt(string key)
        {
            var toggl = new Toggl.Api.TogglClient(key);
            var request = toggl.Users.GetCurrent();
            if (request.IsCompleted)
            {
                if (request.IsFaulted)
                {
                    if (request.Exception.InnerExceptions != null)
                    {
                        if (request.Exception.InnerException.Message ==
                            "The remote server returned an error: (403) Forbidden.")
                        {
                            throw new Exception("Bad API key");
                        }

                        throw request.Exception.InnerException;
                    }
                    else
                    {
                        throw request.Exception;
                    }
                }

                Environment.SetEnvironmentVariable("TOGGL_COM_API_KEY", key, EnvironmentVariableTarget.User);

                if (Environment.GetEnvironmentVariable("TOGGL_COM_API_KEY", EnvironmentVariableTarget.User).Length == 0)
                {
                    throw new Exception("Blank key stored in environment variable - fill in your key based on what your Toggl.com profile says.");
                }
            }
        }

        private void frmSetTogglAPIKey_Load(object sender, EventArgs e)
        {
            try
            {
                txtKey.Text = Environment.GetEnvironmentVariable("TOGGL_COM_API_KEY", EnvironmentVariableTarget.User);
                VerifyKeyAndSaveIt(txtKey.Text);
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickToggl
{
    public partial class frmInputRequest : Form
    {
        public string TextResult { get; set; }
        private bool IsRequired { get; set; }

        public frmInputRequest(string prompt = "Requested input", string defaultValue = "", string title = "Requested input", bool isRequired = false)
        {
            InitializeComponent();
            lblPrompt.Text = prompt;
            txtInput.Text = defaultValue;
            this.Text = title;
            IsRequired = isRequired;
        }

        private bool RequiredButEmpty => IsRequired && txtInput.Text.Trim().Length == 0;

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (RequiredButEmpty)
            {
                return;
            }
            this.TextResult = txtInput.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (RequiredButEmpty)
            {
                return;
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmInputRequest_Load(object sender, EventArgs e)
        {

        }
    }
}

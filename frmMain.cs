using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using QuickToggl;

namespace TogglPlus
{
    public partial class frmMain : Form
    {
        private InteractWithToggl _iwt;
        private ButtonOptionList _buttonDefns;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!VerifyWeHaveAnAPIKey())
            {
                this.Close();
                return;
            }
            _iwt = new InteractWithToggl();

            _buttonDefns = new ButtonOptionList();

            foreach (var defn in _buttonDefns.Buttons)
            {
                var btn = new Button
                {
                    Text = defn.Text,
                    BackColor = defn.BackColor,
                    Size = new Size(150, 75)
                };
                btn.Click += new System.EventHandler(this.button_Click);
                flowLayoutPanel1.Controls.Add(btn);
            }
        }

        private bool VerifyWeHaveAnAPIKey()
        {
            string key = Environment.GetEnvironmentVariable("TOGGL_COM_API_KEY", EnvironmentVariableTarget.User);
            if (key == null || key.Trim().Length == 0)
            {
                var setKeyForm = new frmSetTogglAPIKey();
                var result = setKeyForm.ShowDialog();
                if (result == DialogResult.No)
                {
                    return false;
                }
            }

            return true;
        }

        private void button_Click(object sender, EventArgs e)
        {
            var button = (Button) sender;
            var matchingButtonDefn = _buttonDefns.Buttons.FirstOrDefault(x => x.Text == button.Text);

            if (matchingButtonDefn != null)
            {
                string input = "";

                if (matchingButtonDefn.RequiresInput)
                {
                    while (input.Length == 0)
                    {
                        input = Microsoft.VisualBasic.Interaction.InputBox($"Task type {matchingButtonDefn.Text} REQUIRES input:",
                            "Required Input",
                            input);
                    }
                }
                else if (matchingButtonDefn.RecommendsInput)
                {
                    input = Microsoft.VisualBasic.Interaction.InputBox($"Task type {matchingButtonDefn.Text} RECOMMENDS input:",
                        "Recommended Input",
                        input);
                    
                }

                var newTaskName = input.Length > 0 ? $"{matchingButtonDefn.Text} {input}" : matchingButtonDefn.Text;

                _iwt.StopCurrentTask();
                if (matchingButtonDefn.Text == "Stop" && matchingButtonDefn.Project == "")
                {
                    // this is the stop button
                    return;
                }
                _iwt.StartNewTask(newTaskName, matchingButtonDefn.Project);
            }
        }
    }
}

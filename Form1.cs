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

namespace TogglPlus
{
    public partial class Form1 : Form
    {
        private InteractWithToggl iwt;
        private ButtonOptionList buttonDefns;
        public Form1()
        {
            InitializeComponent();

            buttonDefns = new ButtonOptionList();

            foreach (var defn in buttonDefns.Buttons)
            {
                var btn = new Button();
                btn.Text = defn.Text;
                btn.BackColor = defn.BackColor;
                btn.Size = new Size(150, 75);
                btn.Click += new System.EventHandler(this.button_Click);
                flowLayoutPanel1.Controls.Add(btn);
            }

            iwt = new InteractWithToggl();
        }

        private void button_Click(object sender, EventArgs e)
        {
            var button = (Button) sender;
            var matchingButtonDefn = buttonDefns.Buttons.FirstOrDefault(x => x.Text == button.Text);

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

                iwt.StopCurrentTask();
                iwt.StartNewTask(newTaskName, matchingButtonDefn.Project);
            }
        }
    }
}

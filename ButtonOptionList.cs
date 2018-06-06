using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TogglPlus
{
    class ButtonOptionList
    {
        public List<ButtonOption> Buttons { get; private set; }

        public ButtonOptionList()
        {
            Buttons = new List<ButtonOption>();
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.LightGreen,
                RequiresInput = false,
                RecommendsInput = true,
                Text = "Sprint Task",
                Project = "Velocity"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.LightGreen,
                RequiresInput = false,
                Text = "Moving deploy closer to release (sprint)",
                Project = "Velocity"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.LightGreen,
                RequiresInput = false,
                Text = "PSP",
                Project = "Velocity"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.LightGreen,
                RequiresInput = false,
                Text = "Official Triage Meeting",
                Project = "Velocity"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.LightGreen,
                RequiresInput = false,
                Text = "Kickoff",
                Project = "Velocity"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.LightGreen,
                RequiresInput = false,
                Text = "Standup",
                Project = "Velocity"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.LightGreen,
                RequiresInput = false,
                Text = "Misc sprint-related meeting",
                Project = "Velocity"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.Orange,
                RequiresInput = true,
                Text = "Ticket",
                Project = "Maintenance"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.Orange,
                RequiresInput = false,
                Text = "Moving deploy closer to release (ticket)",
                Project = "Maintenance"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.DodgerBlue,
                RequiresInput = false,
                Text = "Company meeting",
                Project = "Common"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.DodgerBlue,
                RequiresInput = false,
                Text = "Dept meeting",
                Project = "Common"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.DodgerBlue,
                RequiresInput = false,
                Text = "Misc meeting",
                Project = "Common"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.MediumPurple,
                RequiresInput = false,
                Text = "Building teams",
                Project = "Common"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.MediumPurple,
                RequiresInput = false,
                Text = "Building tools",
                Project = "Common"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.MediumPurple,
                RequiresInput = false,
                Text = "Building docs",
                Project = "Common"
            });
            Buttons.Add(new ButtonOption()
            {
                BackColor = Color.Red,
                RequiresInput = false,
                Text = "Stop",
                Project = ""
            });
        }
    }
}
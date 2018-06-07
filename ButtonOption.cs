using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickToggl
{
    class ButtonOption
    {
        public string Text { get; set; }
        public string Project { get; set; }
        public Color BackColor { get; set; }
        public bool RequiresInput { get; set; }
        public bool RecommendsInput { get; set; }
        public int Order { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FairyStar
{
    public partial class GUI : Form
    {

        public PlayArea Play;
        public KeyTask KeyIN;
        public GUI()
        {
            InitializeComponent();
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true); //더블버퍼링
            UpdateStyles();
            SetBounds(0, 0, Config.wWidth, Config.wHeight);
            Config.pWidth = PlayArea_Panel.Width;
            Config.pHeight = PlayArea_Panel.Height;
            Res.window = this;


            Play = new PlayArea(this);
            Play.Init();
        }

        private void PlayArea_Panel_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                KeyIN = new PlayListener(Play);
            }
            else
            {
                Play = null;
            }
        }
    }
}

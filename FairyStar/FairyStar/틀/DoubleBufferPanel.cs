using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FairyStar
{
    public class DoubleBufferPanel : Panel
    {
        public DoubleBufferPanel() : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            UpdateStyles();
        }
    }
}

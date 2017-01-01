using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyStar
{
    public class AnimateForm
    {
        public int x, y, width, height;
        public double oriX, oriY, speedX, speedY, accelX, accelY;
        public Point core = new Point();
        public AnimateForm() { }
    }
}

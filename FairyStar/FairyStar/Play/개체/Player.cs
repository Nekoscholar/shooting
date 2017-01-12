using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyStar
{
    public class Player : AnimateCircle
    {
        public int HP_MAX = 100, MP_MAX = 100, HP= 100, MP = 100;

        public Player(float a, float b, float r, int w, int h) : base(a, b, r, w, h)
        {

        }

        public void impact(int dm)
        {
            HP -= dm;
        }
    }
}
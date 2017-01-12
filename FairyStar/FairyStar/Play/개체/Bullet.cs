using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FairyStar
{
    public class Bullet : AnimateCircle
    {
        public int Damage = 10;
        public int Type = 1;
        public int Action = 1;
        public int Team = 2;        //2는 적, 1은 아군
        public bool REMOVE = false;
        public Timer TASK, TASK2;

        public Bullet(float a, float b, float r, int w, int h) : base(a, b, r, w, h)
        {

        }

        public void Init()
        {
            //TASK = new Timer(MOVE, null, 0, 15);
            TASK2 = new Timer(check_outArea, null, 0, 1000);
        }

        public void MOVE(object obj)
        {
            if (!REMOVE)
            {
                x += speedX;
                y += speedY;
                speedX += accelX;
                speedY += accelY;
            }
        }

        public void paintDo(Graphics g, Pen p, Brush b)
        {
            Res.DrawDefaultBullet(g, p, b, x, y, width, height, radius, Team);
        }
        
        public void check_outArea(object obj)
        {
            if (Action == 0 && (x - width > Res.window.Play.areaWidth && speedX >= 0 && accelX >= 0) ||
                            (x + width < 0 && speedX <= 0 && accelX <= 0) ||
                            (y - height > Res.window.Play.areaHeight && speedY >= 0 && accelY >= 0) ||
                            (y + height < 0 && speedY <= 0 && accelY <= 0))
            {
                REMOVE = true;
            }
            else
                REMOVE = false;
        }

        public void Dispose()
        {
            TASK.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            TASK.Dispose(); TASK = null;
        }
    }
}

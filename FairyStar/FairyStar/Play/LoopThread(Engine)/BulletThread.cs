using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FairyStar
{
    public class BulletThread
    {
        public static int ThreadNum = 1;
        PlayArea Play;

        Timer[] Move_Bullet = new Timer[ThreadNum];
        public BulletThread(PlayArea obj)
        {
            Play = obj;
        }

        public void Init()
        {
            //for (int i = 0; i < ThreadNum; i++)
            //{
            //    Move_Bullet[i] = new Timer(TASK, i, 0, 15);
            //}
        }

        public void TASK(object obj)
        {
            for(int i = (int)obj; i < Play.Object_Bullet.Count; i += ThreadNum)
            {
                try
                {
                    Bullet B = Play.Object_Bullet.ElementAt(i);
                    B.x += B.speedX;
                    B.y += B.speedY;
                    B.speedX += B.accelX;
                    B.speedY += B.accelY;
                    if (B.Action == 0 && (B.x > Res.window.Play.areaWidth && B.speedX >= 0 && B.accelX >= 0) ||
                                    (B.x + B.width < 0 && B.speedX <= 0 && B.accelX <= 0) ||
                                    (B.y > Res.window.Play.areaHeight && B.speedY >= 0 && B.accelY >= 0) ||
                                    (B.y + B.height < 0 && B.speedY <= 0 && B.accelY <= 0))
                    {
                        Res.window.Play.Object_Bullet.RemoveAt(i);
                    }
                }
                catch(Exception e)
                {
                    continue;
                }
            }
        }
    }

    public class Move_Bullet_Task
    {

    }
}

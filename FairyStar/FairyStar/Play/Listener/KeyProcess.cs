using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FairyStar
{
    public class KeyProcess
    {
        public PlayListener MASTER;
        public KeyProcess(PlayListener obj)
        {
            MASTER = obj;
        }
        
        private Timer MoveTimer;
        public bool onMoving = false;
        public void Move()
        {
            if (!onMoving) {
                onMoving = true;
                MoveTimer = new Timer(MoveTask, null, 0, 15);
            }
        }
        
        private static int moveTerm = 24;       //성능을 김치에 싸서 드셔보세요
        public void MoveTask(object data)
        {
            if (MASTER.KEY_UP)
                MASTER.Play.Object_Circle.ElementAt(0).y = (MASTER.Play.Object_Circle.ElementAt(0).y - moveTerm >= 0) ? MASTER.Play.Object_Circle.ElementAt(0).y - moveTerm : 0;

            if (MASTER.KEY_DOWN)
                MASTER.Play.Object_Circle.ElementAt(0).y = (MASTER.Play.Object_Circle.ElementAt(0).y + moveTerm <= MASTER.Play.areaHeight) ? MASTER.Play.Object_Circle.ElementAt(0).y + moveTerm : MASTER.Play.areaHeight;

            if (MASTER.KEY_LEFT)
                MASTER.Play.Object_Circle.ElementAt(0).x = (MASTER.Play.Object_Circle.ElementAt(0).x - moveTerm >= 0) ? MASTER.Play.Object_Circle.ElementAt(0).x - moveTerm : 0;

            if (MASTER.KEY_RIGHT)
                MASTER.Play.Object_Circle.ElementAt(0).x = (MASTER.Play.Object_Circle.ElementAt(0).x + moveTerm <= MASTER.Play.areaWidth) ? MASTER.Play.Object_Circle.ElementAt(0).x + moveTerm : MASTER.Play.areaWidth;

            if (!(MASTER.KEY_UP || MASTER.KEY_DOWN || MASTER.KEY_LEFT || MASTER.KEY_RIGHT))
            {
                MoveTimer.Dispose();
                MoveTimer = null;
                onMoving = false;
            }
        }
    }
}

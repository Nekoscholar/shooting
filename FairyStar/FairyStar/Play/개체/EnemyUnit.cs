using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FairyStar
{
    public class EnemyUnit : AnimateCircle
    {
        public List<EnemyAction> Action;
        public int ActionCycle = 6;
        public int Timing = 0;

        public int id;
        public int HP, HP_MAX, MP, MP_MAX;
        public TestAI AI;

        public EnemyUnit(float a, float b, float r, int w, int h) : base(a, b, r, w, h)
        {
            AI = new TestAI(this);
        }

        public void Init()
        {
            AI.Init();

        }

        public void paintDo(Graphics g, Pen p, Brush b)
        {
            Res.DrawDefaultUnit(g, p, b, this);
            base.paintDo(g, b);
        }
    }

    public class TestAI
    {
        EnemyUnit Owner;
        Timer TIME;
        public TestAI(EnemyUnit obj)
        {
            Owner = obj;
        }

        public void Init()
        {
            TIME = new Timer(TASK, null, 0, 15);
        }

        public void TASK(object obj)
        {
            //Owner.direct = (float)((Res.window.Play.TempPlayer.ElementAt(0).x - Owner.x < 0) ? 180 - Math.Asin((Res.window.Play.TempPlayer.ElementAt(0).y - Owner.y) / Owner.distance(Res.window.Play.TempPlayer.ElementAt(0))) * 180 / Math.PI : Math.Asin((Res.window.Play.TempPlayer.ElementAt(0).y - Owner.y) / Owner.distance(Res.window.Play.TempPlayer.ElementAt(0))) * 180 / Math.PI);
            Owner.direct += 3.01f;
            //if (Owner.Timing == 0)
                Res.window.Play.Shot(Owner, 1);
            Owner.Timing = (++Owner.Timing) % Owner.ActionCycle;
        }
    }
}

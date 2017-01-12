using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyStar
{
    public class EnemyPosition      //몹 등장 타이밍, 몹 종류와 몹 최초위치를 지정
    {
        public int Time;
        public int Unit;
        public float x, y;
    }

    public class EnemyAction        //몹이 각 타이밍에 할 행동.
    {
        public int Time;
        public int Action;
    }
}

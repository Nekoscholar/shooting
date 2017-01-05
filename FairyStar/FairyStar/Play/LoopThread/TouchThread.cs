using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FairyStar
{
    public class TouchThread
    {
        public static int ThreadNum = 16;  //피탄 스레드 분산. 현재 아래 코드들은 이름 빼고 임시 코드이므로, 추후에 적탄/적레이저/적유닛, 아군탄/아군레이저/아군유닛 클래스 작성 밑 분류가 끝나면 재구성할것.

        Touch_Player_EnemyBullet_Task[] TS1 = new Touch_Player_EnemyBullet_Task[ThreadNum];
        Thread[] Touch_Player_EnemyBullet = new Thread[ThreadNum];
        Touch_Player_EnemyLaser_Task[] TS2 = new Touch_Player_EnemyLaser_Task[ThreadNum];
        Thread[] Touch_Player_EnemyLaser = new Thread[ThreadNum];

        PlayArea MASTER;
        public SyncTask SYNC;

        public TouchThread(PlayArea obj)
        {
            MASTER = obj;
            SYNC = new SyncTask(MASTER);
            
            for (int i = 0; i < ThreadNum; i++)
            {
                TS1[i] = new Touch_Player_EnemyBullet_Task(i, MASTER);
                TS2[i] = new Touch_Player_EnemyLaser_Task(i, MASTER);
                Touch_Player_EnemyBullet[i] = new Thread(TS1[i].TASK);
                Touch_Player_EnemyLaser[i] = new Thread(TS2[i].TASK);
            }
        }
        public void Init()
        {
            for (int i = 0; i < ThreadNum; i++)
            {
                TS1[i].Init();
                TS2[i].Init();
                Touch_Player_EnemyBullet[i].Start();
                Touch_Player_EnemyLaser[i].Start();
            }
        }
    }

    public class Touch_Player_EnemyBullet_Task
    {
        public int index;
        public ThreadStart TASK, STASK;
        PlayArea Play;
        public Touch_Player_EnemyBullet_Task(int n, PlayArea obj)
        {
            Play = obj;
            index = n;
            TASK = new ThreadStart(DO);
        }

        public void Init()
        {
            STASK = new ThreadStart(Play.IMPACT_ENGINE.SYNC.Do);
        }
        public void DO()
        {
            while (true)
            {
                for (int i = index+1; i < Play.Object_Circle.Count; i+= TouchThread.ThreadNum)
                {
                    if (Play.Object_Circle.ElementAt(0).touch(Play.Object_Circle.ElementAt(i)))
                    {
                        Play.Object_Circle.ElementAt(i).x = Play.rand.Next(0, (int)Play.areaWidth);
                        Play.Object_Circle.ElementAt(i).y = Play.rand.Next(0, (int)Play.areaHeight);
                        new Thread(STASK).Start();
                    }
                }
                Thread.Sleep(1000 / Config.Thread_Rate);
            }
        }
    }

    public class Touch_Player_EnemyLaser_Task
    {
        public int index;
        public ThreadStart TASK, STASK;
        PlayArea Play;
        public Touch_Player_EnemyLaser_Task(int n, PlayArea obj)
        {
            Play = obj;
            index = n;
            TASK = new ThreadStart(DO);
        }

        public void Init()
        {
            STASK = new ThreadStart(Play.IMPACT_ENGINE.SYNC.Do);
        }
        public void DO()
        {
            while (true)
            {
                for (int i = index; i < Play.Object_Laser.Count; i += TouchThread.ThreadNum)
                {
                    if(Play.Object_Laser.ElementAt(i).touch(Play.Object_Circle.ElementAt(0)))
                    {
                        Play.Object_Laser.ElementAt(i).x = Play.rand.Next(0, (int)Play.areaWidth);
                        Play.Object_Laser.ElementAt(i).y = Play.rand.Next(0, (int)Play.areaHeight);
                        Play.Object_Laser.ElementAt(i).rotateSet(Play.rand.Next(0, 360));
                        new Thread(STASK).Start();
                    }
                }
                Thread.Sleep(1000 / Config.Thread_Rate);
            }
        }
    }

    public class SyncTask
    {
        PlayArea Play;
        public SyncTask(PlayArea obj)
        {
            Play = obj;
        }
        public void Do()
        {
            lock (this)
            {
                Play.score += 5;
            }
        }
    }
}

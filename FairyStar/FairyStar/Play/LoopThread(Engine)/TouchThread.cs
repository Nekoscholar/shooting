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
        public static int ThreadNum = 1;  //피탄 스레드 분산. 현재 아래 코드들은 이름 빼고 임시 코드이므로, 추후에 적탄/적레이저/적유닛, 아군탄/아군레이저/아군유닛 클래스 작성 밑 분류가 끝나면 재구성할것.

        Touch_Bullet_Task[] TS1 = new Touch_Bullet_Task[ThreadNum];
        Thread[] Touch_Bullet = new Thread[ThreadNum];
        Touch_Laser_Task[] TS2 = new Touch_Laser_Task[ThreadNum];
        Thread[] Touch_Player_EnemyLaser = new Thread[ThreadNum];

        PlayArea Play;
        public SyncTask SYNC;

        public Timer[] TASK = new Timer[ThreadNum];
        public TouchThread(PlayArea obj)
        {
            Play = obj;
            SYNC = new SyncTask(Play);
            
            for (int i = 0; i < ThreadNum; i++)
            {
                TS1[i] = new Touch_Bullet_Task(i, Play);
                TS2[i] = new Touch_Laser_Task(i, Play);
                Touch_Bullet[i] = new Thread(TS1[i].TASK);
                Touch_Player_EnemyLaser[i] = new Thread(TS2[i].TASK);
            }
        }
        
        public void Init()
        {
            for (int i = 0; i < ThreadNum; i++)
            {

                TASK[i] = new Timer(DO, i, 0, 15);
                //    TS1[i].Init();
                //    TS2[i].Init();
                //    Touch_Bullet[i].Start();
                //    Touch_Player_EnemyLaser[i].Start();
            }
        }

        public void DO(object obj)
        {
            List<int> ErrorIndex = new List<int>();
            for (int i = (int)obj; i < Play.Object_Bullet.Count; i+=ThreadNum)
            {
                try
                {
                    BulletDO(i);
                }
                catch (Exception e)
                {
                    ErrorIndex.Add(i);
                    continue;
                }
            }

            List<int> ErrorIndex2 = new List<int>();
            foreach (int i in ErrorIndex)
            {
                try
                {
                    BulletDO(i);
                }
                catch (Exception e)
                {
                    ErrorIndex2.Add(i);
                    continue;
                }
            }

            foreach (int i in ErrorIndex2)
            {
                try
                {
                    BulletDO(i);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        public void BulletDO(int i)
        {
            lock (Play.Object_Bullet.ElementAt(i))
            {
                if (Play.Object_Bullet.ElementAt(i).REMOVE)
                    return;
                Bullet B = Play.Object_Bullet.ElementAt(i);
                B.MOVE(null);
                switch (B.Team)
                {
                    case 1:
                        break;
                    case 2:
                        for (int j = 0; j < Play.TempPlayer.Count; j++)
                        {
                            if (Play.TempPlayer.ElementAt(j).touch(B))
                            {
                                Play.TempPlayer.ElementAt(j).impact(5);
                                B.REMOVE = true;
                            }
                        }
                        break;
                }
            }
        }
    }

    public class Touch_Bullet_Task
    {
        public int index;
        public ThreadStart TASK, STASK;
        PlayArea Play;
        public Touch_Bullet_Task(int n, PlayArea obj)
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
                for (int i = index; i < Play.Object_Bullet.Count; i+= TouchThread.ThreadNum)
                {
                    try
                    {
                        lock (Play.Object_Bullet.ElementAt(i))
                        {
                            Bullet B = Play.Object_Bullet.ElementAt(i);
                            B.x += B.speedX;
                            B.y += B.speedY;
                            B.speedX += B.accelX;
                            B.speedY += B.accelY;
                            switch (B.Team)
                            {
                                case 1:
                                    break;
                                case 2:
                                    for (int j = 0; j < Play.TempPlayer.Count; j++)
                                    {
                                        if (Play.TempPlayer.ElementAt(j).touch(B))
                                        {
                                            Play.TempPlayer.ElementAt(j).impact(5);
                                            Play.Object_Bullet.RemoveAt(i);
                                            B.Dispose();
                                        }
                                    }
                                    break;
                            }
                            //if (B.outArea())
                            //{
                            //    Play.Object_Bullet.RemoveAt(i);
                            //    B.Dispose();
                            //}
                        }
                    }
                    catch(Exception e)
                    {
                        continue;
                    }
                }
                Thread.Sleep(1000 / Config.Thread_Rate);
            }
        }
    }

    public class Touch_Laser_Task
    {
        public int index;
        public ThreadStart TASK, STASK;
        PlayArea Play;
        public Touch_Laser_Task(int n, PlayArea obj)
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
                    if(Play.Object_Laser.ElementAt(i).touch(Play.TempPlayer.ElementAt(0)))
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

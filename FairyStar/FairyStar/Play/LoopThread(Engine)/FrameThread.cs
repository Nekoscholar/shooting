using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FairyStar
{
    public class FrameThread                    //추후 서브 스레드까지 제작하여 4중 버퍼링 구현할것. 이 클래스는 패널 전체를 담당하고, 서브 스레드에서 비트맵은 플레이 영역으로 한정하여 탄, 유닛, 레이저등 요소를 TouchThread 처럼 나눠서 구현할것
    {                                           //필요 정보 : 투명색 초기화.
        PlayArea Play;
        private Timer Paint_Call;
        public static int Buffer_Count = 3;
        public Bitmap[] Buffer = new Bitmap[Buffer_Count];
        public int Buffer_Select = 0;       //출력버퍼 선택, 입력버퍼는 다음버퍼를 이용
        public Graphics g;
        public Pen p;
        public Brush b;
        ThreadStart TS;
        Thread TASK;
        public FrameThread(PlayArea obj)
        {
            Play = obj;
            Paint_Call = new Timer(repaint, null, 0, 15);

            for(int i=0;i<Buffer_Count;i++)
                Buffer[i] = new Bitmap(Config.pWidth, Config.pHeight);
            
            g = Graphics.FromImage(Buffer[1]);
            Config.DefaultMatrix = g.Transform;
            g.SmoothingMode = Config.SmoothingStrategy;            //그래픽 혹사.. 안되면 AntiAlias 사용.
            Config.DefaultClip = g.ClipBounds;
            p = new Pen(Config.color_PlayArea_Foreground);
            b = new SolidBrush(Config.color_PlayArea_Background);
        }

        public void Init()
        {
            TS = new ThreadStart(BufferPaint);
            TASK = new Thread(TS);
            TASK.Start();
        }
        
        public void EndPaint() {
            while (true)
            {
                Buffer_Select = (++Buffer_Select) % Buffer_Count;
                try
                {
                    g = Graphics.FromImage(Buffer[(Buffer_Select + 1) % Buffer_Count]);
                    g.Clear(Color.Transparent);
                    g.SmoothingMode = Config.SmoothingStrategy;
                }
                catch(Exception e)      //버퍼에 동시 참조가 걸리면 잠기지 않은 다음 버퍼를 찾음.
                {
                    continue;
                }
                break;
            }
        }

        public void BufferPaint()
        {
            while (true)
            {
                DateTime start = DateTime.Now;

                Play.paintDo(g, p, b);
                EndPaint();

                DateTime end = DateTime.Now;
                double term = (end - start).TotalSeconds;
                if (term < 0.015)
                {
                    Thread.Sleep(15 - (int)(term * 1000));
                }
            }
        }

        public long Count = 0;
        public void repaint(object obj)
        {
            Count++;
            Play.window.Refresh();
        }
    }
}

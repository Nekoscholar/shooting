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
    public class FrameThread
    {
        PlayArea Play;

        public Bitmap[] Buffer = new Bitmap[2];
        public int Buffer_Select = 0;       //출력버퍼 선택, 입력버퍼는 나머지 1개를 이용
        public Graphics g;
        public Pen p;
        public Brush b;
        ThreadStart TS;
        Thread TASK;
        public FrameThread(PlayArea obj)
        {
            Play = obj;

            Buffer[0] = new Bitmap(Config.pWidth, Config.pHeight);
            Buffer[1] = new Bitmap(Config.pWidth, Config.pHeight);
            g = Graphics.FromImage(Buffer[1]);
            Config.DefaultMatrix = g.Transform;
            g.SmoothingMode = SmoothingMode.HighQuality;            //그래픽 혹사.. 안되면 AntiAlias 사용.
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
            if (Buffer_Select == 0)
            {
                Buffer_Select = 1;
                g = Graphics.FromImage(Buffer[0]);
                g.Clear(Color.DarkSeaGreen);
                g.SmoothingMode = SmoothingMode.HighQuality;
                Changing = false;
            }
            else
            {
                Buffer_Select = 0;
                g = Graphics.FromImage(Buffer[1]);
                g.Clear(Color.DarkSeaGreen);
                g.SmoothingMode = SmoothingMode.HighQuality;
                Changing = false;
            }
        }
        public bool Changing = false;           //버퍼 바꾸는중에 버퍼 조작을 중단함. 바꾸기 순서는 버퍼 완성-> 화면 그리기 -> 버퍼 타겟 변경 -> 바꾸기 완료 순서임. 동시 참조 문제 해결.
        public void BufferPaint()
        {
            while (true)
            {
                if (Changing)
                {
                    Thread.Sleep(15);
                    continue;
                }
                DateTime start = DateTime.Now;

                Play.paintDo(g, p, b);

                Changing = true;
                repaint(null);
                DateTime end = DateTime.Now;
                double term = (end - start).TotalSeconds;
                if (term < 0.015)
                {
                    Thread.Sleep(15 - (int)(term * 1000));
                }
            }
        }

        public void repaint(object obj)
        {
            Play.window.Refresh();
        }
    }
}

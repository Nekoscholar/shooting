using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FairyStar
{
    public class PlayArea
    {
        public GUI window;
        public Rectangle Z = new Rectangle();
        public float areaWidth = 2000, areaHeight = 2500;

        public List<AnimateCircle> Object_Circle = new List<AnimateCircle>();
        public List<Laser> Object_Laser = new List<Laser>();

        public Random rand = new Random(DateTime.Now.Millisecond);
        public TouchThread IMPACT_ENGINE;
        public FrameThread GRAPHIC_ENGINE;

        public int score = 0;       //디버그
        public PlayArea(GUI obj) : base()
        {
            window = obj;
            Z.Height = (int)(Config.pHeight * 19d / 20d);
            Z.Width = (int)(Z.Height / 5d * 4d);
            Z.Y = (int)(Config.pHeight / 40d);
            Z.X = (int)((Config.pWidth / 2d) - (Z.Width / 2d));
            Config.resolution_rate = Z.Width / areaWidth;
        }

        public bool Load = false;
        public void Init()
        {
            GRAPHIC_ENGINE = new FrameThread(this);
            GRAPHIC_ENGINE.Init();
            IMPACT_ENGINE = new TouchThread(this);
            IMPACT_ENGINE.Init();

            Object_Circle.Add(new AnimateCircle(400, 400, 20, 200, 200));      //임시 개체
            for(int i= 0; i< 1000;i++)
                Object_Circle.Add(new AnimateCircle(rand.Next(0, (int)areaWidth), rand.Next(0, (int)areaHeight), 30, 200, 200));
            for(int i = 0; i < 2; i++)
                Object_Laser.Add(new Laser(1000, 1000, 2000, 20, 180));

            Load = true;
        }
        
        public void paintDo(Graphics g, Pen p, Brush b)
        {
            g.FillRectangle(b, Z.X,Z.Y,Z.Width-1,Z.Height-1);

            g.SetClip(Z);
            b = new SolidBrush(Config.color_PlayArea_Foreground);
            for (int i = 0; i < Object_Circle.Count; i++)
            {
                try
                {
                    Object_Circle.ElementAt(i).paintDo(g, b);
                }
                catch(InvalidOperationException e)
                {
                    continue;
                }
            }
            for(int i = 0; i < Object_Laser.Count; i++)
            {
                try
                {
                    Object_Laser.ElementAt(i).paintDo(g, b);
                }
                catch(InvalidOperationException e)
                {
                    continue;
                }
            }
            g.SetClip(Config.DefaultClip);
        }
    }
    public partial class GUI : Form
    {
        public Pen p = new Pen(Config.color_PlayArea_Foreground);
        public Brush b = new SolidBrush(Config.color_PlayArea_Background);
        private void PlayArea_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Config.DefaultMatrix = g.Transform;
            g.SmoothingMode = SmoothingMode.HighQuality;

            if (Play.Load)
            {
                g.DrawImage(Play.GRAPHIC_ENGINE.Buffer[Play.GRAPHIC_ENGINE.Buffer_Select], 0, 0);
                Play.GRAPHIC_ENGINE.EndPaint();
            }

            g.DrawString(Play.score.ToString(), this.Font, b,50, 50);
        }
    }
}
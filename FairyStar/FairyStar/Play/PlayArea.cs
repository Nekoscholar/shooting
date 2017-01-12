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

        public List<Player> TempPlayer = new List<Player>();
        public List<EnemyUnit> Object_Enemy = new List<EnemyUnit>();
        public List<Bullet> Object_Bullet = new List<Bullet>();
        public List<Laser> Object_Laser = new List<Laser>();

        public Random rand = new Random((int)DateTime.Now.ToBinary());
        public TouchThread IMPACT_ENGINE;
        public FrameThread GRAPHIC_ENGINE;
        public BulletThread MOVE_ENGINE;

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
            MOVE_ENGINE = new BulletThread(this);
            MOVE_ENGINE.Init();
            TempPlayer.Add(new Player(400, 400, 20, 200, 200));      //임시 개체

            EnemyUnit L;
            Object_Enemy.Add(L = new EnemyUnit(1000, 1000, 30, 300, 300));
            Object_Enemy.ElementAt(0).direct = 90;
            L.Init();
            Load = true;
        }

        public void Shot(EnemyUnit E,int order)
        {
            switch (order)
            {
                case 1:
                    for (int i = 0; i < 3; i++) {
                        Bullet L;
                        Object_Bullet.Add(L = new Bullet(E.x, E.y, 20, 100, 100));
                        double Radian = (E.direct+(360/3)*i) * Math.PI / 180;
                        L.speedX = 10 * (float)Math.Cos(Radian);
                        L.speedY = 10 * (float)Math.Sin(Radian);
                        L.Init();
                        L.Action = 0;
                    }
                    break;
            }
        }

        public void paintDo(Graphics g, Pen p, Brush b)
        {
            g.FillRectangle(b, Z.X,Z.Y,Z.Width-1,Z.Height-1);

            g.SetClip(Z);
            b = new SolidBrush(Config.color_PlayArea_Foreground);
            for (int i = 0; i < TempPlayer.Count; i++)
            {
                try
                {
                    TempPlayer.ElementAt(i).paintDo(g, b);
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
            for (int i = 0; i < Object_Enemy.Count; i++)
            {
                try
                {
                    Object_Enemy.ElementAt(i).paintDo(g, p, b);
                }
                catch (InvalidOperationException e)
                {
                    continue;
                }
            }
            for (int i = 0; i < Object_Bullet.Count; i++)
            {
                try
                {
                    Object_Bullet.ElementAt(i).paintDo(g, p, b);
                }
                catch (Exception e)
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
            g.SmoothingMode = Config.SmoothingStrategy;

            if (Play.Load)
                g.DrawImage(Play.GRAPHIC_ENGINE.Buffer[Play.GRAPHIC_ENGINE.Buffer_Select], 0, 0);

            g.DrawString(Play.TempPlayer.ElementAt(0).HP.ToString(), this.Font, b,50, 50);
            g.DrawString(Play.Object_Bullet.Count.ToString(), this.Font, b, 50, 80);
        }
    }
}
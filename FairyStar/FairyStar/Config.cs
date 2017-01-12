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
    public class Config
    {
        public static Color color_player_core = Color.Blue;
        public static Color color_player_bullet_core = Color.Green;
        public static Color color_enemy_core = Color.Red;
        public static Color color_enemy_bullet_core = Color.Yellow;

        public static Color color_PlayArea_Background = Color.Black;
        public static Color color_PlayArea_Foreground = Color.White;

        public static float resolution_rate = 1;           //플레이영역 표시해상도와 실제 좌표간 비율. 값 = 해상도/실제좌표 

        public static int wWidth = 1920, wHeight = 1080;
        public static int pWidth, pHeight;      //패널(표시공간)크기, GUI 초기화시 받음
        public static int Thread_Rate = 60;

        public static Matrix DefaultMatrix;
        public static RectangleF DefaultClip;

        public static SmoothingMode SmoothingStrategy = SmoothingMode.Default;
        
        //public static Bitmap BitBullet = new Bitmap(@"H:\bullet.png");
    }

    public class Res        //해상도에 상관없이 플레이영역에 절대적인 개체 표시를 위한 메소드 모음
    {
        public static GUI window;
        
        public static void DrawEllipse(Graphics g, Pen p, float x, float y, float width, float height)
        {
            g.DrawEllipse(p, window.Play.Z.X + (x * Config.resolution_rate), window.Play.Z.Y + (y * Config.resolution_rate), width * Config.resolution_rate, height * Config.resolution_rate);
        }

        public static void DrawEllipse(Graphics g, Pen p, Circle obj)
        {
            g.DrawEllipse(p, window.Play.Z.X + ((obj.x - obj.radius) * Config.resolution_rate), window.Play.Z.Y + ((obj.y - obj.radius) * Config.resolution_rate), obj.radius * 2 * Config.resolution_rate, obj.radius * 2 * Config.resolution_rate);
        }
        
        public static void FillEllipse(Graphics g, Brush b, Circle obj)
        {
            g.FillEllipse(b, window.Play.Z.X + (obj.x - obj.radius) * Config.resolution_rate, window.Play.Z.Y + (obj.y - obj.radius) * Config.resolution_rate, obj.radius * 2 * Config.resolution_rate, obj.radius * 2 * Config.resolution_rate);
        }

        public static void FillEllipse(Graphics g, Brush b, float x, float y, float radius)
        {
            g.FillEllipse(b, window.Play.Z.X + (x - radius) * Config.resolution_rate, window.Play.Z.Y + (y - radius) * Config.resolution_rate, radius * 2 * Config.resolution_rate, radius * 2 * Config.resolution_rate);
        }

        public static void DrawLaser(Graphics g, Brush b, Laser l)
        {
            g.FillEllipse(b, window.Play.Z.X + (l.x - l.width) * Config.resolution_rate, window.Play.Z.Y + (l.y - l.width) * Config.resolution_rate, l.width * 2 * Config.resolution_rate, l.width * 2 * Config.resolution_rate);
            g.FillEllipse(b, window.Play.Z.X + (l.end.x - l.width) * Config.resolution_rate, window.Play.Z.Y + (l.end.y - l.width) * Config.resolution_rate, l.width * 2 * Config.resolution_rate, l.width * 2 * Config.resolution_rate);
            g.Transform = l.m;
            g.FillRectangle(b, window.Play.Z.X+(l.x*Config.resolution_rate), window.Play.Z.Y+((l.y - l.width)*Config.resolution_rate), l.length* Config.resolution_rate, l.width * 2* Config.resolution_rate);
            g.Transform = Config.DefaultMatrix;
        }

        public static void LaserGraphicSort(Laser l)
        {
            l.m = new Matrix();
            l.m.RotateAt(l.direct, new PointF(window.Play.Z.X+(l.x * Config.resolution_rate), window.Play.Z.Y+(l.y * Config.resolution_rate)));
        }

        public static void DrawDefaultUnit(Graphics g, Pen p, Brush b, AnimateCircle obj)
        {
            System.Drawing.PointF[] Tri = new System.Drawing.PointF[3];
            for (int i = 0; i < 3; i++)
                Tri[i] = new System.Drawing.PointF(window.Play.Z.X + (obj.x + obj.width / 2f * (float)Math.Cos((obj.direct + 120 * i) * Math.PI / 180))*Config.resolution_rate, window.Play.Z.Y + (obj.y + obj.width / 2f * (float)Math.Sin((obj.direct + 120 * i) * Math.PI / 180))* Config.resolution_rate);
            g.DrawPolygon(p,Tri);
        }

        public static void DrawDefaultBullet(Graphics g, Pen p, Brush b, Bullet obj)
        {
            g.DrawEllipse(p, window.Play.Z.X + (obj.x - obj.width / 2f)* Config.resolution_rate, window.Play.Z.Y + (obj.y - obj.height / 2f)* Config.resolution_rate, obj.width* Config.resolution_rate, obj.height* Config.resolution_rate);
            if (obj.Team == 2)
            {
                b = new SolidBrush(Config.color_enemy_bullet_core);
                FillEllipse(g, b, obj);
            }
        }
        public static void DrawDefaultBullet(Graphics g, Pen p, Brush b, float x, float y, float width, float height, float radius, float team)
        {
            // g.DrawImage(Config.BitBullet, window.Play.Z.X + (x - width / 2f) * Config.resolution_rate, window.Play.Z.Y + (y - height / 2f) * Config.resolution_rate, width * Config.resolution_rate, height * Config.resolution_rate);
            g.DrawEllipse(p, window.Play.Z.X + (x - width / 2f) * Config.resolution_rate, window.Play.Z.Y + (y - height / 2f) * Config.resolution_rate, width * Config.resolution_rate, height * Config.resolution_rate);
            if (team == 2)
            {
                b = new SolidBrush(Config.color_enemy_bullet_core);
                FillEllipse(g, b, x, y, radius);
            }
        }
    }
}

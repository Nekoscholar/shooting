using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static double resolution_rate = 1;           //플레이영역 표시해상도와 실제 좌표간 비율. 값 = 해상도/실제좌표 

        public static int wWidth = 1280, wHeight = 720;
        public static int pWidth, pHeight;
    }

    public class Res        //해상도에 상관없이 플레이영역에 절대적인 개체 표시를 위한 메소드 모음
    {
        public static GUI window;
        
        public static void DrawEllipse(Graphics g, Pen p, double x, double y, double width, double height)
        {
            g.DrawEllipse(p, window.Play.x + (int)(x * Config.resolution_rate), window.Play.y + (int)(y * Config.resolution_rate), (int)(width * Config.resolution_rate), (int)(height * Config.resolution_rate));
        }

        public static void DrawEllipse(Graphics g, Pen p, Circle obj)
        {
            g.DrawEllipse(p, window.Play.x + (int)((obj.x - obj.radius) * Config.resolution_rate), window.Play.y + (int)((obj.y - obj.radius) * Config.resolution_rate), (int)(obj.radius * 2 * Config.resolution_rate), (int)(obj.radius * 2 * Config.resolution_rate));
        }
        
        public static void FillEllipse(Graphics g, Brush b, Circle obj)
        {
            g.FillEllipse(b, window.Play.x + (int)((obj.x - obj.radius) * Config.resolution_rate), window.Play.y + (int)((obj.y - obj.radius) * Config.resolution_rate), (int)(obj.radius * 2 * Config.resolution_rate), (int)(obj.radius * 2 * Config.resolution_rate));
        }
    }
}

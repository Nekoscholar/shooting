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
        public int x, y, width, height;
        public double areaWidth=2000, areaHeight=2500;

        public List<AnimateCircle> Object_Circle = new List<AnimateCircle>();

        public PlayArea(GUI obj) : base()
        {
            window = obj;
            height = (int)(Config.pHeight * 19d / 20d);
            width = (int)(height / 5d * 4d);
            y = (int)(Config.pHeight / 40d);
            x = (int)((Config.pWidth / 2d) - (width / 2d));
            Config.resolution_rate = width / areaWidth;
            Object_Circle.Add(new AnimateCircle(400, 400, 100, 200, 200));
        }

        
        public void paintDo(Graphics g, Pen p, Brush b)
        {
            for(int i = 0; i < Object_Circle.Count; i++)
            {
                Object_Circle.ElementAt(i).paintDo(g, b);
            }
        }
    }

    public partial class GUI : Form
    {
        private void PlayArea_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Config.color_PlayArea_Foreground);
            g.SmoothingMode = SmoothingMode.HighQuality;            //그래픽 혹사.. 안되면 AntiAlias 사용.

            Brush b = new SolidBrush(Config.color_PlayArea_Background);
            g.FillRectangle(b, Play.x, Play.y, Play.width, Play.height);
            b = new SolidBrush(Config.color_PlayArea_Foreground);
            Play.paintDo(g, p, b);
        }
    }
}
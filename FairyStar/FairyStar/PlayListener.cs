using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FairyStar
{
    public class PlayListener : KeyTask
    {
        public PlayArea Play;
        public PlayListener(PlayArea obj)
        {
            Play = obj;
        }

        public override void KeyDown(object sender, KeyEventArgs e)
        {
            System.Console.Write("aasd");
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Play.Object_Circle.ElementAt(0).x = (Play.Object_Circle.ElementAt(0).x > 0) ? Play.Object_Circle.ElementAt(0).x - 1 : 0;
                    break;
                case Keys.Right:
                    Play.Object_Circle.ElementAt(0).x = (Play.Object_Circle.ElementAt(0).x < Play.areaWidth) ? Play.Object_Circle.ElementAt(0).x + 1 : Play.areaWidth;
                    break;
                case Keys.Up:
                    Play.Object_Circle.ElementAt(0).y = (Play.Object_Circle.ElementAt(0).y > 0) ? Play.Object_Circle.ElementAt(0).y - 1 : 0;
                    break;
                case Keys.Down:
                    Play.Object_Circle.ElementAt(0).y = (Play.Object_Circle.ElementAt(0).y < Play.areaHeight) ? Play.Object_Circle.ElementAt(0).y + 1 : Play.areaHeight;
                    break;
            }
            Play.window.Refresh();
        }
    }

    public abstract class KeyTask
    {
        public abstract void KeyDown(object sender, KeyEventArgs e);
    }

    public partial class GUI : Form
    {
        private void GUI_KeyDown(object sender, KeyEventArgs e)
        {
            System.Console.Write("aasd");
            KeyIN.KeyDown(sender, e);
        }
    }
}
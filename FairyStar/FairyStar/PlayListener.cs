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

        public bool KEY_UP = false, KEY_DOWN = false, KEY_LEFT = false, KEY_RIGHT = false;

        public override void KeyDown(object sender, KeyEventArgs e)
        {
            int Task_Case = -1;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    KEY_LEFT = true;
                    Task_Case = 1;
                    break;
                case Keys.Right:
                    KEY_RIGHT = true;
                    Task_Case = 1;
                    break;
                case Keys.Up:
                    KEY_UP = true;
                    Task_Case = 1;
                    break;
                case Keys.Down:
                    KEY_DOWN = true;
                    Task_Case = 1;
                    break;
            }

            
        }

        public override void KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    KEY_LEFT = false;
                    break;
                case Keys.Right:
                    KEY_RIGHT = true;
                    break;
                case Keys.Up:
                    KEY_UP = true;
                    break;
                case Keys.Down:
                    KEY_DOWN = true;
                    break;
            }
        }
    }

    public abstract class KeyTask
    {
        public abstract void KeyDown(object sender, KeyEventArgs e);
        public abstract void KeyUp(object sender, KeyEventArgs e);
    }

    public partial class GUI : Form
    {
        private void GUI_KeyDown(object sender, KeyEventArgs e)
        {
            KeyIN.KeyDown(sender, e);
        }
    }
}
//case Keys.Left:
//                    Play.Object_Circle.ElementAt(0).x = (Play.Object_Circle.ElementAt(0).x > 0) ? Play.Object_Circle.ElementAt(0).x - 1 : 0;
//                    break;
//                case Keys.Right:
//                    Play.Object_Circle.ElementAt(0).x = (Play.Object_Circle.ElementAt(0).x<Play.areaWidth) ? Play.Object_Circle.ElementAt(0).x + 1 : Play.areaWidth;
//                    break;
//                case Keys.Up:
//                    Play.Object_Circle.ElementAt(0).y = (Play.Object_Circle.ElementAt(0).y > 0) ? Play.Object_Circle.ElementAt(0).y - 1 : 0;
//                    break;
//                case Keys.Down:
//                    Play.Object_Circle.ElementAt(0).y = (Play.Object_Circle.ElementAt(0).y<Play.areaHeight) ? Play.Object_Circle.ElementAt(0).y + 1 : Play.areaHeight;
//                    break;
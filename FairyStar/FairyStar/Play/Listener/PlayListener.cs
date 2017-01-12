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
        public KeyProcess Process;
        public PlayListener(PlayArea obj)
        {
            Play = obj;
            Process = new KeyProcess(this);
        }

        public bool KEY_UP = false, KEY_DOWN = false, KEY_LEFT = false, KEY_RIGHT = false, KEY_SHIFT = false;

        public override void KeyDown(object sender, KeyEventArgs e)
        {
            int ProcessCase = -1;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    KEY_LEFT = true; ProcessCase = 1;
                    break;
                case Keys.Right:
                    KEY_RIGHT = true; ProcessCase = 1;
                    break;
                case Keys.Up:
                    KEY_UP = true; ProcessCase = 1;
                    break;
                case Keys.Down:
                    KEY_DOWN = true; ProcessCase = 1;
                    break;
                case Keys.ShiftKey:
                    KEY_SHIFT = true; ProcessCase = 0;
                    break;
            }

            switch (ProcessCase)
            {
                case 1:
                    Process.Move();
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
                    KEY_RIGHT = false;
                    break;
                case Keys.Up:
                    KEY_UP = false;
                    break;
                case Keys.Down:
                    KEY_DOWN = false;
                    break;
                case Keys.ShiftKey:
                    KEY_SHIFT = false;
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

        private void GUI_KeyUp(object sender, KeyEventArgs e)
        {
            KeyIN.KeyUp(sender, e);
        }

        private bool onPress = false;
        private void PlayArea_Panel_MouseDown(object sender, MouseEventArgs e)
        {
            onPress = true;
        }

        private void PlayArea_Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (onPress && Play.Z.Contains(e.Location))
            {
                Play.TempPlayer.ElementAt(0).x = (e.X-Play.Z.X)/Config.resolution_rate;
                Play.TempPlayer.ElementAt(0).y = (e.Y - Play.Z.Y) / Config.resolution_rate;
            }
        }

        private void PlayArea_Panel_MouseUp(object sender, MouseEventArgs e)
        {
            onPress = false;
        }
    }
}
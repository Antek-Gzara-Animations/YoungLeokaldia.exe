using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

/*
 * kod który tu stworzyłem jest bardzo chaotyczny ponieważ próbowałem poruszać oknem jak najbardziej chaotycznie się da
 * chyba się udało w miarę możliwości
 * 
 */

namespace YoungLeokaldia.exe
{
    public partial class Form1 : Form
    {

        private SoundPlayer SzklankiPlayer = new SoundPlayer(Properties.Resources.Young_Leosia___Szklanki);

        private int AnimationFrameCount = 0;
        private bool AnimationTypeX = false;
        private bool AnimationTypeY = false;
        private int AnimationChangeNumber = 0;
        private int Speed = 5;

        public Form1()
        {
            InitializeComponent();
        }

        private void wyjdźToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SzklankiPlayer.PlayLooping();
            animation1.Start();
            AnimationChangeNumber = 10 * (new Random().Next(1, 3));
        }

        private void animation1_Tick(object sender, EventArgs e)
        {
            AnimationFrameCount++;

            if (AnimationTypeX)
            {
                Location = new Point(this.Location.X + Speed, this.Location.Y); // przesuń leokaldię w prawo
            }
            else
            {
                Location = new Point(this.Location.X - Speed, this.Location.Y) ; //przesuń leokaldię w lewo
            }

            if (AnimationTypeY)
            {
                Location = new Point(this.Location.X, this.Location.Y + Speed); // przesuń leokaldię w górę
            }
            else
            {
                Location = new Point(this.Location.X, this.Location.Y - Speed); // przesuń leokaldię w dół
            }

            if (AnimationFrameCount > AnimationChangeNumber)
            {
                //if (AnimationTypeX) { AnimationTypeX = false; } else { AnimationTypeX = true; }
                //AnimationTypeX = Convert.ToBoolean(new Random().Next(0, 2)); // nie najlepsze rozwiązanie, ale działą
                
                //AnimationTypeY = Convert.ToBoolean(new Random().Next(0, 2)); // nie najlepsze rozwiązanie ale przyjamniej działa XD
                AnimationFrameCount = 0;
                AnimationChangeNumber = 10 * (new Random().Next(1, 3)); //losowanie odległości do przejścia czy coś takiego

                //próby aby kierunki były jak najbardziej randomowe 

                /*
                if (new Random().Next(0, 1) == 0)
                {
                    if (AnimationTypeY) { AnimationTypeY = false; } else { AnimationTypeY = true; }
                }
                if (new Random().Next(5, 8) == 6 || new Random().Next(5, 8) == 5)
                {
                    if (AnimationTypeX) { AnimationTypeX = false; } else { AnimationTypeX = true; }
                }
                */

                //to wydaje się bardziej randomowe
                try // gdyby milisekunda winosiła 0, program będzie chciał ją pozielić 
                {
                    if (DateTime.Now.Millisecond % DateTime.Now.Millisecond == 0)
                    {
                        if (AnimationTypeY) { AnimationTypeY = false; } else { AnimationTypeY = true; }
                    }

                    if (DateTime.Now.Millisecond % DateTime.Now.Millisecond + new Random().Next(0, 10) == 0)
                    {
                        if (AnimationTypeX) { AnimationTypeX = false; } else { AnimationTypeX = true; }
                    }
                }
                catch
                {
                    if (DateTime.Now.Millisecond % DateTime.Now.Millisecond + 1 == 0)
                    {
                        if (AnimationTypeY) { AnimationTypeY = false; } else { AnimationTypeY = true; }
                    }

                    if (DateTime.Now.Millisecond % DateTime.Now.Millisecond + new Random().Next(0, 10) == 0)
                    {
                        if (AnimationTypeX) { AnimationTypeX = false; } else { AnimationTypeX = true; }
                    }
                }
                /* //to nie działa, nie wiem dlaczego, jest godzina 01:11 i nie chce mi się nawet nad tym myśleć więc lepiej to olać. Ta cześć kody miała sprawdzać czy okno nie wyszło poza ekran i przejść na środek ekranu jeśli się poza nim znajdowało czy coś
                if(Location.X > Screen.PrimaryScreen.WorkingArea.Width)
                {
                    Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2, Location.Y);
                }
                if (Location.X < Screen.PrimaryScreen.WorkingArea.Width)
                {
                    Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2, Location.Y);
                }
                if(Location.Y > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    Location = new Point(Location.X, Screen.PrimaryScreen.WorkingArea.Height / 2);
                }
                if (Location.Y < Screen.PrimaryScreen.WorkingArea.Height)
                {
                    Location = new Point(Location.X, Screen.PrimaryScreen.WorkingArea.Height / 2);
                }
                */

                if (!IsOnScreen(this))
                {
                    CenterToScreen();
                }

            }

            //Console.WriteLine(AnimationFrameCount.ToString() + " " + AnimationTypeX.ToString() + " " + AnimationTypeY.ToString()); //tylko do debugowania!!!
        }

        //kod ze stack overflow: https://stackoverflow.com/questions/987018/determining-if-a-form-is-completely-off-screen
        public bool IsOnScreen(Form form)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens)
            {
                Rectangle formRectangle = new Rectangle(form.Left, form.Top,
                                                         form.Width, form.Height);

                if (screen.WorkingArea.Contains(formRectangle))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

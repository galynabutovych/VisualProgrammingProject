using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Snake
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Game field.
        /// </summary>
        GameField gameField = null;
        Settings settings = null;
        /// <summary>
        /// Settings control.
        /// </summary>


        public Form1()
        {
            InitializeComponent();
            gameField = new GameField(20, 20, 20);
            // TODO: set correct geometry
            //gameField.Width = this.ClientRectangle.Width;
            //gameField.Height = this.ClientRectangle.Height;
            gameField.Width = 20 * 20;
            gameField.Height = 20 * 20;
            gameField.OnUpdateScore +=new GameField.ScoreUpdateHandler(onScoreChanged);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            
            this.Controls.Add(gameField);
            //SizeFromClientSize(gameField.Size + menuStrip1.Size);

            ClientSize =new Size(gameField.Size.Width, gameField.Size.Height + menuStrip1.Size.Height + statusStrip1.Size.Height);    
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }
       
       
        public void pauseFromGameField()     // call pause from gamefield
        {
            gameField.pause();
            toolStripMenuItem2.Visible = true;
            pauseToolStripMenuItem.Visible = false;
        }

        public void resumeFromGameField()   //call resume from gamefield
        {
                gameField.resume();
                toolStripMenuItem2.Visible = false;
                pauseToolStripMenuItem.Visible = true;
        }


        //about

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseFromGameField();
            About about = new About();
            about.Show();
        }

          // key down event handler

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                    case Keys.Escape:          //exit from escape
                    pauseFromGameField();
                     DialogResult result = MessageBox.Show("Are you sure you want to exit?","Exit", MessageBoxButtons.YesNo);

           if (result == DialogResult.Yes)
           {

               DialogResult resultsave = MessageBox.Show("Do you want to save the game?", "Save", MessageBoxButtons.YesNo);
               if (resultsave == DialogResult.Yes)
               {
                   // save game
               }
               else Close();
           }
               break;

                case Keys.Space:       //pause (resume) from space
           if (gameField.isRunning())
           {
               pauseFromGameField();
           }
           else
           {
                resumeFromGameField();
           }
           break;
                
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
             {

                 pauseFromGameField();
             }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)  // resume (menustrip)
            {
               resumeFromGameField();           
            }

        private void newGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
                    
            pauseFromGameField();
            DialogResult result = MessageBox.Show("Are you sure you want to start new game?", "New game", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {

                DialogResult resultsave = MessageBox.Show("Do you want to save the game?", "Save", MessageBoxButtons.YesNo);
                if (resultsave == DialogResult.Yes)
                {
                    // save game
                   gameField.StartGame(); //new game
                }
                else
                {
                   resumeFromGameField();
                   
                    gameField.StartGame(); //new game;
                }
            }

        
        }

        // exit

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
 

            pauseFromGameField();

           DialogResult result = MessageBox.Show("Are you sure you want to exit?","Exit", MessageBoxButtons.YesNo);

           if (result == DialogResult.Yes)
           {

               DialogResult resultsave = MessageBox.Show("Do you want to save the game?", "Save", MessageBoxButtons.YesNo);
               if (resultsave == DialogResult.Yes)
               {
                   // save game
               }
               else Close();
           }
         

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pauseFromGameField();
            if (settings == null)
            {
                settings = new Settings();
                settings.OnUpdateSpeed += new Settings.SpeedUpdateHandler(speedChanged);
                settings.TopMost = true;
            }
            settings.Show();
            resumeFromGameField();
        }

        void speedChanged(SpeedEventArgs e)
        {
            if (gameField != null)
            {
                gameField.setTimerInterval(e.Speed);
            }
        }

        void onScoreChanged(ScoreEventArgs e)
        {

            this.ScoreCounterLabel.Text = e.Score.ToString();
        }

   }
}

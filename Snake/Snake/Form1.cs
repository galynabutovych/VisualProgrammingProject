using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Snake
{
    public partial class Form1 : Form
    {
        #region Variables & Constructors
        GameField gameField = null;
        Settings settings = null;

        public Form1()
        {
            InitializeComponent();
            gameField = new GameField(22, 40, 25);
            gameField.Width = 40 * 25;
            gameField.Height = 22 * 25;
            gameField.OnUpdateScore +=new GameField.ScoreUpdateHandler(onScoreChanged);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            ResizeRedraw = false;
            
            this.Controls.Add(gameField);
            ClientSize =new Size(gameField.Size.Width, gameField.Size.Height + menuStrip1.Size.Height + statusStrip1.Size.Height);    
        }
        #endregion

        #region Methods
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
        #endregion

        #region Event Handlers
        // key down event handler
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Escape:          //exit from escape
                    pauseFromGameField();
                    DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);

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

        #endregion

        #region Menu
        //about
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
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
            DialogResult resultsave = MessageBox.Show("Do you want to save the game?", "Save", MessageBoxButtons.YesNoCancel);
            if (resultsave == DialogResult.Yes)
            {
                // save game
                gameField.StartGame(GameSettings.Default); //new game;
            }
            else if (resultsave == DialogResult.No)
            {
                resumeFromGameField();
                gameField.StartGame(GameSettings.Default); //new game;
            }

        }

        // exit

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseFromGameField();

            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);

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
           // resumeFromGameField();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog lSaveDialog = new SaveFileDialog();
            lSaveDialog.Title = "Save game";
            lSaveDialog.OverwritePrompt = true;
            if (lSaveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StorageManager.storageInstance.storeGame(gameField.getSettings(), lSaveDialog.FileName);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog lOpenDialog = new OpenFileDialog();
            lOpenDialog.Title = "Load game";
            lOpenDialog.Filter = "Xml Files|*.xml;";
            if (lOpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GameSettings lLoadedGame = StorageManager.storageInstance.loadGame(lOpenDialog.FileName);
                if(lLoadedGame != null)
                {
                    // pass it to gamefield (create new game)
                    gameField.StartGame(lLoadedGame);
                }
            }
            
        }
        #endregion

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            pauseFromGameField();
            Invalidate();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseFromGameField();
        }

     
  }
}

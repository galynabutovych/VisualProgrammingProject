using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Media;

namespace Snake
{
    public partial class Form1 : Form
    {
        #region Variables & Constructors
        GameField gameField = null;
        Settings settings = null;
        SoundPlayer sp = new SoundPlayer();
        bool isSoundActive = false;

        public Form1()
        {
            InitializeComponent();
            gameField = new GameField(22, 40, 25);
            gameField.Width = 40 * 25;
            gameField.Height = 22 * 25;
            gameField.OnUpdateScore +=new GameField.ScoreUpdateHandler(onScoreChanged);
            gameField.EndOfGame += new GameField.GameOver(onGameOver);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            ResizeRedraw = false;
            
            this.Controls.Add(gameField);
            ClientSize =new Size(gameField.Size.Width, gameField.Size.Height + menuStrip1.Size.Height + statusStrip1.Size.Height);
            gameField.StartGame(GameSettings.Default);
            sp.Stream = global::Snake.Properties.Resources.melody_zizibum;
        }
        #endregion

        #region Methods
        public void pauseFromGameField()     // call pause from gamefield
        {
            if (gameField.isGameOver())
                return;
            gameField.pause();
            toolStripMenuItem2.Visible = true;
            pauseToolStripMenuItem.Visible = false;
        }

        public void resumeFromGameField()   //call resume from gamefield
        {
            if (gameField.isGameOver())
                return;
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
                        if (!gameField.isGameOver())
                        {
                            DialogResult resultsave = MessageBox.Show("Do you want to save the game?", "Save", MessageBoxButtons.YesNo);
                            if (resultsave == DialogResult.Yes)
                            {
                                saveRequested();
                            }
                        }
                        Close();
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

        void onScoreChanged(GameEventArgs e)
        {
            this.ScoreCounterLabel.Text = e.Score.ToString();
        }

        void onGameOver()
        {
            saveToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Visible = false;
            toolStripMenuItem2.Visible = false;
        }

        void onSettingsChanged()
        {
            if (GeneralSettings.Default.Sound)
            {
                if (!isSoundActive)
                    startBackGroundSound();
            }
            else
            {
                if (isSoundActive)
                    stopBackgroundSound();
            }
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
            DialogResult resultsaveNG = MessageBox.Show("Do you want to start new game?", "New Game", MessageBoxButtons.YesNo);
            if (resultsaveNG == DialogResult.Yes)
            {
                if (!gameField.isGameOver())
                {
                    DialogResult resultsave = MessageBox.Show("Do you want to save the game?", "Save", MessageBoxButtons.YesNo);
                    if (resultsave == DialogResult.Yes)
                    {
                        saveRequested();
                        //gameField.StartGame(GameSettings.Default); //new game;
                    }
                    //else if (resultsave == DialogResult.No)
                    //{
                    //    resumeFromGameField();
                    //    //gameField.StartGame(GameSettings.Default); //new game;
                    //}
                }
                gameField.StartGame(GameSettings.Default);
                saveToolStripMenuItem.Enabled = true;
                pauseToolStripMenuItem.Visible = true;
            }          

        }

        // exit

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseFromGameField();

            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                if (!gameField.isGameOver())
                {
                    DialogResult resultsave = MessageBox.Show("Do you want to save the game?", "Save", MessageBoxButtons.YesNo);
                    if (resultsave == DialogResult.Yes)
                    {
                        saveRequested();
                    }
                }
                Close();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pauseFromGameField();
            if (settings == null)
            {
                settings = new Settings();
                settings.OnUpdateSpeed += new Settings.SpeedUpdateHandler(speedChanged);
                settings.Changed += new Settings.SettingsChanged(onSettingsChanged);
                settings.TopMost = true;
            }
            settings.Show();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveRequested();
        }

        void saveRequested()
        {
            if (!gameField.isGameOver())
            {
                SaveFileDialog lSaveDialog = new SaveFileDialog();
                lSaveDialog.Title = "Save game";
                lSaveDialog.OverwritePrompt = true;
                if (lSaveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    StorageManager.storageInstance.storeGame(gameField.getSettings(), lSaveDialog.FileName);
                }
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

        private void startBackGroundSound()
        {
            isSoundActive = true;
            sp.PlayLooping();
        }

        private void stopBackgroundSound()
        {
            isSoundActive = false;
            sp.Stop();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if(GeneralSettings.Default.Sound)
            startBackGroundSound();
        }
  }
}

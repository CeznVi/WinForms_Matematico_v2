using Matematico.GameFieldControl;
using System.Numerics;
using System;

namespace Matematico
{
    public partial class Form_Main : Form
    {
        private Game _game;

        public Form_Main()
        {
            InitializeComponent();
            _game = new Game(new CardDeck(gameFieldControl_Player.Buttons), new CardDeck(gameFieldControl_Computer.Buttons));
            _game.OnNextNumberChanged += _game_OnNextNumberChanged;
            _game.OnGameFinished += _game_OnGameFinished;

        }

        private void button_NewGame_Click(object sender, EventArgs e)
        {
            _game.NewGame();
            button_nextNumber.Text = _game.GetNextNumber().ToString();
            toolStripStatusLabel_info.Text = "Идет игра";
        }

        private void _game_OnGameFinished(object sender, Player e)
        {
            string message;
            
            if (e != null)
            {
                message = $"Победил {e.Login} ";
            }
            else
            {
                message = $"Ничья";
            }

            toolStripStatusLabel_info.Text = message;

            MessageBox.Show(message, "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void _game_OnNextNumberChanged(object sender, int e)
        {
            button_nextNumber.Text = e.ToString();
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _game.NewGame();
            button_nextNumber.Text = _game.GetNextNumber().ToString();
            toolStripStatusLabel_info.Text = "Идет игра";
        }
    }
}
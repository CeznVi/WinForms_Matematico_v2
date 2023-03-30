using Matematico.GameFieldControl;
using System.Numerics;
using System;
using System.Xml.Linq;

namespace Matematico
{
    public partial class Form_Main : Form
    {
        private Game _game;

        public Form_Main()
        {
            InitializeComponent();

            _game = new Game(new CardDeck(gameFieldControl_Player.Buttons), new CardDeck(gameFieldControl_Computer.Buttons));
            IncludeDifficulty();
            _game.OnNextNumberChanged += _game_OnNextNumberChanged;
            _game.OnGameFinished += _game_OnGameFinished;
            _game.ShowScore += _game_ShowScore;
        }

        private void button_NewGame_Click(object sender, EventArgs e)
        {
            NewStartGame(sender, e);
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

        private void _game_ShowScore(object sender, Player e)
        {
            string message = "Cчет: ";

            if (e.Login == _game.Player.Login)
            {
                message += _game.Player.Points.ToString();
                labelPlayerScore.Text = message;
            }
            else
            {
                message += _game.Comp.Points.ToString();
                labelComputerScore.Text = message;
            }


        }

        private void _game_OnNextNumberChanged(object sender, int e)
        {
            button_nextNumber.Text = e.ToString();
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewStartGame(sender, e);
        }

        /// <summary>
        /// Метод начала игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewStartGame(object sender, EventArgs e)
        {
            _game.NewGame();
            labelPlayerScore.Text = "Cчет: ";
            labelComputerScore.Text = "Cчет: ";
            button_nextNumber.Text = _game.GetNextNumber().ToString();
            toolStripStatusLabel_info.Text = "Идет игра";

            ///Включение текстового поля отображения набранных очков
            labelPlayerScore.Visible = true;
            labelComputerScore.Visible = true;
        }

        private void IncludeDifficulty()
        {
            comboBoxDifficulty.Items.Add(Difficulty.Easy);
            comboBoxDifficulty.Items.Add(Difficulty.Normal);
            comboBoxDifficulty.Items.Add(Difficulty.Hard);
            comboBoxDifficulty.SelectedIndex = 0;
            _game.GameDifficulty = comboBoxDifficulty.SelectedItem.ToString();

        }

        private void comboBoxDifficulty_SelectedValueChanged(object sender, EventArgs e)
        {
            _game.GameDifficulty = comboBoxDifficulty.SelectedItem.ToString();
        }
    }
}
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
                message = $"������� {e.Login} ";
            }
            else
            {
                message = $"�����";
            }

            toolStripStatusLabel_info.Text = message;

            MessageBox.Show(message, "����������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void _game_ShowScore(object sender, Player e)
        {
            string message = "C���: ";

            if (e.Login ==  _game.Player.Login)
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

        private void ���������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewStartGame(sender, e);
        }

        /// <summary>
        /// ����� ������ ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewStartGame(object sender, EventArgs e)
        {
            _game.NewGame();
            labelPlayerScore.Text = "C���: ";
            labelComputerScore.Text = "C���: ";
            button_nextNumber.Text = _game.GetNextNumber().ToString();
            toolStripStatusLabel_info.Text = "���� ����";

            ///��������� ���������� ���� ����������� ��������� �����
            labelPlayerScore.Visible = true;
            labelComputerScore.Visible = true;
        }
    }
}
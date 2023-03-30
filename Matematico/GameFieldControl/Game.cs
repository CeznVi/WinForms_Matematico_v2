using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Matematico.GameFieldControl
{
    class Game
    {
        public int _сurrentNumber = 0;
        private Random _rand;
        private List<int> _numbers;

        public Player Player = new() { Login = "Игрок" };
        public Player Comp = new() { Login = "Компьютер" };
        public CardDeck CardDeckPlayer { get; set; }
        public CardDeck CardDeckComputer { get; set; }

        public event EventHandler<int> OnNextNumberChanged;

        protected virtual void OnNextNumberChangedCompleted(int number)
        {
            OnNextNumberChanged?.Invoke(this, number);
        }

        public event EventHandler<Player> OnGameFinished;

        protected virtual void OnGameFinishedCompleted(Player winner)
        {
            OnGameFinished?.Invoke(this, winner);
        }

        /// <summary>
        /// Конструткор
        /// </summary>
        /// <param name="cardDeckPlayer">Доска игрока</param>
        /// <param name="CardDeckComp">Доска компьютера</param>
        public Game(CardDeck cardDeckPlayer, CardDeck CardDeckComp)
        {
            _rand = new Random();
            _numbers = new List<int>();

            CardDeckPlayer = cardDeckPlayer;
            CardDeckComputer = CardDeckComp;

            foreach (var oneRowCard in CardDeckPlayer.Cards)
            {
                foreach (var oneCard in oneRowCard)
                {
                    oneCard.Button.Click += Button_Click;
                }
            }
        }

        ///----------------------------ТЕЛО ИГРЫ В СОБЫТИИ КЛИКА
        private void Button_Click(object sender, EventArgs e)
        {

            Button tmp = (Button)sender;
            Card currentCard = null;

            foreach (var oneRowCard in CardDeckPlayer.Cards)
            {
                foreach (var oneCard in oneRowCard)
                {
                    if (oneCard.Button == tmp)
                    {
                        currentCard = oneCard;
                    }
                }
            }

            if (currentCard.Button.Enabled == true)
            {
                currentCard.Button.Text = CurrentNumber.ToString();
                currentCard.Button.Enabled = false;
                currentCard.Points = CurrentNumber;

                CompStep();

                if (_numbers.Count == 27)
                {
                    OnGameFinishedCompleted(CheckWinner());
                }
                else
                {
                    GetNextNumber();
                }
            }
        }

        /// <summary>
        /// Возвращает Player победителя
        /// </summary>
        /// <returns></returns>
        private Player CheckWinner()
        {

            if (CardDeckPlayer.GetPoints() > CardDeckComputer.GetPoints())
            {
                return Player;
            }
            else if (CardDeckPlayer.GetPoints() < CardDeckComputer.GetPoints())
            {
                return Comp;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Ход компьютера
        /// </summary>
        private void CompStep()
        {
            List<Card> _freeCard = CardDeckComputer.GetFreeCards();

            int index = _rand.Next(0, _freeCard.Count-1);

            _freeCard[index].Points = CurrentNumber;
            _freeCard[index].Button.Text = CurrentNumber.ToString();
            _freeCard[index].Button.Enabled = false;
        }

        private void fillNumbers()
        {
            _numbers.Clear();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    _numbers.Add(j);
                }
            }
        }

        public void NewGame()
        {
            fillNumbers();
            CardDeckPlayer.Clear();
            CardDeckComputer.Clear();
        }

        public int CurrentNumber
        {
            get { return _сurrentNumber; }
            private set
            {
                _сurrentNumber = value;
            }
        }

        public int GetNextNumber()
        {
            int randIndex = _rand.Next(0, _numbers.Count - 1);
            CurrentNumber = _numbers[randIndex];
            _numbers.RemoveAt(randIndex);
            OnNextNumberChangedCompleted(CurrentNumber);

            return CurrentNumber;
        }
    }


}

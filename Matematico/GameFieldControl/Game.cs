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

        public event EventHandler<Player> ShowScore;

        protected virtual void GetScore(Player p)
        {
            ShowScore?.Invoke(this, p);
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

                Player.Points = CardDeckPlayer.GetPoints();
                GetScore(Player);

                CompStep();
                Comp.Points = CardDeckComputer.GetPoints();
                GetScore(Comp);

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
            ////Ход рандомно
            //List<Card> _freeCard = CardDeckComputer.GetFreeCards();
            //int index = _rand.Next(0, _freeCard.Count-1);
            //_freeCard[index].Points = CurrentNumber;
            //_freeCard[index].Button.Text = CurrentNumber.ToString();
            //_freeCard[index].Button.Enabled = false;

            List<Card> _freeCard = CardDeckComputer.GetFreeCards();
            int index;

            ///если первый ход то "пальцем в небо"
            if (_numbers.Count - 1 == 52)
                index = _rand.Next(0, _freeCard.Count - 1);


            ////описание просчетных ходов с учетом всего
            else
            {
                index = CalculateMoveInHardMode(_freeCard);
                
                if (index == -1)
                    index = _rand.Next(0, _freeCard.Count - 1);
            }
            


            _freeCard[index].Points = CurrentNumber;
            _freeCard[index].Button.Text = CurrentNumber.ToString();
            _freeCard[index].Button.Enabled = false;
        }

        private int CalculateMoveInHardMode(List<Card> freeCard)
        {
            ///Переменна для запоминания индекса;
            int tempIndex = 0;
 
            int thisPoints =0, lastPoints = 0;

            for (int i = 0; i<freeCard.Count-1; i++) 
            {
                freeCard[i].Points = CurrentNumber;

                thisPoints = CardDeckComputer.GetPoints();

                if (thisPoints > lastPoints)
                    tempIndex = i;

                lastPoints = thisPoints;

               freeCard[i].Points = 0;
            }

            if (thisPoints == 0)
                return -1;

            return tempIndex;
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

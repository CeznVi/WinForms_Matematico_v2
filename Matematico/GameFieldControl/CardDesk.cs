using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matematico.GameFieldControl
{
    class CardDeck
    {
        /* -------------_______  Переменные _______------------- */
        /// <summary>
        /// Масив для хранение Card
        /// </summary>
        private Card[][] _cards = new Card[5][];
       
        /* -------------_______  Конструкторы _______------------- */
        public CardDeck(TableLayoutControlCollection buttons)
        {
            for (int i = 0; i < _cards.Length; i++)
            {
                _cards[i] = new Card[5];
            }

            int counter = 0;
            int ind = 0;

            foreach (Button oneBut in buttons)
            {
                _cards[counter / 5][ind] = new Card();
                _cards[counter / 5][ind].Button = oneBut;
                _cards[counter / 5][ind].Button.Enabled = false;
                _cards[counter / 5][ind].Button.BackgroundImage = ImageResource.bg;
                _cards[counter / 5][ind].Button.BackgroundImageLayout = ImageLayout.Stretch;
                _cards[counter / 5][ind].Points = 0;
                counter++;
                ind++;
                if (ind == 5) ind = 0;
            }
        }
        
        /* -------------_______    Свойства    _______------------- */
        /// <summary>
        /// Возвращает _cards
        /// </summary>
        public Card[][] Cards
        {
            get { return _cards; }
        }

        /* -------------_______Публичные методы_______------------- */
        /// <summary>
        /// Очищает карточную доску (Points и Button.Text)
        /// </summary>
        public void Clear()
        {
            for (int r = 0; r < _cards.Length; r++)
            {
                for (int c = 0; c < _cards[r].Length; c++)
                {
                    _cards[r][c].Points = 0;
                    _cards[r][c].Button.Enabled = true;
                    _cards[r][c].Button.BackgroundImage = null;
                    _cards[r][c].Button.Text = "";
                }
            }
        }
        /// <summary>
        /// Возвращает просчитаное количество очков(по вертикалям,горизонатям,диагоналям) согласно правил игры 
        /// </summary>
        /// <returns></returns>
        public int GetPoints()
        {
            int _point = 0;

            List<int[]> _verticaAndHorrizontal = GetVerticalAndHorizontalArras();
            List<int[]> _diagonal = GetDiagonalArrays();

            ///Просчет очков по вертикалям и горизонталям
            foreach (int[] arr in  _verticaAndHorrizontal)
            {
                _point += Scoring.CheckTwoIdentialNumbers(arr);
                _point += Scoring.CheckThreeIdentialNumbers(arr);
                _point += Scoring.CheckFourIdentialNumbers(arr);
                _point += Scoring.CheckFourUnitsNumbers(arr);
                _point += Scoring.CheckTwoPairIdentialNumbers(arr);
                _point += Scoring.CheckThreeAndTwoIdentialNumbers(arr);
                _point += Scoring.CheckThreeUnitAndTwoThirteenNumbers(arr);
                _point += Scoring.CheckCombinationNumbers(arr);
                _point += Scoring.CheckFiveConsecutiveNumbers(arr);
            }

            ///Просчет очков по диагоналям
            foreach (int[] arr in _diagonal)
            {
                _point += Scoring.CheckTwoIdentialNumbers(arr);
                _point += Scoring.CheckThreeIdentialNumbers(arr);
                _point += Scoring.CheckFourIdentialNumbers(arr);
                _point += Scoring.CheckFourUnitsNumbers(arr);
                _point += Scoring.CheckTwoPairIdentialNumbers(arr);
                _point += Scoring.CheckThreeAndTwoIdentialNumbers(arr);
                _point += Scoring.CheckThreeUnitAndTwoThirteenNumbers(arr);
                _point += Scoring.CheckCombinationNumbers(arr);
                _point += Scoring.CheckFiveConsecutiveNumbers(arr);
            }

            return _point;
        }

        public List<Card> GetFreeCards()
        {
           List<Card> _freeCards = new();

            foreach (var item in _cards)
            {
                foreach (Card card in item)
                {
                    if(card.Button.Enabled == true)
                        _freeCards.Add(card);
                }
            }

            return _freeCards;
        }

        /* -------------_______Приватные методы_______------------- */
        /// <summary>
        /// Разбивка двумерного масива на список горизонтальных масивов
        /// </summary>
        /// <returns>Список горизонтальных масовов</returns>        
        private List<int[]> GetHorizonntalArrays()
        {
            List<int[]> _temp = new();

            for (int row = 0; row < 5; row++)
            {
                int[] arr = new int[5];

                for(int column = 0; column < 5; column++)
                {
                    arr[column] = _cards[row][column].Points;
                }

                _temp.Add(arr);
            }

            return _temp;
        }
        /// <summary>
        /// Разбивка двумерного масива на список вертикальных масивов
        /// </summary>
        /// <returns>Список вертикальных масовов</returns>        
        private List<int[]> GetVerticalArrays()
        {
            List<int[]> _temp = new();

            for (int column = 0; column < 5; column++)
            {
                int[] arr = new int[5];

                for (int row = 0; row < 5; row++)
                {
                    arr[row] = _cards[row][column].Points;
                }

                _temp.Add(arr);
            }

            return _temp;
        }
        /// <summary>
        /// Разбивает двумерный масив на список в котором содержатся Points по вертикали и горизонтали
        /// </summary>
        /// <returns>Список масивов с Point по вертикали и горизонтали</returns>
        private List<int[]> GetVerticalAndHorizontalArras()
        {
            List<int[]> _temp = new();

            ///Горизонтальный масив
            _temp.AddRange(GetHorizonntalArrays());

            ///Вертикальный масив
            _temp.AddRange(GetVerticalArrays());

            return _temp;
        }
        /// <summary>
        /// Разбивает двумерный масив на список в котором содержатся Points по диагоналям
        /// </summary>
        /// <returns>Список масивов с Point по вертикали и горизонтали</returns>
        private List<int[]> GetDiagonalArrays()
        {
            List<int[]> _temp = new();

            ///Диагональ 1
            int[] arr = new int[5];
            int rowDiag = 0;
            for (int column = 0; column < 5; column++)
            {

                arr[column] = _cards[rowDiag][column].Points;
                rowDiag++;

            }
            _temp.Add(arr);

            ///Диагональ 2
            arr = new int[5];
            rowDiag = 4;
            for (int column = 0; column < 5; column++)
            {

                arr[column] = _cards[rowDiag][column].Points;
                rowDiag--;

            }
            _temp.Add(arr);

            return _temp;
        }

    }



}

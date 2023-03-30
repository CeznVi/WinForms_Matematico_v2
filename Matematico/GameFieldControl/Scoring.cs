using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matematico.GameFieldControl
{
    class Scoring
    {
        //поиск двух одинаковых чисел    
        public static int CheckTwoIdentialNumbers(int[] arr, bool isColumnOrRow = true)
        {
            var result = arr.GroupBy(n => n).Where(n => n.Count() == 2).ToList().Count();

            if (result == 1)
            {
                return isColumnOrRow ? 10 : 20;
            }
            return 0;
        }
        //поиск трех одинаковых чисел
        public static int CheckThreeIdentialNumbers(int[] arr, bool isColumnOrRow = true)
        {
            var result = arr.GroupBy(n => n).Where(n => n.Count() == 3).ToList().Count();

            if (result == 1)
            {
                return isColumnOrRow ? 40 : 50;
            }
            return 0;
        }
        //поиск четырех одинаковых чисел
        public static int CheckFourIdentialNumbers(int[] arr, bool isColumnOrRow = true)
        {
            var result = arr.GroupBy(n => n).Where(n => n.Count() == 4).ToList().Count();

            if (result > 0)
            {
                return isColumnOrRow ? 160 : 170;
            }
            return 0;
        }
        //поиск четырех единиц
        public static int CheckFourUnitsNumbers(int[] arr, bool isColumnOrRow = true)
        {
            var result = arr.GroupBy(n => n).Where(n => n.Count() == 4 && n.Key == 1).ToList().Count();

            if (result > 0)
            {
                return isColumnOrRow ? 200 : 210;
            }
            return 0;
        }
        //поиск двух пар одинаковых чисел    
        public static int CheckTwoPairIdentialNumbers(int[] arr, bool isColumnOrRow = true)
        {
            var result = arr.GroupBy(n => n).Where(n => n.Count() == 2).ToList().Count();

            if (result == 2)
            {
                return isColumnOrRow ? 20 : 30;
            }
            return 0;
        }
        //поиск трех и двух одинаковых чисел    
        public static int CheckThreeAndTwoIdentialNumbers(int[] arr, bool isColumnOrRow = true)
        {
            if (CheckThreeIdentialNumbers(arr, isColumnOrRow) > 0 && CheckTwoIdentialNumbers(arr, isColumnOrRow) > 0)
            {
                return isColumnOrRow ? 80 : 90;
            }
            return 0;
        }
        //поиск трех единиц и 2х 13 ток    
        public static int CheckThreeUnitAndTwoThirteenNumbers(int[] arr, bool isColumnOrRow = true)
        {
            var resultUnits = arr.GroupBy(n => n).Where(n => n.Count() == 3 && n.Key == 1).ToList().Count();
            var resultThirteen = arr.GroupBy(n => n).Where(n => n.Count() == 2 && n.Key == 13).ToList().Count();


            if (resultUnits == 1 && resultThirteen == 1)
            {
                return isColumnOrRow ? 100 : 110;
            }
            return 0;
        }
        //поиск комбинации: 1, 13, 12, 11, 10
        public static int CheckCombinationNumbers(int[] arr, bool isColumnOrRow = true)
        {
            int[] tmp = { 1, 13, 12, 11, 10 };
            bool equals = false;
            for (int i = 0; i < tmp.Length; i++)
            {
                equals = false;
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[j] == tmp[i])
                    {
                        equals = true;
                        break;
                    }
                }
                if (equals != true)
                {
                    break;
                }
            }
            if (equals == false)
            {
                return 0;
            }
            else
            {
                return isColumnOrRow ? 150 : 160;
            }
        }
        // поиск пяти последовательных чисел не обяз по порядку
        public static int CheckFiveConsecutiveNumbers(int[] arr, bool isColumnOrRow = true)
        {

            List<int> tmp = arr.ToList();
            tmp.Sort();

            bool isConsecutiveNum = false;

            for (int i = 0; i < tmp.Count-2; i++)
            {
                if (tmp[i] - tmp[i + 1] == -1)
                    isConsecutiveNum = true;
                else
                    break;
            }

            if (isConsecutiveNum == true)
            {
                return isColumnOrRow ? 50 : 60;
            }
            return 0;
                        
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SymbolRepeat
{
    class Program
    {
        private static int _numPerCard;
        private static List<Dictionary<int, bool>> _cardsSoFar;
        private static List<int> _index;
        private static int _numSymbols; 

        static void Main(string[] args)
        {
            _numPerCard = 6;
            // http://www.pleacher.com/mp/mlessons/stat/spotit.html
            _numSymbols = _numPerCard * _numPerCard - _numPerCard + 1; 

            _cardsSoFar = new List<Dictionary<int, bool>>();

            _index = new List<int>();
            while (_index.Count < _numPerCard)
            {
                _index.Add(0);
            }

            try
            {
                do
                {
                    //Console.Write("Consider: "+Dumpit());
                    if (IsValidSymbol())
                    {
                        //Console.Write(" Valid"); 

                        // must have exactly one symbol matching with every pervious card so far
                        if (IsANewCardWithExactlyOneMatchWithAll())
                        {
                            AddNewCardToPile();
                            // Console.Write(" ADDED");
                            Console.WriteLine(Dumpit());
                        }
                    }
//                    Console.WriteLine();
                } while (AdvanceIndex());
            }
            finally
            {
                Console.WriteLine("-----COMPLETE");
                Console.ReadLine();
            }
        }

        private static bool IsANewCardWithExactlyOneMatchWithAll()
        {
            foreach (var card in _cardsSoFar)
            {
                var numberOfMatches = 0;
                foreach (var i in _index)
                {
                    if (card.ContainsKey(i)) numberOfMatches++;
                    if (numberOfMatches > 1)
                    {
  //                      Console.Write($" NO: {Dumpit(card)}={numberOfMatches}");
                        return false;
                    }
                }
                if (numberOfMatches == 0)
                {
    //                Console.Write($" NO: 0 matches with {Dumpit(card)}");
                    return false;
                }
            }
            return true;
        }

        //private static string  Dumpit(Dictionary<int, bool> card)
        //{
        //    var sb = new StringBuilder();
        //    foreach (var i in card.Keys)
        //    {
        //        sb.Append(Convert.ToChar('A' +i));
        //    }
        //    return sb.ToString(); 
        //}

        private static void AddNewCardToPile()
        {
            var matchyness = new Dictionary<int, bool>();
            foreach (var i in _index)
            {
                matchyness[i] = true;
            }
            _cardsSoFar.Add(matchyness);
        }

        public static string Dumpit()
        {
            var sb = new StringBuilder();
            foreach (var x in _index)
            {
                sb.Append(Convert.ToChar('A'+x));
            }
            return sb.ToString();
        }

        private static bool AdvanceIndex()
        {
            var x = _index.Count - 1;
            while (x >= 0)
            {
                _index[x]++;
                if (_index[x] >= _numSymbols)
                {
                    _index[x] = 0;
                    x--;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }


        private static bool IsValidSymbol()
        {
            var _seen = new bool[_numSymbols];
            foreach (var x in _index)
            {
                if (_seen[x]) return false;
                _seen[x] = true;
            }
            return true;
        }
    }
}
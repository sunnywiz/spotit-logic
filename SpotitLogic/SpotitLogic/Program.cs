using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolRepeat
{
    class Program
    {
        private static List<char> _availableSymbols;
        private static int _availableSymbolCount;
        private static int _numPerCard;
        private static List<Dictionary<char, bool>> _cardsSoFar;
        private static List<int> _index;

        static void Main(string[] args)
        {
            _availableSymbols = new List<char>() { 'A', 'B', 'C', 'D', 'E' };
            _availableSymbolCount = _availableSymbols.Count;

            _numPerCard = 2;

            _cardsSoFar = new List<Dictionary<char, bool>>();

            _index = new List<int>();
            while (_index.Count < _numPerCard)
            {
                _index.Add(0);
            }

            try
            {
                do
                {
                    // Console.WriteLine("Consider: "+Dumpit());
                    if (IsValidSymbol())
                    {
                        // Console.WriteLine("Valid"); 

                        // must have exactly one symbol matching with every pervious card so far
                        if (IsANewCardWithExactlyOneMatchWithAll())
                        {
                            AddNewCardToPile();
                            Console.WriteLine("Added " + Dumpit());
                        }
                    }
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
            var arewegood = true;

            foreach (var card in _cardsSoFar)
            {
                var numberOfMatches = 0;
                foreach (var i in _index)
                {
                    var ch = _availableSymbols[i];
                    if (card.ContainsKey(ch)) numberOfMatches++;
                    if (numberOfMatches > 1)
                    {
                        // Console.WriteLine("** too many matches with "+String.Join("",card.Keys));
                        return false;
                    }
                }
                if (numberOfMatches == 0)
                {
                    // Console.WriteLine("** 0 matches with "+String.Join("", card.Keys));
                    return false;
                }
            }
            return arewegood;
        }

        private static void AddNewCardToPile()
        {
            var matchyness = new Dictionary<char, bool>();
            foreach (var i in _index)
            {
                matchyness[_availableSymbols[i]] = true;
            }
            _cardsSoFar.Add(matchyness);
        }

        public static string Dumpit()
        {
            var sb = new StringBuilder();
            foreach (var x in _index)
            {
                sb.Append(_availableSymbols[x]);
            }
            return sb.ToString();
        }

        private static bool AdvanceIndex()
        {
            var x = _index.Count - 1;
            while (x >= 0)
            {
                _index[x]++;
                if (_index[x] >= _availableSymbolCount)
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
            Dictionary<int, bool> seen = new Dictionary<int, bool>();
            foreach (var x in _index)
            {
                if (seen.ContainsKey(x)) return false;
                seen[x] = true;
            }
            return true;
        }
    }
}
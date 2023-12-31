﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestExercise
{
    public class ATMModel
    {
        private Dictionary<Banknote, int> _banknotes = new Dictionary<Banknote, int>();
        private const int maxCountPerBanknote = 100;
        public ATMModel() 
        {
            _banknotes.Add(new Banknote(10, Colors.Aqua), 50);
            _banknotes.Add(new Banknote(50, Colors.Blue), 50);
            _banknotes.Add(new Banknote(100, Colors.Yellow), 50);
            _banknotes.Add(new Banknote(1000, Colors.Red), 50);
        }
        public IEnumerable<Banknotes> GetBanknotes()
        {
            List<Banknotes> banknotes = new List<Banknotes>();
            foreach (var b in _banknotes)
            {
                banknotes.Add(new Banknotes(b.Key.Value, b.Value, b.Key.Color));
            }
            return banknotes;
        }
        public int GetBalance(bool maxBalance)
        {
            if(maxBalance)
            {
                return _banknotes.Sum(x => x.Key.Value * maxCountPerBanknote);
            }
            return _banknotes.Sum(x => x.Key.Value * x.Value);
        }
        public HashSet<Banknotes> AcceptMoney(ref int requestedAmount)
        {
            HashSet<Banknotes> banknotes = new HashSet<Banknotes>();
            if (requestedAmount % 10 != 0)
            {
                return banknotes;
            }
            foreach (var b in _banknotes.Keys.OrderByDescending(x => x.Value))
            {
                while (_banknotes[b] < maxCountPerBanknote && requestedAmount >= b.Value)
                {
                    _banknotes[b]++;
                    requestedAmount -= b.Value;
                    if (!banknotes.Contains(b, new ComparerBanknote()))
                    {
                        banknotes.Add(new Banknotes(b.Value, 1, b.Color));
                    }
                    else
                    {
                        var historyB = banknotes.First(x => x.Value == b.Value);
                        historyB.Count++;
                    }
                }
            }
            return banknotes;
        }
        public HashSet<Banknotes> GiveAwayMoney(bool small, bool large, int requestedAmount)
        {
            HashSet<Banknotes> banknotes = new HashSet<Banknotes>();
            if(requestedAmount % 10 != 0)
            {
                return banknotes;
            }
            if (small)
            {
                int currentSum = 0;
                List<Banknote> collection = new List<Banknote>();
                foreach(var b in _banknotes.Keys.OrderBy(x => x.Value).ToArray())
                {
                    currentSum += b.Value * _banknotes[b];
                    collection.Insert(0, b);
                    if (requestedAmount <= currentSum)
                    {
                        break;
                    }
                }
                foreach (var b in collection)
                {
                    while (requestedAmount >= b.Value && _banknotes[b] > 0)
                    {
                        _banknotes[b]--;
                        requestedAmount -= b.Value;
                        if (!banknotes.Contains(b, new ComparerBanknote()))
                        {
                            banknotes.Add(new Banknotes(b.Value, 1, b.Color));
                        }
                        else
                        {
                            var historyB = banknotes.First(x => x.Value == b.Value);
                            historyB.Count++;
                        }
                    }
                }
            }
            else if (large)
            {
                foreach (var b in _banknotes.Keys.OrderByDescending(x => x.Value))
                {
                    while (requestedAmount >= b.Value && _banknotes[b] > 0)
                    {
                        _banknotes[b]--;
                        requestedAmount -= b.Value;
                        if (!banknotes.Contains(b, new ComparerBanknote()))
                        {
                            banknotes.Add(new Banknotes(b.Value, 1, b.Color));
                        }
                        else
                        {
                            var historyB = banknotes.First(x => x.Value == b.Value);
                            historyB.Count++;
                        }
                    }
                }
            }
            return banknotes;
        }
    }
}

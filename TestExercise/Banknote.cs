﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestExercise
{
    public class Banknote : BaseVM
    {
        public Banknote(int denomination, Color colorBanknote)
        {
            _value = denomination;
            _count = maxCount / 2;
            Color = colorBanknote;
        }
        public readonly uint maxCount = 100;
        public uint Count
        {
            get => _count;
            set
            {
                if(value > maxCount)
                {

                }
                else
                {
                    _count = value;
                    OnPropertyChanged();
                }
            }
        }
        private uint _count;
        public Brush ColorBanknote
        {
            get => new SolidColorBrush(Color);
        }
        public readonly Color Color;
        public int Value
        {
            get => _value;
        }
        private readonly int _value;
        
    }
}

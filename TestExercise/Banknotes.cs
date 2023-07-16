using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestExercise
{
    public class Banknotes : Banknote
    {
        public Banknotes(int denomination, int count, Color colorBanknote) : base(denomination, colorBanknote)
        {
            Count = count;
        }

        public int Count 
        { 
            get => count;
            set 
            { 
                count = value; 
                OnPropertyChanged(); 
            } 
        }
        private int count;
    }
}

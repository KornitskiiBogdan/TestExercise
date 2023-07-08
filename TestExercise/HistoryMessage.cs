using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestExercise
{
    public enum Operations
    {
        Add,
        Remove
    }
    public class HistoryMessage : BaseVM
    {
        public HistoryMessage(Operations operation, ICollection<Banknote> banknotes) 
        {
            _operation = operation;
            Banknotes = new ObservableCollection<Banknote>(banknotes);
        }
        public ObservableCollection<Banknote> Banknotes { get; } 
        public Operations EOperation { get => _operation; }
        private readonly Operations _operation;
    }
}

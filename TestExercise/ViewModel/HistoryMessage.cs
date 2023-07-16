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
        public HistoryMessage(Operations operation, ICollection<Banknotes> banknotes) 
        {
            _operation = operation;
            Banknotes = new ObservableCollection<Banknotes>(banknotes);
        }
        public ObservableCollection<Banknotes> Banknotes { get; } 
        public Operations EOperation { get => _operation; }
        private readonly Operations _operation;
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace TestExercise
{
    public enum EBalanceState
    {
        Low,
        Medium,
        High
    }
    public class ApplicationViewModel : BaseVM
    {
        public ICommand AcceptMoneyCommand { get; set; }
        public ICommand GiveAwayMoneyCommand { get; set; }
        public ApplicationViewModel()
        {
            Banknotes = new ObservableCollection<Banknote>()
            {
                new Banknote(10, Colors.Aqua), new Banknote(50, Colors.Blue), new Banknote(100, Colors.Yellow), new Banknote(1000, Colors.Red)
            };
            HistoryMessages = new ObservableCollection<HistoryMessage>();
            AcceptMoneyCommand = new RelayCommand((o) => { var accepthMoneyWindow = new AcceptMoneyWindow(this); accepthMoneyWindow.ShowDialog(); });
            GiveAwayMoneyCommand = new RelayCommand((o) => { var accepthMoneyWindow = new GiveAwayMoneyWindow(this); accepthMoneyWindow.ShowDialog(); });
        }
        public ObservableCollection<Banknote> Banknotes { get; private set; }
        public ObservableCollection<HistoryMessage> HistoryMessages { get; set; }
        public int Balance
        {
            get => (int)Banknotes.Sum(x => x.Value * x.Count);
        }
        public EBalanceState BalanceState 
        { 
            get 
            {
                var maxBalance = Banknotes.Sum(x => x.Value * x.maxCount);
                if(Balance < maxBalance / 2.0 && Balance > maxBalance / 3.0)
                {
                    return EBalanceState.Medium;
                }
                else if(Balance < maxBalance / 3.0)
                {
                    return EBalanceState.Low;
                }
                return EBalanceState.High;
            } 
        }
    }
}

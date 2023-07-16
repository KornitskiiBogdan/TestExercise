using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
            _atmModel = new ATMModel();
            HistoryMessages = new ObservableCollection<HistoryMessage>();
            AcceptMoneyCommand = new RelayCommand((o) => { var accepthMoneyWindow = new AcceptMoneyWindow(this); accepthMoneyWindow.ShowDialog(); });
            GiveAwayMoneyCommand = new RelayCommand((o) => { var accepthMoneyWindow = new GiveAwayMoneyWindow(this); accepthMoneyWindow.ShowDialog(); });
        }
        public ATMModel ATMModel => _atmModel;
        private readonly ATMModel _atmModel;
        public IEnumerable<Banknotes> Banknotes => _atmModel.GetBanknotes();
        public ObservableCollection<HistoryMessage> HistoryMessages { get; set; }
        public int Balance
        {
            get => _atmModel.GetBalance(false);
        }
        public EBalanceState BalanceState 
        { 
            get 
            {
                var maxBalance = _atmModel.GetBalance(true);
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

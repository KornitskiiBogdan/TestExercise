using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TestExercise
{
    /// <summary>
    /// Interaction logic for GiveAwayMoneyWindow.xaml
    /// </summary>
    public partial class GiveAwayMoneyWindow : Window
    {
        public GiveAwayMoneyWindow(ApplicationViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }
        private readonly ApplicationViewModel _viewModel;
        private void sumBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }

        private void sumBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(sumBox.Text, out int result) && result % 10 == 0)
            {
                if(result > _viewModel.Balance)
                {
                    MessageBox.Show($"В банкомате недостаточно денег, введите другую сумму", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    return;
                }
                HashSet<Banknotes> banknotes = new HashSet<Banknotes>();
                if (smallRadioButton.IsChecked ?? false)
                {
                    banknotes = _viewModel.ATMModel.GiveAwayMoney(true, false, result);
                }
                else if(largeRadioButton.IsChecked ?? false)
                {
                    banknotes = _viewModel.ATMModel.GiveAwayMoney(false, true, result);
                }
                MessageBox.Show($"Банкомат выдал{banknotes.Sum(x => x.Value * x.Count)}", "Банкомат", MessageBoxButton.OK, MessageBoxImage.Information);
                _viewModel.OnPropertyChanged(nameof(_viewModel.Balance));
                _viewModel.OnPropertyChanged(nameof(_viewModel.Banknotes));
                _viewModel.OnPropertyChanged(nameof(_viewModel.BalanceState));
                _viewModel.HistoryMessages.Add(new HistoryMessage(Operations.Remove, banknotes));
                DialogResult = true;

            }
            else
            {
                MessageBox.Show("Введите сумму кратную 10", "Неправильная сумма", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

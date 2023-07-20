using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TestExercise
{
    /// <summary>
    /// Interaction logic for AcceptMoneyWindow.xaml
    /// </summary>
    public partial class AcceptMoneyWindow : Window
    {
        public AcceptMoneyWindow(ApplicationViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }
        private readonly ApplicationViewModel _viewModel;
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(sumBox.Text, out int result) && result % 10 == 0)
            {
                result = _viewModel.AcceptMoney(result);
                if(result != 0)
                {
                    MessageBox.Show($"Банкомат переполнен, возьмите оставшиеся деньги {result}", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Введите сумму кратную 10", "Неправильная сумма", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

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
    }
}

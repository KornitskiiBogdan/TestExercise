using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                foreach(var b in _viewModel.Banknotes.Reverse())
                {
                    while(b.Count < b.maxCount && result >= b.Value)
                    {
                        b.Count++;
                        result -= b.Value;
                    }
                }
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

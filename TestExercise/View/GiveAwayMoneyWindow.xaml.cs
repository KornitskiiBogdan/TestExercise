using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
                HashSet<Banknote> banknotes = new HashSet<Banknote>();
                if (smallRadioButton.IsChecked ?? false)
                {
                    var collection = _viewModel.Banknotes.OrderBy(x => x.Value).ToArray();
                    bool returnBeginning = false;
                    bool toEnd = false;
                    for (int i = 0; i < collection.Length; i++)
                    {
                        var b = collection[i];
                        if (i == collection.Length - 1)
                        {
                            toEnd = true;
                        }
                        if (result > b.Value * b.Count && !toEnd)
                        {
                            returnBeginning = true;
                            continue;
                        }
                        while (result >= b.Value && b.Count > 0)
                        {
                            b.Count--;
                            result -= b.Value;
                            if (!banknotes.Contains(b, new ComparerBanknotes()))
                            {
                                banknotes.Add(new Banknote(b.Value, b.Color) { Count = 1 });
                            }
                            else
                            {
                                var historyB = banknotes.First(x => x.Value == b.Value);
                                historyB.Count++;
                            }
                        }
                        if(returnBeginning)
                        {
                            toEnd = true;
                            returnBeginning = false;
                            i = -1;
                        }
                        
                    }
                }
                else if(largeRadioButton.IsChecked ?? false)
                {
                    foreach (var b in _viewModel.Banknotes.OrderByDescending(x => x.Value))
                    {
                        while (result >= b.Value && b.Count > 0)
                        {
                            b.Count--;
                            result -= b.Value;
                            if (!banknotes.Contains(b, new ComparerBanknotes()))
                            {
                                banknotes.Add(new Banknote(b.Value, b.Color) { Count = 1 });
                            }
                            else
                            {
                                var historyB = banknotes.First(x => x.Value == b.Value);
                                historyB.Count++;
                            }
                        }
                    }
                }
                MessageBox.Show($"Банкомат выдал{banknotes.Sum(x => x.Value * x.Count)}", "Банкомат", MessageBoxButton.OK, MessageBoxImage.Information);
                _viewModel.OnPropertyChanged(nameof(_viewModel.Balance));
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

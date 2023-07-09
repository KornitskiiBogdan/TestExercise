using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TestExercise;
using Microsoft.Win32;

namespace Test1
{
    public class ApplicationViewModel : BaseVM
    {
        public ApplicationViewModel()
        {
            HistoryProcessings = new ObservableCollection<HistoryProcessing>();
        }
        public ObservableCollection<HistoryProcessing> HistoryProcessings { get; set; }
        public ICommand AddInputFile
        {
            get
            {
                return _addInputFile ?? (_addInputFile = new RelayCommand(o =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Text files (*.txt)|*.txt";
                    openFileDialog.Multiselect = true;
                    if (openFileDialog.ShowDialog() ?? false)
                    {
                        _filesInputPath = new List<string>(openFileDialog.FileNames);
                    }

                }));
            }
        }
        private ICommand? _addInputFile;
        public ICommand AddOutputFile
        {
            get
            {
                return _addOutputFile ?? (_addOutputFile = new RelayCommand(o =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Text files (*.txt)|*.txt";
                    openFileDialog.Multiselect = true;
                    if (openFileDialog.ShowDialog() ?? false)
                    {
                        _filesOutputPath = new List<string>(openFileDialog.FileNames);
                    }
                }));
            }
        }
        private ICommand? _addOutputFile;

        public ICommand CalculateCommand
        {
            get
            {
                return _calculateCommand ?? (_calculateCommand = new RelayCommand(async o =>
                {
                    if(_filesInputPath.Count != _filesOutputPath.Count)
                    {
                        MessageBox.Show("количество входных и выходных файлов должно быть одинаково. Выберите заново файлы", "Обработка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        for (int i = 0; i < _filesInputPath.Count; i++)
                        {
                            var inputFile = _filesInputPath[i];
                            var outputFile = _filesOutputPath[i];
                            var process = new HistoryProcessing(inputFile, outputFile);
                            if (inputFile == string.Empty || outputFile == string.Empty)
                            {
                                MessageBox.Show("Выберите файлы", "Обработка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else if (WorldLength == string.Empty)
                            {
                                MessageBox.Show("Введите длину строки", "Обработка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                HistoryProcessings.Add(process);
                                _listTasks.Add(Task.Run(() =>
                                {
                                    process.Status = WordProcessing.CalculateStatus(inputFile, outputFile, int.Parse(WorldLength), (bool)o);
                                }));
                            }
                        }
                        await Task.WhenAll(_listTasks.ToArray());
                        _listTasks.Clear();
                    }
                    _filesOutputPath.Clear();
                    _filesInputPath.Clear();
                }));
            }
        }
        private ICommand? _calculateCommand;
        public List<Task> _listTasks = new List<Task>();
        private List<string> _filesInputPath = new List<string>();
        private List<string> _filesOutputPath = new List<string>();


        public string WorldLength
        {
            get => _worldLength;
            set
            {
                if (int.TryParse(value, out _))
                {
                    _worldLength = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _worldLength = string.Empty;
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TestExercise;

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
                    if (openFileDialog.ShowDialog() ?? false)
                    {
                        FileOutputPath = openFileDialog.FileName;
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
                    foreach (var fileInput in _filesInputPath)
                    {
                        var process = new HistoryProcessing(fileInput, FileOutputPath);
                        if (fileInput == string.Empty || FileOutputPath == string.Empty)
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
                            process.Status = await WordProcessing.Calculate(fileInput, FileOutputPath, int.Parse(WorldLength), (bool)o);
                        }
                    }
                }));
            }
        }
        private ICommand? _calculateCommand;
        private List<string> _filesInputPath = new List<string>();
        public string FileOutputPath
        {
            get => _fileOutputPath;
            private set
            {
                _fileOutputPath = value;
                OnPropertyChanged();
            }
        }
        private string _fileOutputPath = string.Empty;


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

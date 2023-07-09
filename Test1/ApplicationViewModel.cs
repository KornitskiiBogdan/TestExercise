using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestExercise;

namespace Test1
{
    public class ApplicationViewModel : BaseVM
    {
        public ApplicationViewModel() 
        { 

        }
        public ICommand AddInputFile 
        { 
            get
            {
                return _addInputFile ?? (_addInputFile = new RelayCommand(o =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Text files (*.txt)|*.txt";
                    if (openFileDialog.ShowDialog() ?? false)
                    {
                        FileInputPath = openFileDialog.FileName;
                    }
                
                }));
            } 
        }
        private ICommand _addInputFile;
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
        private ICommand _addOutputFile;
        
        public ICommand CalculateCommand
        {
            get
            {
                return _calculateCommand ?? (_calculateCommand = new RelayCommand(async o =>
                {
                    if(FileInputPath == string.Empty  || FileOutputPath == string.Empty)
                    {
                        MessageBox.Show("Выберите файлы", "Обработка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else if(WorldLength == string.Empty)
                    {
                        MessageBox.Show("Введите длину строки", "Обработка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else if (_startPrevCommand)
                    {
                        MessageBox.Show("Идет обработка предыдущего файла, дождитесь окончания", "Обработка", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        _startPrevCommand = true;
                        await WordProcessing.Calculate(FileInputPath, FileOutputPath, int.Parse(WorldLength), (bool)o);
                        _startPrevCommand = false;
                    }
                }));
            }
        }
        private bool _startPrevCommand;
        private ICommand _calculateCommand;
        public string FileInputPath 
        { 
            get => _fileInputPath; 
            private set 
            { 
                _fileInputPath = value; 
                OnPropertyChanged(); 
            } 
        }
        private string _fileInputPath = string.Empty;
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
                if(int.TryParse(value, out _))
                {
                    _worldLength = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _worldLength = string.Empty;
    }
}

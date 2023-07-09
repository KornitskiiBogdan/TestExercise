using TestExercise;

namespace WordProcessing
{
    public enum EStatus
    {
        Failed,
        InProgress,
        Done
    }
    public class HistoryProcessing : BaseVM
    {
        public HistoryProcessing(string fileInput, string fileOutput) 
        {
            _status = EStatus.InProgress;
            FileInput = fileInput;
            FileOutput = fileOutput;
        }
        public string FileInput { get; }
        public string FileOutput { get; }
        public EStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }

        }
        private EStatus _status;
    }
}

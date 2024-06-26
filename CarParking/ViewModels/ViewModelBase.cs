using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarParking.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        private bool _disposed = false;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value, nameof(IsBusy)); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value, nameof(ErrorMessage)); }
        }

        protected void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            var viewModel = Activator.CreateInstance<TViewModel>();
            // Navigate to the view associated with the view model
        }

        protected void GoBack()
        {
            // Navigate back to the previous view
        }

        public virtual void Initialize()
        {
            // Load initial data or perform other initialization tasks
        }

        protected virtual bool Validate()
        {
            return false;
        }

        protected virtual void ShowValidationErrors()
        {
            // Display validation errors to the user
        }



        //protected ICommand CreateCommand(Action execute)
        //{
        //    return new RelayCommand(execute);
        //}

        //protected ICommand CreateCommand<T>(Action<T> execute)
        //{
        //    return new RelayCommand<T>(execute);
        //}

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Clean up managed resources
            }

            // Clean up unmanaged resources

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ViewModelBase()
        {
            Dispose(false);
        }

    }
}

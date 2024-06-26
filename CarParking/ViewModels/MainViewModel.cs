using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Common;
using Common.TestData;
using UseCase;

namespace CarParking.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ParkingSlot> _parkingSlotItem;
        private TestRepository _tesRepository;
        private ParkingSlotBooking _parkingSlotBooking;

        private ObservableCollection<ParkingHistory> _parkingHistories;
        public ObservableCollection<ParkingHistory> ParkingHistories
        {
            get => _parkingHistories;
            set => SetProperty(ref _parkingHistories, value);
        }

        private int _remainingSlots = 0;
        public int RemainingSlots
        {
            get => _remainingSlots;
            set => SetProperty(ref _remainingSlots, value);
        }

        private ObservableCollection<string> _cars;
        public ObservableCollection<string> Cars
        {
            get => _cars;
            set => SetProperty(ref _cars, value);
        }

        private ObservableCollection<EmployeeRegistration> _employeeRegistrations;
        public ObservableCollection<EmployeeRegistration> EmployeeRegistrations
        {
            get { return _employeeRegistrations; }
            set => SetProperty(ref _employeeRegistrations, value);
        }

        public ObservableCollection<ParkingSlot> ParkingSlotCollection
        {
            get => _parkingSlotItem;
            set => SetProperty(ref _parkingSlotItem, value);
        }

        private string _entryCarNo;
        public string EntryCarNo
        {
            get => _entryCarNo;
            set => SetProperty(ref _entryCarNo, value);
        }

        private string _exitCarNo;
        public string ExitCarNo
        {
            get => _exitCarNo;
            set => SetProperty(ref _exitCarNo, value);
        }

        private string _currentAllocatedSlot;
        public string CurrentAllocatedSlot
        {
            get => _currentAllocatedSlot;
            set => SetProperty(ref _currentAllocatedSlot, value);
        }


        public ICommand AddAllotmentCommand { get; }
        public ICommand RemoveAllotmentCommand { get; }

        public MainViewModel()
        {
            ParkingSlotCollection = new ObservableCollection<ParkingSlot>();
            AddAllotmentCommand = new RelayCommand(AllotSlot);
            RemoveAllotmentCommand = new RelayCommand(RemoveAllotment);
            InitializeRepository();
            InitializeGridItems();

            RemainingSlots = _tesRepository.ParkingSlots.Where(x => !x.IsOccupied).Count();
            Cars = new ObservableCollection<string>(_tesRepository.EmployeeRegistrations.Select(x => x.CarNo));
            EmployeeRegistrations =
                new ObservableCollection<EmployeeRegistration>(_tesRepository.EmployeeRegistrations);
        }

        private void InitializeRepository()
        {
            _tesRepository = new TestRepository();
            _parkingSlotBooking = new ParkingSlotBooking();
        }

        private void InitializeGridItems()
        {
            ParkingSlotCollection = new ObservableCollection<ParkingSlot>(_tesRepository.ParkingSlots);
        }

        private void AllotSlot()
        {
            var relatimeParkedData = _parkingSlotBooking.BookParkingSlot(EntryCarNo, _tesRepository);

            if (string.IsNullOrWhiteSpace(relatimeParkedData.Message))
            {
                CurrentAllocatedSlot = relatimeParkedData.ParkingSlotNo;
            }

            RemainingSlots = _tesRepository.ParkingSlots.Where(x => !x.IsOccupied).Count();
            ParkingHistories = new ObservableCollection<ParkingHistory>(_tesRepository.ParkingHistory);

            ////Get slot no by passing the input
            //if (_counter == 12) return;

            //_counter++;


            ////Assign Class to slot
            //var slot = ParkingSlotCollection.FirstOrDefault(x => x.SlotNo == _counter.ToString());
            //if (slot != null)
            //{
            //    slot.IsOccupied = true;
            //   // slot.AllottedCarNo = _entryCarNo;
            //    _entryCarNo = String.Empty; ;
            //}


        }

        private void RemoveAllotment()
        {

            _parkingSlotBooking.ReleaseParkingSlot(ExitCarNo, _tesRepository);
            RemainingSlots = _tesRepository.ParkingSlots.Where(x => !x.IsOccupied).Count();
            ParkingHistories = new ObservableCollection<ParkingHistory>(_tesRepository.ParkingHistory);

            //var slot = ParkingSlotCollection.FirstOrDefault(x => x.AllottedCarNo.Equals(_exitCarNo));
            //if (slot != null)
            //{
            //    slot.IsOccupied = false;
            //   // slot.AllottedCarNo = String.Empty;
            //}
        }
    }
}

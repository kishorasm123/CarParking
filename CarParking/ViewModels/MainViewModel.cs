using System;
using System.Collections.ObjectModel;
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
            private int _counter = 0;


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


            public ICommand AddAllotmentCommand { get; }
            public ICommand RemoveAllotmentCommand { get; }

            public MainViewModel()
            {
                ParkingSlotCollection = new ObservableCollection<ParkingSlot>();
                AddAllotmentCommand = new RelayCommand(AllotSlot);
                RemoveAllotmentCommand = new RelayCommand(RemoveAllotment);
                InitializeRepository();
                InitializeGridItems();
            }

            private void InitializeRepository()
            {
                _tesRepository = new TestRepository();
                _parkingSlotBooking = new ParkingSlotBooking();
            }


            private void InitializeGridItems()
            {
                for (int i = 1; i <= 12; i++) // 4 rows * 3 columns
                {
                    ParkingSlotCollection.Add(new ParkingSlot() { IsOccupied = true, SlotNo = i.ToString() });
                }
            }

            private void AllotSlot()
            {
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
                //var slot = ParkingSlotCollection.FirstOrDefault(x => x.AllottedCarNo.Equals(_exitCarNo));
                //if (slot != null)
                //{
                //    slot.IsOccupied = false;
                //   // slot.AllottedCarNo = String.Empty;
                //}
            }
        }
}

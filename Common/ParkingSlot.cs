using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ParkingSlot : ModelBase
    {
        private string _slotNo;
        private string _carNo;
        private bool _isOccupied;
        private EnumParkingPreferences _preferenceMap;

        public string SlotNo
        {
            get { return _slotNo; }
            set
            {
                _slotNo = value;
                OnPropertyChanged();
            }
        }

        public bool IsOccupied
        {
            get { return _isOccupied; }
            set
            {
                _isOccupied = value;
                OnPropertyChanged();
            }
        }

        public string CarNo
        {
            get { return _carNo; }
            set
            {
                _carNo = value;
                OnPropertyChanged();
            }
        }

        public EnumParkingPreferences PreferenceMap
        {
            get { return _preferenceMap; }
            set
            {
                _preferenceMap = value;
                OnPropertyChanged();
            }
        }


    }
}

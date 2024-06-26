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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ParkingSlot
    {
        public String SlotNo { get; set; }
        public bool IsOccupied { get; set; }
        public EnumParkingPreferences PreferenceMap { get; set; }

        
    }
}

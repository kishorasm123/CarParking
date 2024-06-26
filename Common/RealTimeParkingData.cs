using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RealTimeParkingData
    {
        public int ParkingId { get; set; }

        public string ParkingSlotNo { get; set; }
        public string CarNo { get; set; }

        public EmployeeRegistration EmployeeRegistration { get; set; }
        public ParkingSlot ParkingSlot { get; set; }

        public RealTimeParkingData(string parkingSlotNo, string carNo)
        {
            ParkingSlotNo = parkingSlotNo;
            CarNo = carNo;
        }
    }
}

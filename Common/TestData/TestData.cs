using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TestData
{
    public class TestData
    {
        private static ICollection<EmployeeRegistration> _employeeRegistrations;
        private static List<ParkingSlot> _parkingSlots;

        public ICollection<EmployeeRegistration> EmployeeRegistrations
        {
            get { return _employeeRegistrations; }
            set { _employeeRegistrations = value; }
        }

        public List<ParkingSlot> ParkingSlot
        {
            get { return _parkingSlots; }
            set { _parkingSlots = value; }
        }


        static TestData()
        {
            _employeeRegistrations = new List<EmployeeRegistration>()
            {
                new EmployeeRegistration()
                {
                    EmployeeId = "E1",
                    CarNo = "C1",
                    EmployeeName = "Ashin",
                    ParkingPreferences = new List<EnumParkingPreferences>() { EnumParkingPreferences.NearEVChargeStation ,EnumParkingPreferences.NearLift}
                },

                new EmployeeRegistration()
                {
                    EmployeeId = "E2",
                    CarNo = "C2",
                    EmployeeName = "Kishor",
                    ParkingPreferences = new List<EnumParkingPreferences>() { EnumParkingPreferences.NearEVChargeStation}
                },

                new EmployeeRegistration()
                {
                    EmployeeId = "E3",
                    CarNo = "C3",
                    EmployeeName = "Yogesh",
                    ParkingPreferences = new List<EnumParkingPreferences>() { EnumParkingPreferences.NearEntry}
                },

                new EmployeeRegistration()
                {
                    EmployeeId = "E4",
                    CarNo = "C4",
                    EmployeeName = "Praveen",
                    ParkingPreferences = new List<EnumParkingPreferences>() { EnumParkingPreferences.NearEntry}
                },

                new EmployeeRegistration()
                {
                    EmployeeId = "E5",
                    CarNo = "C5",
                    EmployeeName = "Jay",
                    ParkingPreferences = new List<EnumParkingPreferences>() { EnumParkingPreferences.NearExit}
                }

            };

            _parkingSlots = new List<ParkingSlot>(12)
            {
                new ParkingSlot()
                {
                    IsOccupied = false,
                    PreferenceMap = EnumParkingPreferences.General,
                    SlotNo = "S1"
                },
                new ParkingSlot()
                {
                    IsOccupied = false,
                    PreferenceMap = EnumParkingPreferences.General,
                    SlotNo = "S2"
                },
                new ParkingSlot()
                {
                    IsOccupied = false,
                    PreferenceMap = EnumParkingPreferences.General,
                    SlotNo = "S3"
                },
                new ParkingSlot()
                {
                    IsOccupied = false,
                    PreferenceMap = EnumParkingPreferences.General,
                    SlotNo = "S4"
                },
                new ParkingSlot()
                {
                    IsOccupied = false,
                    PreferenceMap = EnumParkingPreferences.General,
                    SlotNo = "S5"
                },
                new ParkingSlot()
                {
                    IsOccupied = false,
                    PreferenceMap = EnumParkingPreferences.General,
                    SlotNo = "S6"
                },
                new ParkingSlot()
                {
                    IsOccupied = false,
                    PreferenceMap = EnumParkingPreferences.General,
                    SlotNo = "S7"
                },
                new ParkingSlot()
                {
                    IsOccupied = false,
                    PreferenceMap = EnumParkingPreferences.General,
                    SlotNo = "S8"
                },
                new ParkingSlot()
                {
                    IsOccupied = false,
                    PreferenceMap = EnumParkingPreferences.General,
                    SlotNo = "S9"
                },
                new ParkingSlot()
                {
                    IsOccupied = false,
                    PreferenceMap = EnumParkingPreferences.General,
                    SlotNo = "S10"
                },
                new ParkingSlot()
                {
                    IsOccupied = false,
                    PreferenceMap = EnumParkingPreferences.General,
                    SlotNo = "S11"
                },
                new ParkingSlot()
                {
                    IsOccupied = false,
                    PreferenceMap = EnumParkingPreferences.General,
                    SlotNo = "S12"
                },
            };
        }


    }
}

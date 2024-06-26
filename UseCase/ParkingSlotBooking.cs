using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.TestData;

namespace UseCase
{
    public class ParkingSlotBooking
    {
        public RealTimeParkingData BookParkingSlot(string carNo, TestRepository testDataRepository)
        {
            RealTimeParkingData realTimeParkingData = new RealTimeParkingData();

            if (testDataRepository.ParkingSlot.Any(x => !x.IsOccupied))
            {
                realTimeParkingData.Message = "Parking is full";
                return realTimeParkingData;
            }

            var employeeRegistration = testDataRepository.EmployeeRegistrations.First(x => x.CarNo == carNo);

            // 1. Based on preference.
            var employeePreferredSlots = testDataRepository.ParkingSlot.Where(x =>
               !x.IsOccupied && employeeRegistration.ParkingPreferences.Contains(x.PreferenceMap));
            if (employeePreferredSlots.Any())
            {
                realTimeParkingData.ParkingSlot = employeePreferredSlots.First();
                realTimeParkingData.EmployeeRegistration = employeeRegistration;
                realTimeParkingData.ParkingSlot.IsOccupied = true;
            }

            // 2. Based on parked history.
            testDataRepository.ParkingHistory.Where(x => x.CarNo == employeeRegistration.CarNo);


            testDataRepository.RealTimeParkingData.Add(realTimeParkingData);
            testDataRepository.ParkingHistory.Add(new ParkingHistory()
            {
                CarNo = employeeRegistration.CarNo,
                InTime = DateTime.Now,
                ParkingSlotNo = realTimeParkingData.ParkingSlotNo
            });

            return realTimeParkingData;
        }
    }
}

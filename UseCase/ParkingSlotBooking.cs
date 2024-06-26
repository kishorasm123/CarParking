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
            bool isParkingDone = false;
            RealTimeParkingData realTimeParkingData = new RealTimeParkingData();

            var currentAvailableSlots = testDataRepository.ParkingSlots.Where(x => !x.IsOccupied);

            if (currentAvailableSlots == null || !currentAvailableSlots.Any())
            {
                realTimeParkingData.Message = "Parking is full";
                return realTimeParkingData;
            }

            var employeeRegistration = testDataRepository.EmployeeRegistrations.First(x => x.CarNo == carNo);

            // 1. Based on preference.
            var unOccupiedEmployeePreferredSlots = currentAvailableSlots.Where(x => employeeRegistration.ParkingPreferences.Contains(x.PreferenceMap));

            if (unOccupiedEmployeePreferredSlots.Any())
            {
                realTimeParkingData.ParkingSlot = unOccupiedEmployeePreferredSlots.First();
                realTimeParkingData.EmployeeRegistration = employeeRegistration;
                realTimeParkingData.ParkingSlot.IsOccupied = true;
            }

            // 2. Based on parked history.
            if (!isParkingDone)
            {
                var parkingHistories = testDataRepository.ParkingHistory.Where(x => x.CarNo == employeeRegistration.CarNo && x.InTime.HasValue)
                                                 .OrderByDescending(x => x.InTime);
                // 1. Based on yesterday parked history.
                if (parkingHistories.Any() && currentAvailableSlots.Any(x=> parkingHistories.First().ParkingSlotNo == x.SlotNo))
                {

                }
            }


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

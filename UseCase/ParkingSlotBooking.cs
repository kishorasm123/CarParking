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

            realTimeParkingData.ParkingEntryTime = DateTime.Now;

            var employeeRegistration = testDataRepository.EmployeeRegistrations.First(x => x.CarNo == carNo);

            var unOccupiedEmployeePreferredSlots = currentAvailableSlots.Where(x => employeeRegistration.ParkingPreferences.Contains(x.PreferenceMap));
            var parkingHistories = testDataRepository.ParkingHistory.Where(x => x.CarNo == employeeRegistration.CarNo && x.InTime.HasValue)
                .OrderByDescending(x => x.InTime);

            // 2. Based on parked history.
            if (parkingHistories.Any())
            {
                var lastParkedSlotAvailable = unOccupiedEmployeePreferredSlots.Where(x => parkingHistories.First().ParkingSlotNo == x.SlotNo);

                // 1. Based on last parked slot history + preference.
                if (lastParkedSlotAvailable.Any())
                {
                    realTimeParkingData.ParkingSlot = lastParkedSlotAvailable.First();
                    realTimeParkingData.EmployeeRegistration = employeeRegistration;
                    realTimeParkingData.ParkingSlot.IsOccupied = true;
                }

                // 2. Based on most parked slot history.
                else
                {
                    var mostFrequentSlot = parkingHistories
                        .GroupBy(ph => ph.ParkingSlotNo)
                        .OrderByDescending(g => g.Count())
                        .FirstOrDefault()?.Key;

                    var mostFrequentlyParkedSlotAvailablity =
                        unOccupiedEmployeePreferredSlots.Where(x => x.SlotNo == mostFrequentSlot);

                    if (mostFrequentlyParkedSlotAvailablity.Any())
                    {
                        realTimeParkingData.ParkingSlot = mostFrequentlyParkedSlotAvailablity.First();
                        realTimeParkingData.EmployeeRegistration = employeeRegistration;
                        realTimeParkingData.ParkingSlot.IsOccupied = true;
                    }
                }

                // Allocating to any avaialable slot if not parked.
                if (realTimeParkingData.ParkingSlot == null)
                {
                    realTimeParkingData.ParkingSlot = unOccupiedEmployeePreferredSlots.First();
                    realTimeParkingData.EmployeeRegistration = employeeRegistration;
                    realTimeParkingData.ParkingSlot.IsOccupied = true;
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

        public bool ReleaseParkingSlot(string carNo, TestRepository testDataRepository)
        {
            var parkedData = testDataRepository.RealTimeParkingData.First(x => x.CarNo == carNo);
            var parkingSlot = testDataRepository.ParkingSlots.First(x => x.SlotNo == parkedData.ParkingSlotNo);

            parkingSlot.IsOccupied = false;

            testDataRepository.ParkingHistory.Add(new ParkingHistory()
            {
                CarNo = parkedData.CarNo,
                InTime = DateTime.Now,
                ParkingSlotNo = parkedData.ParkingSlotNo
            });

            return true;
        }
    }
}

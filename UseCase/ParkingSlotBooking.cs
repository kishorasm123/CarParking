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

            if (testDataRepository.RealTimeParkingData.Any(x => x.CarNo == carNo))
            {
                realTimeParkingData.Message = "Already parked";
                return realTimeParkingData;
            }

            var currentAvailableSlots = testDataRepository.ParkingSlots.Where(x => !x.IsOccupied);

            if (currentAvailableSlots == null || !currentAvailableSlots.Any())
            {
                realTimeParkingData.Message = "Parking is full";
                return realTimeParkingData;
            }

            realTimeParkingData.ParkingEntryTime = DateTime.Now;

            var employeeRegistration = testDataRepository.EmployeeRegistrations.First(x => x.CarNo == carNo);

            //var unOccupiedEmployeePreferredSlots = currentAvailableSlots.Where(x => employeeRegistration.ParkingPreferences.Contains(x.PreferenceMap));

            // Create a lookup dictionary for the preference order
            var preferenceOrder = employeeRegistration.ParkingPreferences
                .Select((preference, index) => new { preference, index })
                .ToDictionary(p => p.preference, p => p.index);

            // Filter and order the available slots based on the preference order
            var unOccupiedEmployeePreferredSlots = currentAvailableSlots
                .Where(x => employeeRegistration.ParkingPreferences.Contains(x.PreferenceMap))
                .OrderBy(x => preferenceOrder[x.PreferenceMap])
                .ToList();


            var parkingHistories = testDataRepository.ParkingHistory.Where(x => x.CarNo == employeeRegistration.CarNo && x.InTime.HasValue)
                .OrderByDescending(x => x.InTime);

            // 2. Based on parked history.
            if (parkingHistories.Any() && unOccupiedEmployeePreferredSlots.Any())
            {
                var lastParkedSlotIsUserPrefferedAndAvailable = unOccupiedEmployeePreferredSlots.Where(x => parkingHistories.First().ParkingSlotNo == x.SlotNo);

                // 1. Based on last parked slot history + preference.
                if (lastParkedSlotIsUserPrefferedAndAvailable.Any() && lastParkedSlotIsUserPrefferedAndAvailable.First() == unOccupiedEmployeePreferredSlots.First())
                {
                    realTimeParkingData.ParkingSlot = lastParkedSlotIsUserPrefferedAndAvailable.First();
                }

                // 2. Based on most parked slot history.
                else
                {
                    var mostFrequentSlots = parkingHistories
                        .GroupBy(ph => ph.ParkingSlotNo)
                        .OrderByDescending(g => g.Count())
                        .ToDictionary(g => g.Key, g => g.Count());

                    var slotsParkedByEmployeeMostTimes = mostFrequentSlots.Where(x =>
                        unOccupiedEmployeePreferredSlots.Select(y => y.SlotNo).Contains(x.Key));

                    var mostUsedPreferredSlot = mostFrequentSlots.OrderByDescending(x => x.Value).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(mostUsedPreferredSlot.Key))
                    {
                        var mostFrequentlyParkedSlotAvailablity = unOccupiedEmployeePreferredSlots
                                                .FirstOrDefault(x => x.SlotNo == mostUsedPreferredSlot.Key);

                        if (mostFrequentlyParkedSlotAvailablity != null)
                        {
                            realTimeParkingData.ParkingSlot = mostFrequentlyParkedSlotAvailablity;
                        }
                    }

                    //var mostFrequentlyParkedSlotAvailablity =
                    //    unOccupiedEmployeePreferredSlots.Where(x => x.SlotNo == mostFrequentSlotKey.First());

                    //if (mostFrequentlyParkedSlotAvailablity.Any())
                    //{
                    //    realTimeParkingData.ParkingSlot = mostFrequentlyParkedSlotAvailablity.First();
                    //}
                }

                // Allocating to any avaialable slot if not parked.
                if (realTimeParkingData.ParkingSlot == null)
                {
                    realTimeParkingData.ParkingSlot = unOccupiedEmployeePreferredSlots.First();
                }
            }

            // Based on employee preffered slot.
            else if (unOccupiedEmployeePreferredSlots.Any())
            {
                if (unOccupiedEmployeePreferredSlots.First().PreferenceMap == EnumParkingPreferences.NearEntry)
                {
                    var min = unOccupiedEmployeePreferredSlots.Where(x => x.PreferenceMap == EnumParkingPreferences.NearEntry).Min(y => y.SlotNo);
                    realTimeParkingData.ParkingSlot = unOccupiedEmployeePreferredSlots.First(x => x.SlotNo == min);
                }

                else if (unOccupiedEmployeePreferredSlots.First().PreferenceMap == EnumParkingPreferences.NearExit)
                {
                    var max = unOccupiedEmployeePreferredSlots.Where(x => x.PreferenceMap == EnumParkingPreferences.NearExit).Max(y => y.SlotNo);
                    realTimeParkingData.ParkingSlot = unOccupiedEmployeePreferredSlots.First(x => x.SlotNo == max);
                }

                else
                {
                    realTimeParkingData.ParkingSlot = unOccupiedEmployeePreferredSlots.First();
                }
            }

            // Allocating to any available slot if not parked.
            if (realTimeParkingData.ParkingSlot == null)
            {
                realTimeParkingData.ParkingSlot = currentAvailableSlots.First();
            }

            realTimeParkingData.EmployeeRegistration = employeeRegistration;
            realTimeParkingData.ParkingSlot.IsOccupied = true;
            realTimeParkingData.ParkingSlot.CarNo = employeeRegistration.CarNo;

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
            if (!testDataRepository.RealTimeParkingData.Any(x => x.CarNo == carNo))
            {
                return false;
            }

            var parkedData = testDataRepository.RealTimeParkingData.First(x => x.CarNo == carNo);
            var parkingSlot = testDataRepository.ParkingSlots.First(x => x.SlotNo == parkedData.ParkingSlotNo);

            parkingSlot.IsOccupied = false;
            parkingSlot.CarNo = String.Empty;

            testDataRepository.RealTimeParkingData.Remove(parkedData);
            testDataRepository.ParkingHistory.Add(new ParkingHistory()
            {
                CarNo = parkedData.CarNo,
                OutTime = DateTime.Now,
                ParkingSlotNo = parkedData.ParkingSlotNo
            });

            return true;
        }
    }
}

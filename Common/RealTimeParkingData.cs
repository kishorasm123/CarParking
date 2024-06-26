﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RealTimeParkingData : ModelBase
    {
        private int _parkingId;
        private string _parkingSlotNo;
        private string _carNo;
        private EmployeeRegistration _employeeRegistration;
        private ParkingSlot _parkingSlot;

        public DateTime? ParkingEntryTime = null;
        public DateTime? ParkingExitTime = null;

        public int ParkingId
        {
            get { return _parkingId; }
            set
            {
                _parkingId = value;
                OnPropertyChanged();
            }
        }

        public string ParkingSlotNo
        {
            get { return _parkingSlotNo; }
            private set
            {
                _parkingSlotNo = value;
                OnPropertyChanged();
            }
        }

        public string CarNo
        {
            get { return _carNo; }
            private set
            {
                _carNo = value;
                OnPropertyChanged();
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public EmployeeRegistration EmployeeRegistration
        {
            get { return _employeeRegistration; }
            set
            {
                _employeeRegistration = value;

                if (_employeeRegistration == null)
                {
                    CarNo = String.Empty;
                }
                else
                {
                    CarNo = _employeeRegistration.CarNo;
                }
                OnPropertyChanged();
            }
        }

        public ParkingSlot ParkingSlot
        {
            get { return _parkingSlot; }
            set
            {
                _parkingSlot = value;

                if (_parkingSlot == null)
                {
                    ParkingSlotNo = String.Empty;
                }
                else
                {
                    ParkingSlotNo = _parkingSlot.SlotNo;
                }
                OnPropertyChanged();
            }
        }


    }
}

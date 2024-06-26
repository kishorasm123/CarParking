using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EmployeeRegistration : ModelBase
    {
        private int _registrationId;
        private string _employeeId;
        private string _employeeName;
        private string _carNo;
        private ICollection<EnumParkingPreferences> _parkingPreferences;

        public int RegistrationId
        {
            get { return _registrationId; }
            set
            {
                _registrationId = value;
                OnPropertyChanged();
            }
        }

        public string EmployeeId
        {
            get { return _employeeId; }
            set
            {
                _employeeId = value;
                OnPropertyChanged();
            }
        }

        public string EmployeeName
        {
            get { return _employeeName; }
            set
            {
                _employeeName = value;
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

        public ICollection<EnumParkingPreferences> ParkingPreferences
        {
            get { return _parkingPreferences; }
            set
            {
                _parkingPreferences = value;
                OnPropertyChanged();
            }
        }

        // Read-only property to convert ParkingPreferences to string
        public string ParkingPreferencesString
        {
            get
            {
                return string.Join(", ", ParkingPreferences.Select(p => p.ToString()));
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EmployeeRegistration
    {
        public int RegistrationId { get; set; }
        public string EmployeeId { get; set; }
        public String EmployeeName { get; set; }
        public String CarNo { get; set; }
        public ICollection<EnumParkingPreferences> ParkingPreferences { get; set; }

        public EmployeeRegistration()
        {
        }
    }
}

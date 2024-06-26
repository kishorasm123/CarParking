using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ParkingHistory : ModelBase
    {
        public int HistoryId { get; set; }

        public string CarNo { get; set; }
        public string ParkingSlotNo { get; set; }

        public DateTime? InTime { get; set; } = null;
        public DateTime? OutTime { get; set; } = null;
    }
}

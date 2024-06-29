using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.LeaveRequestService
{
    public class LeaveRequestDTO
    {
        public int employeeId { get; set; }
        public int leaveTypeId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string requestComments { get; set; }

    }
}

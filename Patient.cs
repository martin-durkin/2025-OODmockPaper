using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xaml_practice
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string ContactNumber { get; set; }


        //one patient csan have many appointments
        public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

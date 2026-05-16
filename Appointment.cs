using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xaml_practice
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string AppointmentNotes { get; set; }


        //fk for one to many relationship with patient
        public int PatientId { get; set; }

        //navigation property for one to many relationship with patient
        public virtual Patient Patient { get; set; }
    }
}

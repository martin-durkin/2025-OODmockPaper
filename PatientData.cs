using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xaml_practice
{
    public class PatientData: DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public PatientData() : base("PatientData")
        {
        }
    }
}

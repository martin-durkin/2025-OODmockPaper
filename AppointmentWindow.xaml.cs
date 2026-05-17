using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace xaml_practice
{
    /// <summary>
    /// Interaction logic for AppointmentWindow.xaml
    /// </summary>
    public partial class AppointmentWindow : Window
    {
        private PatientData db = new PatientData();
        private int _patientId;
        private int _appointmentId;

        public AppointmentWindow()
        {
            InitializeComponent();
        }

        //constructor that takes patient id as parameter
        public AppointmentWindow(int patientId) : this()
        {
            _patientId = patientId;
        }

        //constructor for editing existing appointment
        public AppointmentWindow(int patientId, int appointmentId) : this()
        {
            _patientId = patientId;
            _appointmentId = appointmentId;

            //load appointment details from db
            var appointment = db.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                dpAppointmentDate.SelectedDate = appointment.AppointmentTime;
                tbxNotes.Text = appointment.AppointmentNotes;
            }
        }

        //clears textbox when selected
        private void tbx_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            tbx.Text = "";
        }

        //addsd appointment to db
        private void btnAddAppointment_Click(object sender, RoutedEventArgs e)
        {
            //read data from screen
            DateTime date = dpAppointmentDate.SelectedDate.Value;
            string notes = tbxNotes.Text;

            //create new appointment object
            Appointment newAppointment = new Appointment()
            {
                AppointmentTime = date,
                AppointmentNotes = notes,
                PatientId = _patientId
            };

            //save to db
            db.Appointments.Add(newAppointment);
            db.SaveChanges();

            //messagebox to show appointment added
            MessageBox.Show("Appointment added successfully!");

            //refresh main window and close
            MainWindow main = this.Owner as MainWindow;
            main.LoadPatients();
            this.Close();
        }


        //updates existing appointment in db
        private void btnUpdateAppointment_Click(object sender, RoutedEventArgs e)
        {
            //find appointment in db
            var appointment = db.Appointments.Find(_appointmentId);

            if (appointment != null)
            {
                //update appointment details
                appointment.AppointmentTime = dpAppointmentDate.SelectedDate.Value;
                appointment.AppointmentNotes = tbxNotes.Text;
                //save changes to db
                db.SaveChanges();
                //messagebox to show appointment updated
                MessageBox.Show("Appointment updated successfully!");
                //refresh main window and close
                MainWindow main = this.Owner as MainWindow;
                main.LoadPatients();
                this.Close();
            }
        }


    }
}

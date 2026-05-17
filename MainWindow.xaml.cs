using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Data.Entity;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace xaml_practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //connect to the db
        private PatientData db = new PatientData();
        public MainWindow()
        {
            InitializeComponent();

            //call load patients on startup
            LoadPatients();
        }

        //part i) load patients into listbox usaing LINQ
        public void LoadPatients()
        {
            var query = from p in db.Patients
                        orderby p.LastName
                        select p;

            //display in listbox
            lbxPatients.ItemsSource = query.ToList();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = "";                                                
        }

        //part j) adding new patient to db
        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            //read data from screen
            string firstName = tbxFirstName.Text;
            string lastName = tbxLastName.Text;
            string phone = tbxPhoneNumber.Text;
            DateTime dob = dpDOB.SelectedDate.Value;

            //create new patient object
            Patient newPatient = new Patient()
            {
                FirstName = firstName,
                LastName = lastName,
                ContactNumber = phone,
                DOB = dob
            };

            //add to db
            db.Patients.Add(newPatient);
            db.SaveChanges();

            //messagebox to indicate addition was successful
            MessageBox.Show("Patient added successfully");

            //refresh listbox
            LoadPatients();
        }

        //park k) adding appointment opens new window to add appointment details
        private void btnAddAppointment_Click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = lbxPatients.SelectedItem as Patient;
            if (selectedPatient != null)
            {
                AppointmentWindow appointmentWindow = new AppointmentWindow(selectedPatient.PatientId);
                appointmentWindow.Owner = this;
                appointmentWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select a patient first.");
            }
        }

        //loads appoinments for slected patient
        private void lbxPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Patient selectedPatient = lbxPatients.SelectedItem as Patient;

            if (selectedPatient != null)
            {
                var query = from a in db.Appointments
                                   where a.PatientId == selectedPatient.PatientId
                                   orderby a.AppointmentTime
                                   select a;

                var results = query.ToList();

                if (results.Count > 0)
                {
                    lbxAppointments.ItemsSource = results;
                }
                else
                {
                    lbxAppointments.ItemsSource = null;
                    MessageBox.Show("No appointments found for this patient.");
                }
            }
        }

        private void btnEditAppointment_Click(object sender, RoutedEventArgs e)
        {
            Appointment selectedAppointment = lbxAppointments.SelectedItem as Appointment;


            if (selectedAppointment != null)
            {
                AppointmentWindow appointmentWindow = new AppointmentWindow(selectedAppointment.PatientId, selectedAppointment.AppointmentId);
                appointmentWindow.Owner = this;
                appointmentWindow.ShowDialog();
            }

            else 
            {
                MessageBox.Show("Please select an appointment to edit.");
            }
        }
    }
}

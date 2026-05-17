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
        private void LoadPatients()
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

        //adding new patient to db
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
    }
}

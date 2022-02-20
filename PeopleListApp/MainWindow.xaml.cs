using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace PeopleListApp
{
    public partial class MainWindow : Window
    {
        //Data inside the table
        ObservableCollection<Person> people = new ObservableCollection<Person>();
        //Flag to check if buttons are disabled
        bool buttonsDisabled = true;
        public MainWindow()
        {
            InitializeComponent();
            LoadPeople();
            SaveTableButton.Click += SaveTableButton_Click;
            DiscardChangesButton.Click += DiscardChangesButton_Click;
        }

        //Add button event
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            people.Add(new Person("FirstName", "LastName", "StreetName", "HouseNumber", null, "PostalCode", "Town", "000000000", new DateTime(2000, 1, 1)));
            DisableButtons(false);
        }

        //Delete button event
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            people.Remove((Person)PeopleDataGrid.SelectedItem);
            DisableButtons(false);
        }

        //Save button event
        private void SaveTableButton_Click(object sender, RoutedEventArgs e)
        {
            //checking if provided data is valid
            StringBuilder validationMassage = new StringBuilder();
            foreach (var person in people)
            {
                validationMassage.Append(person.Validate());
            }
            if (validationMassage.Length > 0)
            {
                validationMassage.Append("Please fix this issues before saving");
                MessageBox.Show(validationMassage.ToString(), "Incorrect data");
                return;
            }

            //Writing to file
            List<Person> listOfPeople = people.ToList();
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(List<Person>));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//ListOfPeople.xml";
            FileStream file = File.Create(path);
            writer.Serialize(file, listOfPeople);
            file.Close();
            DisableButtons(true);
        }

        //Cancel button event
        private void DiscardChangesButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPeople();
            DisableButtons(true);
        }

        //Function to load people from file
        private void LoadPeople()
        {
            people.Clear();
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(List<Person>));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//ListOfPeople.xml";

            //If there is no file in path list remains empty
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);

                List<Person> listOfPeople = (List<Person>)reader.Deserialize(file);

                people = new ObservableCollection<Person>(listOfPeople);
            }
            PeopleDataGrid.ItemsSource = people;
        }

        //Event after which data is reloaded and buttons became active
        private void PeopleDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Dispatcher.BeginInvoke(new DispatcherOperationCallback(param =>
            {
                DisableButtons(false);
                CollectionViewSource.GetDefaultView(people).Refresh();
                return null;
            }), DispatcherPriority.Background, new object[] { null });
        }

        //Event after which buttons became active
        private void PeopleDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                DisableButtons(false);
            }
        }

        //Event after which buttons became active
        private void BirthDateDatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            DisableButtons(false);
        }

        //Function to disable/enable Save and Cancel Button
        private void DisableButtons(bool disable) 
        {
            if (disable)
            {
                if (buttonsDisabled)
                    return;
                else 
                {
                    SaveTableButton.IsEnabled = false;
                    DiscardChangesButton.IsEnabled = false;
                    buttonsDisabled = true;
                }
            }
            else 
            {
                if (!buttonsDisabled)
                    return;
                else
                {
                    SaveTableButton.IsEnabled = true;
                    DiscardChangesButton.IsEnabled = true;
                    buttonsDisabled = false;
                }
            }
        }
    }
}

using System;
using System.Text;

namespace PeopleListApp
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        public string PostalCode { get; set; }
        public string Town { get; set; }
        public string PhoneNumber { get; set; }

        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                //Calculating age and assigning that variable to _age
                int age = DateTime.Now.Year - _dateOfBirth.Year;
                if (DateTime.Now.Month < _dateOfBirth.Month || (DateTime.Now.Month == _dateOfBirth.Month && DateTime.Now.Day < _dateOfBirth.Day))
                    age--;

                _age = age;

            }

        }

        private int _age;
        public int Age
        {
            get => _age;
        }

        public Person()
        { }

        public Person(string firstName, string lastName, string streetName, string houseNumber, int? apartmentNumber, string postalCode, string town, string phoneNumber, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            StreetName = streetName;
            HouseNumber = houseNumber;
            ApartmentNumber = apartmentNumber;
            PostalCode = postalCode;
            Town = town;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
        }

        //Function to check if provided data is valid
        public StringBuilder Validate()
        {
            StringBuilder result = new StringBuilder();
            if (string.IsNullOrEmpty(FirstName))
                result.Append("First name is empty.\n");
            if (FirstName == "FirstName")
                result.Append("First name have default value.\n");

            if (string.IsNullOrEmpty(LastName))
                result.Append("Last name is empty.\n");
            if (LastName == "LastName")
                result.Append("Last name have default value.\n");

            if (string.IsNullOrEmpty(StreetName))
                result.Append("Street name is empty.\n");
            if (StreetName == "StreetName")
                result.Append("Street name have default value.\n");

            if (string.IsNullOrEmpty(HouseNumber))
                result.Append("House number is empty.\n");
            if (HouseNumber == "HouseNumber")
                result.Append("House number have default value.\n");

            if (string.IsNullOrEmpty(PostalCode))
                result.Append("Postal code is empty.\n");
            if (PostalCode == "PostalCode")
                result.Append("Postal code have default value.\n");

            if (string.IsNullOrEmpty(Town))
                result.Append("Town is empty.\n");
            if (Town == "Town")
                result.Append("Town have default value.\n");

            if (string.IsNullOrEmpty(PhoneNumber))
                result.Append("PhoneNumber is empty.\n");
            if (PhoneNumber == "000000000")
                result.Append("PhoneNumber have default value.\n");

            if (_dateOfBirth > DateTime.Now)
                result.Append("Person can't be born in future.\n");

            if (result.Length > 0)
                result.Insert(0, "For user with first name '" + FirstName + "' the following issues were detecteted:\n");

            return result;
        }
    }
}

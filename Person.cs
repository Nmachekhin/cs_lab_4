using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PersonDisplay
{
    internal class Person : INotifyPropertyChanged
    {
        private static string s_emailRegExpr = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate;

        private ZodiakCalculator _calculator;


        private bool EmailValidator(string email)
        {
            Regex validator = new Regex(s_emailRegExpr);
            if (!validator.IsMatch(email))
            {
                return false;
            }
            return true;
        }


        private bool DateValidator(DateTime birthDate)
        {
            return ZodiakCalculator.s_DateValidator(birthDate);
        }


        public Person(string name, string surname, string email, DateTime birthDate) 
        {
           
            _name = new string(name);
            _surname = new string(surname);
            if (EmailValidator(email))
                _email = new string(email);
            else throw new InvalidEmailFormattingException("Email does not match the pattern!");
            if (DateValidator(birthDate))
                _birthDate = birthDate;
            else throw new Exception("Unknown date error!");
                _calculator = new ZodiakCalculator();
            _calculator.PropertyChanged += OnZodiakCalculateorPropertyChange;
        }

        public Person(string name, string surname, string email) : this(name, surname, email, DateTime.Today) { }

        public Person(string name, string surname, DateTime birthDate) : this(name, surname, string.Empty, birthDate) { }

        ~Person() { _calculator = null; }

        public int Age
        {
            get { return _calculator.Age; }
        }

        public bool IsAdult
        {
            get { return _calculator.Age>=18; }
        }

        public string SunSign
        {
            get { return _calculator.CurrentZodiakSign; }
        }

        public string ChineeseSign
        {
            get { return _calculator.CurrentChineeseZodiakSign; }
        }

        public bool IsBirthday
        {
            get { return _calculator.IsBirthdayToday; }
        }

        public string Email
        {
            
            get { return new string(_email); }
        }

        public string Name
        {
            get { return new string(_name); }
            set { _name = new string(value); }
        }

        public string Surname
        {
            get { return new string(_surname); }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
        }


        public async Task UpdateDate(DateTime date)
        {
            await _calculator.UpdateFields(date);
        }

        public bool IsValid
        {
            get { return _calculator.IsDateValid; }
        }

        private void OnZodiakCalculateorPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case (nameof(ZodiakCalculator.Date)):
                    _birthDate = _calculator.Date;
                    OnPropertyChanged(nameof(this.BirthDate));
                    break;
                case (nameof(ZodiakCalculator.IsBirthdayToday)):
                    OnPropertyChanged(nameof(this.IsBirthday));
                    break;
                case (nameof(ZodiakCalculator.Age)):
                    OnPropertyChanged(nameof(this.Age));
                    break;
                case (nameof(ZodiakCalculator.IsDateValid)):
                    OnPropertyChanged(nameof(this.IsValid));
                    break;
                case (nameof(ZodiakCalculator.CurrentZodiakSign)):
                    OnPropertyChanged(nameof(this.SunSign));
                    break;
                case (nameof(ZodiakCalculator.CurrentChineeseZodiakSign)):
                    OnPropertyChanged(nameof(this.ChineeseSign));
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

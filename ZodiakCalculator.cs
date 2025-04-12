using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Controls;

namespace PersonDisplay
{
    internal class ZodiakCalculator : INotifyPropertyChanged
    {
        private DateTime _date;
        private int _age;
        private bool _dateValid = false;
        private bool _isBirthdayToday = false;
        private ZodiakSign _currentSign = null;
        private string _chineeseCurrentSign = string.Empty;
        private static ZodiakSign[] s_zodiakSignsTimespan = {
            new ZodiakSign(new DateTime(4, 3, 21), new DateTime(4, 4, 19), "Aries"),
            new ZodiakSign(new DateTime(4, 4, 20), new DateTime(4, 5, 20), "Taurus"),
            new ZodiakSign(new DateTime(4, 5, 21), new DateTime(4, 6, 20), "Gemini"),
            new ZodiakSign(new DateTime(4, 6, 21), new DateTime(4, 7, 22), "Cancer"),
            new ZodiakSign(new DateTime(4, 7, 23), new DateTime(4, 8, 22), "Leo"),
            new ZodiakSign(new DateTime(4, 8, 23), new DateTime(4, 9, 22), "Virgo"),
            new ZodiakSign(new DateTime(4, 9, 23), new DateTime(4, 10, 22), "Libra"),
            new ZodiakSign(new DateTime(4, 10, 23), new DateTime(4, 11, 21), "Scorpio"),
            new ZodiakSign(new DateTime(4, 11, 22), new DateTime(4, 12, 21), "Sagittarius"),
            new ZodiakSign(new DateTime(4, 12, 22), new DateTime(5, 1, 19), "Capricorn"),
            new ZodiakSign(new DateTime(4, 1, 20), new DateTime(4, 2, 18), "Aquarius"),
            new ZodiakSign(new DateTime(4, 2, 19), new DateTime(4, 3, 20), "Pisces")
        };

        private static readonly object s_AgeLocker = new object();
        private static readonly object s_DateLocker = new object();
        private static readonly object s_ValidDataLocker = new object();
        private static readonly object s_BirthdayLocker = new object();
        private static readonly object s_ZodiakLocker = new object();
        private static readonly object s_ChineeseZodiakLocker = new object();


        public ZodiakCalculator()
        {
            _date = DateTime.Today;
        }

        public ZodiakCalculator(DateTime date)
        {
            _date = date;
        }

        public int Age
        {
            get { 
                lock(s_AgeLocker)
                {
                    return _age;
                }
            }
            
        }

        public async Task CalculateAge()
        {
            lock (s_AgeLocker)
            {
                DateTime today = DateTime.Today;
                _age = (DateTime.Today.Year - Date.Year);
                if (today.Month < Date.Month || (today.Month == Date.Month && today.Day < Date.Day))
                {
                    _age--;
                }
            }
            OnPropertyChanged(nameof(Age));
        }

        public bool IsBirthdayToday
        {
            get { lock (s_BirthdayLocker) return _isBirthdayToday; }
        }

        public bool IsDateValid
        {
            get { lock (s_ValidDataLocker) return _dateValid; }
        }

        public string CurrentZodiakSign
        {
            get
            {
                lock (s_ZodiakLocker)
                {
                    if (_currentSign == null)
                        return "";
                    return _currentSign.SignName;
                }
            }
        }

        public string CurrentChineeseZodiakSign
        {
            get
            {
                lock(s_ChineeseZodiakLocker)
                {
                    string signCopy = new String(_chineeseCurrentSign);
                    return signCopy;
                }
            }
        }

        private async Task CalculateZodiakSign()
        {
            DateTime yearlessDate;
            if (_date.Month == 1 && _date.Day <= 19) yearlessDate = new DateTime(5, _date.Month, _date.Day);
            else yearlessDate = new DateTime(4, _date.Month, _date.Day);
            for (int i = 0; i < 12; i++)
            {
                if (await s_zodiakSignsTimespan[i].CheckBirthDate(yearlessDate))
                    lock (s_ZodiakLocker) { _currentSign = s_zodiakSignsTimespan[i]; }
            }
            OnPropertyChanged(nameof(CurrentZodiakSign));
        }

        private async Task CalculateChineeseZodiakSign()
        {
            string newString = await ChineeseZodiakSign.GetSign(_date);
            lock (s_ChineeseZodiakLocker)
            {
                _chineeseCurrentSign = newString;
            }
            
            OnPropertyChanged(nameof(CurrentChineeseZodiakSign));
        }

        public DateTime Date
        {
            get { lock (s_DateLocker) { return _date; } }
        }

        private async Task<bool> CheckoutBirthday()
        {
            return _date.Day == DateTime.Today.Day && _date.Month == DateTime.Today.Month;
        }

        public async Task UpdateFields(DateTime value)
        {
            bool succesfull_check = false;
            try
            {
                succesfull_check=s_DateValidator(value);
                if (!succesfull_check)
                {
                    lock (s_DateLocker) { _date = DateTime.Today; }
                    lock (s_ValidDataLocker) _dateValid = false;
                    lock (s_BirthdayLocker) { _isBirthdayToday = false; }
                    lock (s_AgeLocker) { _age = 0; }
                    lock (s_ZodiakLocker) { _currentSign = null; }
                    lock (s_ChineeseZodiakLocker) { _chineeseCurrentSign = ""; }
                    throw new Exception("Unknown birth date exception!");
                }
            }catch (BirthDateInFutureException e) { }
            catch (BirthDateTooFarInPastException e) { }
            finally
            {
                if(succesfull_check)
                {
                    lock (s_DateLocker) { _date = value; }
                    lock (s_ValidDataLocker) _dateValid = true;
                    await CalculateAge();
                    await CalculateZodiakSign();
                    await CalculateChineeseZodiakSign();
                    bool birthdayState = await CheckoutBirthday();
                    lock (s_BirthdayLocker) { _isBirthdayToday = birthdayState; }
                }
                OnPropertyChanged(nameof(IsDateValid));
                OnPropertyChanged(nameof(IsBirthdayToday));
                OnPropertyChanged(nameof(Date));
            }
        }


        public static bool s_DateValidator(DateTime date)
        {
            DateTime today = DateTime.Today;
            if (today < date) throw new BirthDateInFutureException("Birth date cannot be in the future!");
            if ((today.Year - date.Year) > 135) throw new BirthDateTooFarInPastException("Birth date is over 135 years in the past!");
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

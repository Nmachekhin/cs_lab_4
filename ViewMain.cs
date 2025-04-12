using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDisplay
{
    internal class ViewMain
    {
        private List<Person> _people;
        public EventHandler<List<Person>> DisplayPeopleEvent;
        public EventHandler ClearGrid;
        public EventHandler<bool> EditPanelVisibility;
        public EventHandler<bool> EditPanelEditable;
        public EventHandler<bool> GridVisibility;
        public EventHandler<Person> FillEditDataEvent;
        public event EventHandler<bool> UpdateProceedButtonStatus;
        private static readonly object s_PeopleLocker = new object();
        private Person _editPerson;
        private bool _editing=false;
        private bool _peopleReady = false;
        private string _sortingAttribute = nameof(Person.Name);
        public async Task CreatePeople() 
        {
            lock(s_PeopleLocker)
            {
                _people = new List<Person>();
                _people.Add(new Person("Iavn", "Ivanenko", "ivan@gmail.com", new DateTime(2004, 10, 31)));
                _people.Add(new Person("Peter", "Petrenko", "peter@gmail.com", new DateTime(2004, 4, 5)));
                _people.Add(new Person("Amy", "Barnes", "anthony59@gmail.com", new DateTime(2005, 7, 14)));
                _people.Add(new Person("Anthony", "Alexander", "ashleystephenson@hodges.info", new DateTime(1965, 9, 16)));
                _people.Add(new Person("Virginia", "Blake", "richardbradshaw@bell.com", new DateTime(1995, 3, 6)));
                _people.Add(new Person("Tonya", "Bailey", "sethwalker@vega.com", new DateTime(1999, 8, 27)));
                _people.Add(new Person("Beverly", "Lee", "harveyolivia@carpenter.com", new DateTime(1979, 3, 10)));
                _people.Add(new Person("Jesse", "Curtis", "clarkrobert@johnson.info", new DateTime(1985, 4, 4)));
                _people.Add(new Person("Robert", "Hall", "dawn04@hotmail.com", new DateTime(1990, 12, 8)));
                _people.Add(new Person("Rebecca", "Nguyen", "blake21@gmail.com", new DateTime(1974, 5, 22)));
                _people.Add(new Person("Samantha", "Miller", "nataliewalker@yahoo.com", new DateTime(1993, 9, 5)));
                _people.Add(new Person("Steven", "Edwards", "katiejacob@henderson.net", new DateTime(1980, 2, 10)));
                _people.Add(new Person("Jacob", "Campbell", "charlesdavidson@willis.biz", new DateTime(1997, 3, 12)));
                _people.Add(new Person("Thomas", "Garcia", "timothydaniel@jones.com", new DateTime(2002, 6, 20)));
                _people.Add(new Person("Jessica", "Allen", "joseph28@gmail.com", new DateTime(2000, 12, 28)));
                _people.Add(new Person("Nancy", "Cameron", "beverly86@hotmail.com", new DateTime(1987, 11, 2)));
                _people.Add(new Person("Sarah", "Roberts", "georgeanderson@yahoo.com", new DateTime(1999, 5, 19)));
                _people.Add(new Person("Jack", "Parker", "kevinrobinson@garcia.com", new DateTime(1991, 10, 18)));
                _people.Add(new Person("Christopher", "Evans", "heather58@thomas.org", new DateTime(1989, 1, 15)));
                _people.Add(new Person("Laura", "Thomas", "lindawilson@hughes.org", new DateTime(1994, 12, 27)));
                _people.Add(new Person("James", "Moore", "lindseyjames@reyes.org", new DateTime(1983, 4, 5)));
                _people.Add(new Person("Cynthia", "King", "jessicawilson@parker.com", new DateTime(1992, 8, 16)));
                _people.Add(new Person("David", "Baker", "patricia13@henderson.com", new DateTime(1986, 9, 29)));
                _people.Add(new Person("Elizabeth", "Scott", "brian86@johnson.net", new DateTime(2001, 11, 3)));
                _people.Add(new Person("Mark", "Mitchell", "john24@scott.com", new DateTime(1996, 4, 17)));
                _people.Add(new Person("Betty", "Wright", "charlesyoung@nichols.com", new DateTime(1988, 2, 11)));
                _people.Add(new Person("Anna", "Rodriguez", "patricia63@robinson.com", new DateTime(2000, 7, 21)));
                _people.Add(new Person("John", "Gonzalez", "thomas18@king.com", new DateTime(1993, 11, 27)));
                _people.Add(new Person("Nancy", "Carter", "melissa41@davis.biz", new DateTime(1997, 5, 19)));
                _people.Add(new Person("Gary", "Perez", "jasonmorgan@thompson.org", new DateTime(2003, 10, 30)));
                _people.Add(new Person("Heather", "Morris", "christopher38@cook.org", new DateTime(1995, 2, 9)));
                _people.Add(new Person("Joshua", "Hernandez", "virginiataylor@brown.net", new DateTime(2002, 3, 14)));
                _people.Add(new Person("Matthew", "Clark", "richard39@williams.org", new DateTime(1981, 12, 22)));
                _people.Add(new Person("Deborah", "Young", "elizabeth37@chavez.net", new DateTime(1996, 10, 2)));
                _people.Add(new Person("Richard", "Adams", "brittanybrown@price.com", new DateTime(1989, 5, 9)));
                _people.Add(new Person("Benjamin", "Morris", "louis99@adams.org", new DateTime(1991, 6, 17)));
                _people.Add(new Person("Laura", "Stewart", "paul20@nguyen.net", new DateTime(1992, 4, 19)));
                _people.Add(new Person("Edward", "Nelson", "maryjackson@james.com", new DateTime(1984, 8, 1)));
                _people.Add(new Person("Julia", "Martinez", "james07@taylor.org", new DateTime(2004, 1, 13)));
                _people.Add(new Person("Charles", "White", "robertclark@jones.com", new DateTime(1998, 5, 23)));
                _people.Add(new Person("Karen", "King", "steven92@king.com", new DateTime(1993, 12, 5)));
                _people.Add(new Person("Mark", "Johnson", "heather36@willis.net", new DateTime(1997, 8, 17)));
                _people.Add(new Person("Donald", "Miller", "michael77@james.net", new DateTime(1987, 1, 10)));
                _people.Add(new Person("Margaret", "Gonzalez", "lindsaymiller@davis.com", new DateTime(2002, 4, 14)));
                _people.Add(new Person("Frank", "Lee", "chris35@young.net", new DateTime(1985, 11, 6)));
                _people.Add(new Person("Joshua", "Walker", "stephenroberts@perez.org", new DateTime(1982, 12, 24)));
                _people.Add(new Person("Brian", "Lopez", "amber56@harris.org", new DateTime(2001, 2, 5)));
                _people.Add(new Person("Amanda", "Rodriguez", "amandak@weaver.com", new DateTime(1987, 10, 3)));
                _people.Add(new Person("Steven", "Taylor", "timothy35@martinez.com", new DateTime(1990, 7, 11)));
                _people.Add(new Person("Brian", "Lopez", "amber56@harris.org", new DateTime(1998, 3, 9)));
                _people.Add(new Person("Douglas", "Hernandez", "robert92@henry.com", new DateTime(1981, 10, 16)));
                _people.Add(new Person("Joseph", "Thomas", "douglas76@allen.org", new DateTime(1997, 6, 11)));
                _people.Add(new Person("Carol", "Jackson", "williamsolivia@nelson.com", new DateTime(2000, 4, 29)));
                _people.Add(new Person("Peter", "Petrenko", "peter@gmail.com", new DateTime(2004, 4, 5)));
                _people.Add(new Person("Iavn", "Ivanenko", "ivan@gmail.com", new DateTime(2004, 10, 31)));
            }
            for (int i = 0; i < _people.Count; i++)
            {
                await _people[i].UpdateDate(_people[i].BirthDate);
            }
            _peopleReady = true;
        }
        
        public ViewMain() 
        {
        }

        private IOrderedEnumerable<T> SortData<T>(List<T> data)
        {
            switch (_sortingAttribute)
            {
                case (nameof(Person.Name)):
                    return data.OrderBy(x => (x as Person).Name);
                case (nameof(Person.Surname)):
                    return data.OrderBy(x => (x as Person).Surname);
                case (nameof(Person.Email)):
                    return data.OrderBy(x => (x as Person).Email);
                case (nameof(Person.BirthDate)):
                    return data.OrderBy(x => (x as Person).BirthDate);
                case (nameof(Person.SunSign)):
                    return data.OrderBy(x => (x as Person).SunSign);
                case (nameof(Person.ChineeseSign)):
                    return data.OrderBy(x => (x as Person).ChineeseSign);
                case (nameof(Person.IsAdult)):
                    return data.OrderBy(x => (x as Person).IsAdult);
                default:
                    throw new NotImplementedException("Unknown sorting type!");
            }
            
        }

        public void GetAll()
        {
            DisplayPeopleEvent.Invoke(this, SortData<Person>(_people).ToList<Person>());
        }

        public void TriggerEditEvent(Person person)
        {
            EditPanelEditable.Invoke(this, true);
            EditPanelVisibility.Invoke(this, true);
            GridVisibility.Invoke(this, false);
            _editPerson = person;
            _editing = true;
            FillEditDataEvent.Invoke(this, person);
        }


        public void CheckInputsStatus(string name, string surname, string email, string date, bool btnStatus)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(surname) && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(date))
            {
                if (!btnStatus) UpdateProceedButtonStatus.Invoke(this, true);
            }
            else if (btnStatus) UpdateProceedButtonStatus.Invoke(this, false);
        }


        public void CancellButtonClick()
        {
            EditPanelVisibility.Invoke(this, false);
            GridVisibility.Invoke(this, true);
            _editing = false;
            _editPerson = null;
        }


        private int GetPersonId(Person person)
        {
            for (int i = 0; i < _people.Count; i++) 
            {
                if(person == _people[i]) return i;
            }
            throw new NotImplementedException("No such Person!");
        }

        public async Task ProceedButtonClick(string name, string surname, string email, DateTime birthDate)
        {
            EditPanelEditable.Invoke(this, false);
            if (_editing)
            {
                int pId = GetPersonId(_editPerson);
                _people[pId] = new Person(name, surname, email);
                await _people[pId].UpdateDate(birthDate);
                EditPanelEditable.Invoke(this, true);
            }
            else
            {
                _people.Add(new Person(name, surname, email));
                await _people.Last().UpdateDate(birthDate);
            }
            ClearGrid.Invoke(this, EventArgs.Empty);
            GetAll();
            CancellButtonClick();
        }

        public void DeleteButtonClick(Person person)
        {
            lock (s_PeopleLocker)
            {
                _people.Remove(person);
            }
            ClearGrid.Invoke(this, EventArgs.Empty);
            GetAll();
            CancellButtonClick();
        }

        public void AddPersonButtonClick()
        {
            GridVisibility.Invoke(this, false );
            EditPanelVisibility.Invoke(this, true );
            EditPanelEditable.Invoke(this, true);

        }


        public bool IsReady
        {
            get { return _peopleReady; }
        }

    }
}

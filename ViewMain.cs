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
        public EventHandler<Person> DisplayPersonEvent;
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
        public async Task CreatePeople() 
        {
            lock(s_PeopleLocker)
            {
                _people = new List<Person>();
                _people.Add(new Person("Iavn", "Ivanenko", "ivan@gmail.com", new DateTime(2004, 10, 31)));
                _people.Add(new Person("Peter", "Petrenko", "peter@gmail.com", new DateTime(2004, 4, 5)));
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

        public void GetAll()
        {
            for (int i = 0; i < _people.Count; i++)
            {
                DisplayPersonEvent.Invoke(this, _people[i]);
            }
        }

        public void TriggerEditEvent(Person person)
        {
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
            int pId=GetPersonId(_editPerson);
            _people[pId]=new Person(name, surname, email);
            await _people[pId].UpdateDate(birthDate);
            EditPanelEditable.Invoke(this, true);
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

        public bool IsReady
        {
            get { return _peopleReady; }
        }

    }
}

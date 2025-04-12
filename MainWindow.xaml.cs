﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonDisplay
{

    public partial class MainWindow : Window
    {
        private ViewMain _viewController;
        private ObservableCollection<Person> _source;
        public MainWindow()
        {
            InitializeComponent();
            _source = new ObservableCollection<Person>();
            PeopleGrid.ItemsSource = _source;
            _viewController = new ViewMain();

            _viewController.DisplayPersonEvent += AddToGrid;
            _viewController.EditPanelVisibility += PanelVisibilityEvent;
            _viewController.GridVisibility += GridVisibilityEvent;
            _viewController.UpdateProceedButtonStatus += ButtonStatusEventHandler;
            _viewController.EditPanelEditable += PanelEditableEvent;
            _viewController.FillEditDataEvent += FillEditFieldsEvent;
            _viewController.ClearGrid += ClearGridEvent;
            
            PeopleGrid.IsEnabled = false;
            WaitUntillReady();
        }

        private async Task WaitUntillReady()
        {
            await _viewController.CreatePeople();
            _viewController.GetAll();
            PeopleGrid.IsEnabled = true;
        }


        private void ClearGridEvent(object sender, EventArgs e)
        {
            _source.Clear();
            //PeopleGrid.ItemsSource= _source;
        }

        private void ClearInputs()
        {
            NameInputBox.Text = "";
            SurnameInputBox.Text = "";
            EmailInputBox.Text = "";
            BirthdayDatePicker.SelectedDate = DateTime.Today;
        }

        private void FillEditFieldsEvent(object sender, Person person)
        {
            NameInputBox.Text = person.Name;
            SurnameInputBox.Text = person.Surname;
            EmailInputBox.Text = person.Email;
            BirthdayDatePicker.SelectedDate = person.BirthDate;
        }


        private void ButtonStatusEventHandler(object sender, bool active)
        {
            ProceedButton.IsEnabled = active;
        }

        private void AddToGrid(object caller, Person person)
        {
            
            _source.Add(person);
            Trace.WriteLine(_source.Count);
        }


        private void EditObjectEvent(object caller, RoutedEventArgs e)
        {
            Person person = (caller as Button).DataContext as Person;
            Trace.WriteLine(person.Email);
            _viewController.TriggerEditEvent(person);
        }


        private void ConfirmDateButtonClick(object sender, RoutedEventArgs e)
        {
            _viewController.ProceedButtonClick(NameInputBox.Text, SurnameInputBox.Text, EmailInputBox.Text, Convert.ToDateTime(BirthdayDatePicker.SelectedDate));
        }


        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _viewController.CancellButtonClick();
        }

        private void InputsTextChanged(object sender, EventArgs e)
        {
            _viewController.CheckInputsStatus(NameInputBox.Text, SurnameInputBox.Text, EmailInputBox.Text, BirthdayDatePicker.Text, ProceedButton.IsEnabled);
        }


        private void PanelVisibilityEvent(object sender, bool visible)
        {
            if (visible) AllInputsPanel.Visibility = Visibility.Visible;
            else
            {
                AllInputsPanel.Visibility = Visibility.Collapsed;
                ClearInputs();
            }
        }

        private void PanelEditableEvent(object sender, bool enable)
        {
            AllInputsPanel.IsEnabled=enable;

        }

        private void GridVisibilityEvent(object sender, bool visible)
        {
            if (visible) PeopleGrid.Visibility = Visibility.Visible;
            else PeopleGrid.Visibility = Visibility.Collapsed;
        }

    }
}
using EntrepriseEmploye.Services;
using EntrepriseEmploye.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EntrepriseEmploye.ViewModels
{
    class EmployeeListViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _dbService = DatabaseService.Instance; 

        public ObservableCollection<Employee> AllEmployees { get; set; } = new();
        public ObservableCollection<Employee> FilteredEmployees { get; set; } = new();

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    ApplyFilters();
                }
            }
        }

        private string _selectedStatus = "Tous";
        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                if (_selectedStatus != value)
                {
                    _selectedStatus = value;
                    OnPropertyChanged();
                    ApplyFilters();
                }
            }
        }

        public async Task LoadEmployeesAsync()
        {
            var list = await _dbService.GetEmployees();
            AllEmployees.Clear();
            foreach (var emp in list)
            {
                AllEmployees.Add(emp);
            }
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var query = AllEmployees.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(SearchText))
                query = query.Where(e => e.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || e.Position.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            if (SelectedStatus != "Tous")
                query = query.Where(e => e.Status == SelectedStatus);

            FilteredEmployees.Clear();
            foreach (var emp in query)
                FilteredEmployees.Add(emp);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ICommand NavigateToAddEmployeeCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("addemployee");
        });
    }
}

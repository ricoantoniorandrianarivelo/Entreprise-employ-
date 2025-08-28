using EntrepriseEmploye.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EntrepriseEmploye.ViewModels
{
    class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _dbService = DatabaseService.Instance;
        private int _totalEmployees;
        public int TotalEmployees
        {
            get => _totalEmployees;
            set { _totalEmployees = value; OnPropertyChanged(); }
        }

        private int _workingEmployees;
        public int WorkingEmployees
        {
            get => _workingEmployees;
            set { _workingEmployees = value; OnPropertyChanged(); }
        }

        private int _onLeaveEmployees;
        public int OnLeaveEmployees
        {
            get => _onLeaveEmployees;
            set { _onLeaveEmployees = value; OnPropertyChanged(); }
        }

        public async Task LoadDataAsync()
        {
            TotalEmployees = await _dbService.GetTotalEmployees();
            WorkingEmployees = await _dbService.GetWorkingEmployees();
            OnLeaveEmployees = await _dbService.GetOnLeaveEmployees();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

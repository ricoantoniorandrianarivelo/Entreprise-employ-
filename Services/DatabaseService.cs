using SQLite;
using EntrepriseEmploye.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntrepriseEmploye.Services
{
    public class DatabaseService
    {
        private static DatabaseService _instance;
        private SQLiteAsyncConnection _db;

        private DatabaseService()
        {
            var databasePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "employes.db");

            _db = new SQLiteAsyncConnection(databasePath);

            _db.CreateTableAsync<Employee>().Wait();

        }

        public static DatabaseService Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new DatabaseService();
                }
                return _instance;
            }
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _db.Table<Employee>().ToListAsync();
        }

        public async Task<int> GetTotalEmployees() =>
            (await GetEmployees()).Count;
        public async Task<int> GetWorkingEmployees() =>
            (await GetEmployees()).Count(e => e.Status == "En travail");
        public async Task<int> GetOnLeaveEmployees() =>
            (await GetEmployees()).Count(e => e.Status == "En congé");

        public Task<int> AjouterEmployeAsync(Employee employe)
        {
            return _db.InsertAsync(employe);
        }

        public async Task<Employee> GetEmployeeByMatriculeAsync(string matricule)
        {
            return await _db.Table<Employee>().FirstOrDefaultAsync(e => e.Matricule == matricule);
        }
            

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _db.Table<Employee>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            return await _db.UpdateAsync(employee);
        }

        public async Task<int> DeleteEmployee(int id)
        {
            return await _db.DeleteAsync<Employee>(id);
        }
    }
}

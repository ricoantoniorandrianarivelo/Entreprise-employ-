using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntrepriseEmploye.Models
{
    [Table("Employees")]
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Matricule { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DateDebut { get; set; }
        public string Status { get; set; } //"En travail" ou "En congé"
        public string Skills { get; set; } //Nouvelle propriété 
    }
}

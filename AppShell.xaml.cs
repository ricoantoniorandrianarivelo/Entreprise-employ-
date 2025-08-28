using EntrepriseEmploye.Pages;
using System.Text.Json;

namespace EntrepriseEmploye
{
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(EmployeeDetailPage), typeof(EmployeeDetailPage));
            Routing.RegisterRoute(nameof(ProfilEmployePage), typeof(ProfilEmployePage));
        }
    }
}

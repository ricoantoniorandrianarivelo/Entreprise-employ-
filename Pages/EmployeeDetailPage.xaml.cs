using EntrepriseEmploye.Models;
using EntrepriseEmploye.ViewModels;
using EntrepriseEmploye.Pages;
using EntrepriseEmploye.Services;
using System.Text.Json;
namespace EntrepriseEmploye.Pages;

[QueryProperty(nameof(EmployeeData), "employeeData")]
public partial class EmployeeDetailPage : ContentPage
{
    public EmployeeDetailPage()
    {
        InitializeComponent();
    }

    private async void OnViewProfileClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedEmployee = button?.CommandParameter as Employee;
        if (selectedEmployee == null)
            return;
        await Navigation.PushAsync(new ProfilEmployePage(selectedEmployee));
    }

    private string _employeeData;
    public string EmployeeData
    {
        get => _employeeData;
        set
        {
            _employeeData = Uri.UnescapeDataString(value);
            var employee = JsonSerializer.Deserialize<Employee>(_employeeData);
            BindingContext = new EmployeeDetailViewModel(employee);
        }
    }

    private async void OnAnalyzeIAProfileClicked(object sender, EventArgs e)
    {
        if(BindingContext is Employee selectedEmployee)
        {
            await Shell.Current.GoToAsync(nameof(ProfilEmployePage), new Dictionary<string, object> { { "selectedEmployee", selectedEmployee } });
        }
    }


}
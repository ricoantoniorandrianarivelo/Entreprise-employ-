using EntrepriseEmploye.Services;
using EntrepriseEmploye.Models;

namespace EntrepriseEmploye.Pages;

public partial class AddEmployeePage : ContentPage
{
    private readonly DatabaseService _dbService;
    private int _editEmployeId;

    public AddEmployeePage()
    {
    }

    public AddEmployeePage(DatabaseService dbService)
	{
		InitializeComponent();
        _dbService = dbService;
        Task.Run(async () => EmployeeListPage.ItemsSource = await _dbService.GetEmployees());
	}

    private async void OnAddEmployeeClicked(object sender, EventArgs e)
    {
        
        if (string.IsNullOrWhiteSpace(FullNameEntry.Text) || string.IsNullOrWhiteSpace(PositionEntry.Text) || StatusPicker.SelectedItem == null)
        {
            await DisplayAlert("Erreur", "Tous les champs sont obligatoires.", "OK");
            return;
        }

        var random = new Random();
        string matricule = $"#{random.Next(100000, 999999)}";

        if (_editEmployeId == 0)
        {
            await _dbService.AjouterEmployeAsync(new Employee
            {
                FullName = FullNameEntry.Text,
                Position = PositionEntry.Text,
                StartDate = StartDatePicker.Date,
                Status = StatusPicker.SelectedItem.ToString()
            });
        }
        else
        {
            await _dbService.UpdateEmployee(new Employee
            {
                Id = _editEmployeId,
                FullName = FullNameEntry.Text,
                Position = PositionEntry.Text,
                StartDate = StartDatePicker.Date,
                Status = StatusPicker.SelectedItem.ToString()
            });

            _editEmployeId = 0;
        }

        FullNameEntry.Text = string.Empty;
        PositionEntry.Text = string.Empty;
        StartDatePicker.Date = DateTime.Now;
        StatusPicker.SelectedItem = string.Empty;

        EmployeeListPage.ItemsSource = await _dbService.GetEmployees();

        


        await DisplayAlert("Debug", $"Matricule: {matricule}\nNom: {FullNameEntry.Text}\nPoste: {PositionEntry.Text}\nStatut: {StatusPicker.SelectedItem}\nDate: {StartDatePicker.Date}", "OK");
        //await _dbService.AddEmployee(newEmployee);
        await DisplayAlert("Succès", "Employé ajouté avec succès", "OK");
        await Shell.Current.GoToAsync(".."); //Retour à la liste
        //await Navigation.PopAsync();
    }
}
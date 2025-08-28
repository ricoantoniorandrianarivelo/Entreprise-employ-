using EntrepriseEmploye.Models;
using EntrepriseEmploye.Services;

namespace EntrepriseEmploye.Pages;

[QueryProperty(nameof(EmployeeId), "SelectedEmployee")]
public partial class ProfilEmployePage : ContentPage
{
	private Employee _employee;
	private readonly DatabaseService _dbService = DatabaseService.Instance;
	private string _employeeId;

	public string EmployeeId
	{
		get => _employeeId;
		set
		{
			_employeeId = value;
			LoadEmployee();
			EmployeeNameLabel.Text = $"Profil de {_employee.FullName}";
        }
	}

    public ProfilEmployePage(Employee employee)
	{
		InitializeComponent();
		_employee = employee;

		EmployeeNameLabel.Text = employee.FullName;

    }

	private async void LoadEmployee()
	{
		if(int.TryParse(EmployeeId, out int id))
		{
			var employee = await _dbService.GetEmployeeById(id);
			if(employee != null)
			{
				EmployeeNameLabel.Text = employee.FullName;

            }
		}
	}


    private async void OnAnalyzeClicked(object sender, EventArgs e)
	{
		var skills = new List<string>();

		//IA simple selon les réponses
		if (!string.IsNullOrWhiteSpace(ToolsEntry.Text))
		{
			if (ToolsEntry.Text.ToLower().Contains("excel")) skills.Add("Analyse de données");
			if (ToolsEntry.Text.ToLower().Contains("vusual")) skills.Add("Développement C#");
		}

		if (CodeSwitch.IsToggled) skills.Add("Développement logiciel");
		if (TeamworkSwitch.IsToggled) skills.Add("Travail en équipe");
		if (ReportsSwitch.IsToggled) skills.Add("Communication");

		switch (DomainPicker.SelectedItem?.ToString())
		{
			case "Technique":
				skills.Add("Compétence technique");
				break;
			case "Administratif":
				skills.Add("Gestion administrative");
				break;
			case "Managérial":
				skills.Add("Leadership");
				break;
		}

		_employee.Skills = string.Join(", ", skills.Distinct());
		await _dbService.UpdateEmployee(_employee);

		await DisplayAlert("Succès", $"Compétences détectées : {_employee.Skills}", "OK");
		await Shell.Current.GoToAsync("..");
	}
}
using EntrepriseEmploye.Services;

namespace EntrepriseEmploye.Pages;

public partial class AboutEmployeePage : ContentPage
{
	private readonly DatabaseService _dbService = DatabaseService.Instance;
	public AboutEmployeePage()
	{
		InitializeComponent();
	}

	private async void OnSearchClicked(object sender, EventArgs e)
	{
		if(string.IsNullOrWhiteSpace(MatriculeEntry.Text))
		{
			await DisplayAlert("Erreur", "Matricule invalide", "OK");
			return;
		}

		var employee = await _dbService.GetEmployeeByMatriculeAsync(MatriculeEntry.Text.Trim());
		if(employee == null)
		{
			await DisplayAlert("Introuvable", "Aucun employé trouvé.", "OK");
			EmployeeInfoFrame.IsVisible = false;
			return;
		}

		//Affichage infos
		FullNameLabel.Text = $"Nom : {employee.FullName}";
		PositionLabel.Text = $"Poste : {employee.Position}";
		StatusLabel.Text = $"Statut : {employee.Status}";
		StartDateLabel.Text = $"Date d'embauche : {employee.StartDate:dd MMMM yyyy}";
        SkillsLabel.Text = $"Compétences : {employee.Skills ?? "Non spécifiées"}";
		int skillCount = employee.Skills?.Split(',').Length ?? 0;


		//Esitmation du salaire via IA basique
		var years = (DateTime.Now - employee.StartDate).Days / 365;
		double baseSalary = 25000;
		double skillBonus = skillCount * 3000;
		double finalSalary = baseSalary + (years * 1500) + skillBonus;
		SalaryEstimateLabel.Text = $"Salaire estimé : {finalSalary:C}";

		//Suggestion IA simple
		string suggestion = skillCount < 3 ? "Ajoutez plus de compétences techniques pour booster le salaire." : "Bon profil ! Envisagez une formation en leadership.";

		IASuggestionLabel.Text = $"IA: {suggestion}";
		EmployeeInfoFrame.IsVisible = true; 

    }
}
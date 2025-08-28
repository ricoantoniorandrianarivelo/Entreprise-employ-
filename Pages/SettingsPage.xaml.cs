using EntrepriseEmploye.Services;

namespace EntrepriseEmploye.Pages;

public partial class SettingsPage : ContentPage
{
	private readonly DatabaseService _dbService = DatabaseService.Instance;
	public SettingsPage()
	{
		InitializeComponent();
	}

	private void OnToggleThemeClicked(object sender, EventArgs e)
	{
		//if (App.Current.UserAppTheme == AppTheme.Dark)
			//App.Current.UserAppTheme == AppTheme.Light;
		//else
            //App.Current.UserAppTheme == AppTheme.Dark;
	}

	private async void OnResetDatabaseClicked(object sender, EventArgs e)
	{
		bool confirm = await DisplayAlert("Confirmer", "Supprimer tous les employés ?", "Oui", "Non");
		if (confirm)
		{
			//await _dbService.DeleteEmployee();
			await DisplayAlert("Succès", "Base réinitialisé", "OK");
		}
	}
}
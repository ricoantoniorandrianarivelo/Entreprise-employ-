namespace EntrepriseEmploye.Pages;

public partial class DashboardPage : ContentPage
{
	public DashboardPage()
	{
		InitializeComponent();

        Appearing += async (_, _) =>
        {
            if (BindingContext is ViewModels.DashboardViewModel vm)
                await vm.LoadDataAsync();
        };
    }
}
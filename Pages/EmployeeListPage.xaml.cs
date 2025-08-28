using EntrepriseEmploye.Models;

namespace EntrepriseEmploye.Pages;


public partial class EmployeeListPage : ContentPage
{
	public EmployeeListPage()
	{
		InitializeComponent();

        Appearing += async (_, _) =>
        {
            if (BindingContext is ViewModels.EmployeeListViewModel vm)
                await vm.LoadEmployeesAsync();
        };
    }

    public static List<Employee> ItemsSource { get; internal set; }

    private async void OnEmployeeSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Employee selected)
        {
            await Shell.Current.GoToAsync($"employeedetail?employeeId={selected.Id}");
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
using EntrepriseEmploye.Services;

namespace EntrepriseEmploye.Pages;

public partial class ExportPage : ContentPage
{
	private readonly DatabaseService _dbService = DatabaseService.Instance;
	public ExportPage()
	{
		InitializeComponent();
	}

	private async void OnExportPdfClicked(object sender, EventArgs e)
	{
		var employees = await _dbService.GetEmployees();
		//A compléter avec ton générateur PDF (PDFSharp, QuestPDF, etc.)
		await DisplayAlert("Export PDF", "Fonction d'export PDF à implémenter", "OK");
	}

	private async void OnExportExcelClicked(object sender, EventArgs e)
	{
		var employees = await _dbService.GetEmployees();
		//A compléter avec ClosedXML, DocumentFormat.OpenXml, ou Syncfusion
		await DisplayAlert("Export Excel", "Fonction d'export Excel à implémenter", "OK");
	}

	private async void OnImportFileClicked(object sender, EventArgs e)
	{
		try
		{
			var file = await FilePicker.PickAsync();
			if(file != null)
			{
				var content = await File.ReadAllTextAsync(file.FullPath);
				//A parser selon format : CSV, JSON, Excel
				await DisplayAlert("Import", $"Contenu lu : {content.Substring(0, Math.Min(100, content.Length))}", "OK");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Erreur", ex.Message, "OK");
		}
	}

	private async void OnSyncClicked(object sender, EventArgs e)
	{
		//Synchronisation serveur optionnelle
		await DisplayAlert("Sync", "Fonction de synchronisation non implémentée.", "OK");
	}
}
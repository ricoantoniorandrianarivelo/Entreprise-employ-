using EntrepriseEmploye.Services;
using Microcharts;
using SkiaSharp;
using System.Data;
using System.Threading.Tasks;

namespace EntrepriseEmploye.Pages;

public partial class StatisticsPage : ContentPage
{
	//private readonly DatabaseService _dbService = new();
	public StatisticsPage()
	{
		InitializeComponent();
		//LoadStatistics();
		ChargerGraphiques();

	}

	//private async void LoadStatistics()
	//{
	//	var employees = await _dbService.GetEmployees();

	//	//R�partition des postes
	//	var positionGroups = employees.GroupBy(e => e.Position)
	//		.Select(g => $"{g.Key} : {g.Count()} employ�(s)").ToList();
	//	PositionStatsView.ItemsSource = positionGroups;

	//	//Anciennet� moyenne
	//	var now = DateTime.Now;
	//	var seniorityGroups = employees.GroupBy(e => e.Position)
	//		.Select(g =>
	//		{
	//			var avgMonths = g.Average(e => (now - e.StartDate).TotalDays / 30);
	//			return $"{g.Key} : {avgMonths:F1} mois";
	//		}).ToList();
	//	SeniorityStatsView.ItemsSource = seniorityGroups;

	//	//Cong�s par mois
	//	var leaveMonth = employees
	//		.Where(e => e.Status == "En cong�")
	//		.GroupBy(e => e.StartDate.ToString("MMMM yyyy"))
	//		.Select(g => $"{g.Key} : {g.Count()} en cong�").ToList();
	//	LeaveStatsView.ItemsSource = leaveMonth;

	//	//Evolution effectif
	//	var growth = employees
	//		.GroupBy(e => e.StartDate.ToString("MMMM yyyy"))
	//		.Select(g => $"{g.Key} : {g.Count()} employ�s ajout�s").ToList();
	//	GrowthStatsView.ItemsSource = growth;
	//}

	private async Task ChargerGraphiques()
	{
		var employes = await DatabaseService.Instance.GetEmployees();
		//1)R�partition des postes
		var repartitionPostes = employes.GroupBy(e => e.Position).Select(g => new ChartEntry(g.Count())
		{
			Label = g.Key,
			ValueLabel = g.Count().ToString(),
			Color = SKColor.Parse(RandomColorHex())
		}).ToList();
		PostChart.Chart = new PieChart { Entries = repartitionPostes };

		//2) Moyenne d'anciennet� par poste (en mois)
		var anciennetetParPoste = employes.GroupBy(e => e.Position).Select(g =>
		{
			double moyenneMois = g.Select(e => (DateTime.Now - e.DateDebut).TotalDays / 30)
			.Average();

			return new ChartEntry((float)moyenneMois)
			{
				Label = g.Key,
				ValueLabel = Math.Round(moyenneMois, 1).ToString(),
				Color = SKColor.Parse(RandomColorHex())
			};
		}).ToList();
		AncienneteChart.Chart = new BarChart { Entries = anciennetetParPoste };

		//3)Cong�s par mois
		var congesParMois = employes
			.Where(e => e.Status.ToLower() == "cong�")
			.GroupBy(e => new { e.StartDate.Year, e.StartDate.Month })
			.OrderBy(g => new DateTime(g.Key.Year, g.Key.Month, 1))
			.Select(g =>
			{
				string mois = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy");
				return new ChartEntry(g.Count())
				{
					Label = mois,
					ValueLabel = g.Count().ToString(),
					Color = SKColor.Parse("#5CADAD")
				};
			}).ToList();
		CongeChart.Chart = new LineChart { Entries = congesParMois };

		//4)Ajouts d'mploy�s par mois
		var ajoutsParMois = employes
			.GroupBy(e => new { e.StartDate.Year, e.StartDate.Month })
			.OrderBy(g => new DateTime(g.Key.Year, g.Key.Month, 1))
			.Select(g =>
			{
				string mois = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM yyyy");
				return new ChartEntry(g.Count())
				{
					Label = mois,
					ValueLabel = g.Count().ToString(),
					Color = SKColor.Parse("#FFA07A")
				};
			}).ToList();
		AjoutChart.Chart = new BarChart { Entries = ajoutsParMois };

    }

	private static string RandomColorHex()
	{
		var rand = new Random(Guid.NewGuid().GetHashCode());

		int r = rand.Next(0, 256);
        int g = rand.Next(0, 256);
        int b = rand.Next(0, 256);
		return $"#{r:X2}{g:X2}{b:X2}";
    }
}
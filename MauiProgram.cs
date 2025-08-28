using EntrepriseEmploye.Services;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;

namespace EntrepriseEmploye;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.UseMicrocharts();
        builder.Services.AddSingleton<DatabaseService>();
		SQLitePCL.Batteries_V2.Init();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
	}
}

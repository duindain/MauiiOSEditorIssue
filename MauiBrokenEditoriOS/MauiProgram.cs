﻿using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using CommunityToolkit.Maui;

namespace MauiBrokenEditoriOS;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.UseMauiCompatibility().ConfigureMauiHandlers(collection =>
		{ 
			collection.AddHandler(typeof(Editor), typeof(MyTextViewHandler));
		});
        return builder.Build();
	}
}


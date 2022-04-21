using Microsoft.EntityFrameworkCore;
using ThreeSixtyPlusAI.Data;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography.X509Certificates;
using ThreeSixtyPlusAI.Services;

var builder = WebApplication.CreateBuilder(args);

if (String.IsNullOrEmpty(builder.Configuration["OpenAIAPIKey"]))
{
	throw new Exception("No OpenAIAPIKey");
}

builder.Services.AddDbContext<ThreeSixtyPlusAIContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("ThreeSixtyPlusAIContext"), o => o.UseNodaTime()));


builder.Services.AddDataProtection()
		.PersistKeysToDbContext<ThreeSixtyPlusAIContext>()
		.ProtectKeysWithCertificate(
			new X509Certificate2("certificate.pfx"));

builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(600);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IOpenAI, OpenAI>();

builder.Services.AddHostedService<ExpireThreeSixtyReviewsService>();

var app = builder.Build();

if (args.Contains("-seed-dev") && app.Environment.IsDevelopment())
{
	using var scope = app.Services.CreateScope();
		var services = scope.ServiceProvider;
		await DevSeed.Initialize(services);
}
else
{
	app.UseExceptionHandler("/Error");

	// Configure the HTTP request pipeline.
	if (!app.Environment.IsDevelopment())
	{
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
		app.UseHttpsRedirection();
	}


	app.UseStaticFiles();

	app.UseRouting();

	app.UseSession();

	app.MapRazorPages();

	app.Run();

}
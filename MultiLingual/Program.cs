using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new System.Globalization.CultureInfo("en-US"),
        new System.Globalization.CultureInfo("fr-FR"),
        new System.Globalization.CultureInfo("es-ES")
    };
    options.DefaultRequestCulture = new RequestCulture("fr-FR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
var app = builder.Build();
// Uncommenting this line will use the default request localization settings
// app.UseRequestLocalization();
// forcing the application to load french.Configure the request localization middleware to use French as the default culture
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("fr-FR"),
    SupportedCultures = new[] { new System.Globalization.CultureInfo("fr-FR") },
    SupportedUICultures = new[] { new System.Globalization.CultureInfo("fr-FR") }
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

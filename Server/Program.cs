using Radzen;
using ActivosFiljos.Server.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents().AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024).AddInteractiveWebAssemblyComponents();
builder.Services.AddControllers();
builder.Services.AddRadzenComponents();
builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "ActivosFiljosTheme";
    options.Duration = TimeSpan.FromDays(365);
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<ActivosFiljos.Server.FixedAssetsDBService>();
builder.Services.AddDbContext<ActivosFiljos.Server.Data.FixedAssetsDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FixedAssetsDBConnection"));
});
builder.Services.AddControllers().AddOData(opt =>
{
    var oDataBuilderFixedAssetsDB = new ODataConventionModelBuilder();
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>("AssetAssignments");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>("AssetAttributes");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>("AssetAttributeValues");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory>("AssetCategories");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>("AssetInsurances");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>("Depreciations");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>("DisposalRecords");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.Document>("Documents");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>("FixedAssets");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.Location>("Locations");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>("MaintenanceRecords");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>("Notifications");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.Role>("Roles");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>("ScheduledMaintenances");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>("UserRoles");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.User>("Users");
    oDataBuilderFixedAssetsDB.EntitySet<ActivosFiljos.Server.Models.FixedAssetsDB.Status>("Statuses");
    opt.AddRouteComponents("odata/FixedAssetsDB", oDataBuilderFixedAssetsDB.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<ActivosFiljos.Client.FixedAssetsDBService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode().AddInteractiveWebAssemblyRenderMode().AddAdditionalAssemblies(typeof(ActivosFiljos.Client._Imports).Assembly);
app.Run();
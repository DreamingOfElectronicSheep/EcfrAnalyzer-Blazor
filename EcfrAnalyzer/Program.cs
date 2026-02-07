using EcfrAnalyzer.Components;
using EcfrAnalyzer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient<IEcfrApiService, EcfrApiService>(client =>
{
    client.BaseAddress = new Uri("https://www.ecfr.gov/api/");
});

var app = builder.Build();

app.MapGet("/api/agencies", async (IEcfrApiService ecfrService, CancellationToken cancellationToken) =>
{
    var agencies = await ecfrService.GetAgenciesAsync(cancellationToken);
    return Results.Ok(agencies);
})
.WithName("GetAgencies");

app.MapGet("/api/corrections", async (IEcfrApiService ecfrService, CancellationToken cancellationToken) =>
{
    var corrections = await ecfrService.GetCorrectionsAsync(cancellationToken);
    return Results.Ok(corrections);
})
.WithName("GetCorrections");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

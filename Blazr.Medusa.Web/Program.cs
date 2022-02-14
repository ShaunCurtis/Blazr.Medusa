using Blazr.Medusa.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<Blazr.Medusa.Red.Data.WeatherForecastService>();
builder.Services.AddSingleton<Blazr.Medusa.Web.Data.WeatherForecastService>();
builder.Services.AddSingleton<Blazr.Medusa.Blue.Data.WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/purple"), app1 =>
{
    app1.UseBlazorFrameworkFiles("/purple");
    app1.UseRouting();
    app1.UseEndpoints(endpoints =>
    {
        endpoints.MapFallbackToPage("/purple/{*path:nonfile}", "/purple");
    });

});

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/grey"), app1 =>
{
    app1.UseBlazorFrameworkFiles("/grey");
    app1.UseRouting();
    app1.UseEndpoints(endpoints =>
    {
        endpoints.MapFallbackToFile("/grey/{*path:nonfile}", "/grey/index.html");
    });

});


app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/green"), app1 =>
{
    app1.UseBlazorFrameworkFiles("/green");
    app1.UseRouting();
    app1.UseEndpoints(endpoints =>
    {
        endpoints.MapFallbackToFile("/green/{*path:nonfile}", "/green/index.html");
    });

});

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/bluer"), app1 =>
{
    app.UsePathBase("/blue");
    app.UseRouting();
    app.MapFallbackToPage("/blue/{*path:nonfile}", "/Blue");
    //app.UseEndpoints(endpoints =>
    //{
    //    endpoints.MapFallbackToPage("/blue/{*path:nonfile}", "/Blue");
    //});
});

app.UseRouting();

app.UseBlazorFrameworkFiles("");

app.MapBlazorHub();
app.MapFallbackToPage("/blue/{*path:nonfile}", "/Blue");
app.MapFallbackToPage("/sky/{*path:nonfile}", "/Hot");
app.MapFallbackToPage("/red/{*path:nonfile}", "/Red");
app.MapFallbackToPage("/_Host");

app.Run();

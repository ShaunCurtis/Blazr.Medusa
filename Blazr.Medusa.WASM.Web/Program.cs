var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

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

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/purple"), app1 =>
{
    app1.UseBlazorFrameworkFiles("/purple");
    app1.UseRouting();
    app1.UseEndpoints(endpoints =>
    {
        endpoints.MapFallbackToFile("/purple/{*path:nonfile}", "/purple/index.html");
    });
});

app.UseBlazorFrameworkFiles("");

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


app.MapFallbackToFile("/index.html");

app.Run();

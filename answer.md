This is a summary of the Repo that demonstrates how to do this.

The sub-folder Web Assembly projects have project files like this.  The import bit is setting the `<StaticWebAssetBasePath>`
```
<Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">

	<PropertyGroup>
		<TargetFramework>net6.0</TargetFramework>
		<Nullable>enable</Nullable>
		<ImplicitUsings>enable</ImplicitUsings>
		<StaticWebAssetBasePath>grey</StaticWebAssetBasePath>
	</PropertyGroup>
....
```

and `Index.html` like this.  We've updated the paths on the `base`, css and the framework js file.

```
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>Blazr.Medusa.Grey</title>
    <base href="/grey/" />
    <link href="/grey/css/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="/grey/css/app.css" rel="stylesheet" />
    <link href="Blazr.Medusa.Grey.styles.css" rel="stylesheet" />
</head>

<body>
    <div id="app">Loading...</div>

    <div id="blazor-error-ui">
        An unhandled error has occurred.
        <a href="" class="reload">Reload</a>
        <a class="dismiss">🗙</a>
    </div>
    <script src="/grey/_framework/blazor.webassembly.js"></script>
</body>

</html>
```

The Web Project has dependencies on all the Web Assembly projects, so they can all be mapped to `wwwwroot`.

The Web Project `Program` looks like this with specific end points for each Web Assembly SPA.  The default maps to the base Web Assembly project - *Blazr.Medusa.WASM*.

```
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
```

The Site Links component in the `MainLayout` of each site provides navigation between the SPA's

```
<div class="p-2 m-2 text-end">
    <button class="btn btn-sm btn-primary me-1" @onclick='() => Go("")'>Go Base</button>
    <button class="btn btn-sm btn-secondary me-1" @onclick='() => Go("grey")'>Go Grey</button>
    <button class="btn btn-sm btn-success me-1" @onclick='() => Go("green")'>Go Green</button>
    <button class="btn btn-sm btn-dark me-1" @onclick='() => Go("purple")'>Go Purple</button>
</div>

@code {

    [Inject] private NavigationManager? NavManager { get; set; }
    
    private void Go(string colour)
        => this.NavManager?.NavigateTo($"/{colour}", true);
}
```

The Repo that contains the full solution is [here](https://github.com/ShaunCurtis/Blazr.Medusa)



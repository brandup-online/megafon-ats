using BrandUp.Website;
using BrandUp.Website.Infrastructure;
using MegafonATS.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"))
               .AddConsole()
               .AddEventLog()
               .AddDebug();

var services = builder.Services;

services.AddRazorPages();
services.AddControllers();

#region Web

services
    .AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProvider>();
        options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProvider>();

        options.MimeTypes = new string[] { "text/html", "text/xml", "text/json", "text/plain", "application/json", "application/xml", "application/javascript", "text/css" };
    })
    .Configure<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProviderOptions>(options =>
    {
        options.Level = System.IO.Compression.CompressionLevel.Fastest;
    })
    .Configure<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProviderOptions>(options =>
    {
        options.Level = System.IO.Compression.CompressionLevel.Fastest;
    });

services.AddResponseCaching();

services.Configure<Microsoft.Extensions.WebEncoders.WebEncoderOptions>(options =>
{
    options.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(System.Text.Unicode.UnicodeRanges.All);
});

services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = false;
    options.LowercaseQueryStrings = true;
});

services.Configure<RequestLocalizationOptions>(options =>
{
    var defaultCulture = new System.Globalization.CultureInfo("ru");
    var supportedCultures = new[] {
                    defaultCulture,
                    //new System.Globalization.CultureInfo("en")
                };

    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

#endregion

services.AddLogging();

services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

services.AddMegafonAtsClient();

services.AddWebsite(options =>
    {
        options.MapConfiguration(builder.Configuration);
    })
    .AddUrlMapProvider<SubdomainUrlMapProvider>()
    .AddSingleWebsite("website title");


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseRequestLocalization();

app.UseWebsite();
app.UseStaticFiles();
app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});


app.Run();

public partial class Program { }

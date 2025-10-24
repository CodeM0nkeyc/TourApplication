namespace TourApp.Web.Maps;

public static class AppSpaExtensions
{
    public static WebApplication MapSpas(this WebApplication app)
    {
        IConfigurationSection spaSection = app.Configuration.GetSection("SpaPaths");
        
        foreach (var section in spaSection.GetChildren())
        {
            if (section.Value is null)
            {
                throw new NullReferenceException("Spa path value cannot be null");
            }
            
            string spaPath = Path.Combine(app.Environment.ContentRootPath, section.Value);
            
            StaticFileOptions staticFileOptions = new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(spaPath)
            };

            app.Map($"/{section.Key}", cfg =>
            {
                cfg.UseSpaStaticFiles(staticFileOptions);
                cfg.UseSpa(spaCfg =>
                {
                    spaCfg.Options.SourcePath = spaPath;
                    spaCfg.Options.DefaultPageStaticFileOptions = staticFileOptions;
                });
            });
        }

        return app;
    }
}
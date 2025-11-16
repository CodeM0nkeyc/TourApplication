var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(opts =>
    {
        opts.AddDefaultPolicy(cfg =>
        {
            cfg.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
    });
}

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.LoginPath = "/auth/signin";
        opts.Cookie.Name = "Cookies";
    });

builder.Services.AddAuthorization();

builder.Services.AddInfrastructure(builder.Configuration.GetSection("Email"));
builder.Services.AddPersistence(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddCoreServices();

builder.Services.Configure<JsonOptions>(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(exHandlerBuilder =>
    {
        exHandlerBuilder.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { error = Error.Internal });
        });
    });
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseCors();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapSpas();

app.Map("/", context =>
{
    context.Response.Redirect("/home");
    return Task.CompletedTask;
});

app.Run();
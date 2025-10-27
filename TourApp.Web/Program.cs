var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.LoginPath = "/auth/signin";
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
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapSpas();

app.Map("/", context =>
{
    context.Response.Redirect("/home");
    return Task.CompletedTask;
});

app.Run();
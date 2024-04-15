
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScrapeList.Data;
using ScrapeList.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ScrapeListContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ScrapeListContext") ?? throw new InvalidOperationException("Connection string 'ScrapeListContext' not found.")), ServiceLifetime.Scoped);
builder.Services.AddTransient<ScraperService>();
builder.Services.AddTransient<WaybackService>();
builder.Services.AddTransient<RecordCheckService>();
builder.Services.AddTransient<DropdownService>();
builder.Services.AddTransient<PopulateDatabaseService>();
builder.Services.AddTransient<PriceRecordUpdateService>();
 // Using the same SQLite connection string.
// Handling JSON serialization object cycle
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // Adjust this to your frontend's URL
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

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

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();



app.Run();

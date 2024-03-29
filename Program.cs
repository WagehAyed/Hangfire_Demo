using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire((sp,config) =>{
var connectionString=sp.GetRequiredService<IConfiguration>().GetConnectionString("DbConnection");
      config.UseSqlServerStorage(connectionString);
});
builder.Services.AddHangfireServer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseHangfireDashboard("/test/job-Dashboard",new DashboardOptions{
    DashboardTitle="Hangfire Job Demo Application",
    DarkModeEnabled=false,
    DisplayStorageConnectionString=false,
    // Authorization = new []{
    //     new HangFireCustomBasicAuthenticationFilter{
    //         User="admin",
    //         pass="admin123"
    //     }
    // }
});
app.Run();

using Courses.Services.Catalog.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//typeof Program.cs dosyası diye belirttik bağlı bulunduğu project içindeki tüm automapper ları tarayacaktır.
builder.Services.AddAutoMapper(typeof(Program));

var dbs = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();

builder.Services.AddSingleton<IDatabaseSettings, DatabaseSettings>(sp => { return dbs; });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

//builder.Services.AddScoped(typeof(ICategoryService), typeof(CategoryService));

//builder.Services.AddScoped(typeof(ICourseService), typeof(CourseService));

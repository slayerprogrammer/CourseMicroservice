using Courses.Services.Catalog.Services.Categories;
using Courses.Services.Catalog.Services.Courses;
using Courses.Services.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    //tokenı kimin dağıttığı url bilgisini veriyoruz.
    options.Authority = builder.Configuration.GetSection("IdentityServerUrl").Value;
    options.Audience = "resource_catalog";
    //https kullanmadığımız için alttakini ekliyoruz
    options.RequireHttpsMetadata = false;
});

//typeof Program.cs dosyası diye belirttik bağlı bulunduğu project içindeki tüm automapper ları tarayacaktır.
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
builder.Services.AddScoped(typeof(ICourseService), typeof(CourseService));

var dbs = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
builder.Services.AddSingleton<IDatabaseSettings, DatabaseSettings>(sp => { return dbs; });

// Add services to the container.
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
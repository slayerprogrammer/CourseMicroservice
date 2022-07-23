using Courses.Services.Discount.Services;
using Courses.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
//policy yazalım.
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
//claimler gelirken sub identitynational olarak geliyor bize biz kişinin id sini alırken
//sub diye aratıyoruz bundan dolayı map olayında sub ı devre dışı bıraktırdık
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    //tokenı kimin dağıttığı url bilgisini veriyoruz.
    options.Authority = builder.Configuration.GetSection("IdentityServerUrl").Value;
    options.Audience = "resource_discount";
    //https kullanmadığımız için alttakini ekliyoruz
    options.RequireHttpsMetadata = false;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(typeof(ISharedIdentityService), typeof(SharedIdentityService));
builder.Services.AddScoped(typeof(IDiscountService), typeof(DiscountService));

// Add services to the container.
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
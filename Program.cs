using System.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sport_Club_Business.Interface;
using Sport_Club_Business.Services;
using Sport_Club_Bussiness;
using Sport_Club_Bussiness.Services;
using Sport_Club_Data;
using Sport_Club_Bussiness.Interface;
using Microsoft.AspNetCore.Identity;
using Sport_Club_Data.Entitys;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => {
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Enter your JWT Access Token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionString").GetSection("Default").Value));

builder.Services.AddScoped<InstructorInterface, InsructorService>();
//                          interface       =>      class exeute interface  builder.Services.AddScoped<MemberInterface,MemberService>();

builder.Services.AddScoped<BeltRankInterface, BeltRankService>();

builder.Services.AddScoped<PaymentInterface,PaymentService>();

builder.Services.AddScoped<BeltTestInterface, BeltTestService>();

builder.Services.AddScoped<SubscriptionPeriodInterface, SubscriptionPeriodService>();

builder.Services.AddScoped<MemberInstructorService>();

builder.Services.AddScoped<UserInterface, UserService>();



//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])
                        )
                    };

                    // ✨ أضف الأحداث هنا
                    o.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            // منع الرد الافتراضي
                            context.HandleResponse();

                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";

                            return context.Response.WriteAsync("{\"message\": \"No Authorization Token Provided\"}");
                        }
                    };
                });


builder.Services.AddScoped<JwtService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();//new
app.UseAuthorization();

app.MapControllers();

app.Run();

using BibliotecaAPI.Manager;
using BibliotecaAPI.Repositories;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Secret"));

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            services.AddControllers(opt => opt.Filters.Add(new AuthorizeFilter()))
                            .AddNewtonsoftJson(options =>
                            {
                                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                            });

            services.AddSwaggerGen(c =>{
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BibliotecaAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Informe o token no formato Bearer: <token>"
                });
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new string[] {}
                        }
                    });
            });

            services.AddSingleton<UsersRepository>();
            services.AddSingleton<CustomersRepository>();
            services.AddSingleton<EmployeerRepository>();
            services.AddSingleton<AuthorRepository>();
            services.AddSingleton<BookRepository>(); 

            services.AddSingleton<JWTTokenService>();

            services.AddTransient<UserService>();
            services.AddTransient<CustomerService>();
            services.AddTransient<EmployeerService>();
            services.AddTransient<AuthorService>();
            services.AddTransient<BookService>();
            services.AddTransient<AddressService>();
            services.AddTransient<LoginManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UsersRepository repository)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BibliotecaAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            repository.Create
                (
                    new Models.User
                    {
                        Username = "admin",
                        Password = "admin",
                        Role = "admin"
                    }
                );

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

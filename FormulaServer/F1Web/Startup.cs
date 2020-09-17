using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using F1.Data;
using F1.Data.Models;
using F1Web.DataAccess.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using F1Web.DataAccess.Interfaces;
using F1Web.Helpers;
using F1Web.Security;
using F1Web.Service.Inerfaces;
using F1Web.Service.Implementations;
using F1Web.Extensions;

namespace F1Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // adding identity and making sure the password can be used
            services.AddIdentity<User, UserRole>()
                .AddEntityFrameworkStores<FormulaDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddDbContext<FormulaDbContext>(options =>
                {
                    options.UseSqlite(InmemoryConnectionHolder.Connection);
                }, ServiceLifetime.Singleton);

            // DAOs would use scoped lifetime to match lifetime of datacontext
            // but due to the in-memo singleton, they have to be singleton themselves too
            services.AddSingleton<IRepository<FormulaTeam>, TeamDao>();

            // adding services
            services.AddSingleton<IFormulaService<FormulaTeam>, FormulaTeamService>();
            // login and logout is based on sessions
            services.AddScoped<IAuthenticationService<User>, IdentityAuthenticationService<User, Guid>>();

            // binding appsettings
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication based on: https://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api 
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            // removing default claims
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(opts =>
            {
                // identity authentication options
                //opts.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                //opts.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                //opts.DefaultSignInScheme = IdentityConstants.ExternalScheme;

                // jwt authentication options
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             // jwt authentication config
            .AddJwtBearer(opts =>
            {
                opts.RequireHttpsMetadata = false;
                opts.SaveToken = true;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                };
            });

            // identity authentication config
            //based on: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio
            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings.
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //
            //    // Lockout settings.
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //    options.Lockout.AllowedForNewUsers = true;
            //
            //    // User settings.
            //    options.User.AllowedUserNameCharacters =
            //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    options.User.RequireUniqueEmail = false;
            //});
            //
            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            //
            //    options.LoginPath = "/Account/Login";
            //    options.SlidingExpiration = true;
            //});

            // normally, if clients are hosted in a different domain, 
            // only registered clients can reach the server, while users can interact with the client
            // this setting is for reasons of simplicity
            services.AddCors(c =>
            {
                c.AddPolicy("Cors", options => {
                    //options.WithOrigins("https://localhost:4200");
                    options
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                } );
               });

            services.AddSingleton<ITokenService, JWTProvider>();

            // we have to wait for the task to complete, this blocks the UI!
            DbHelper.SetupTestDb(services.BuildServiceProvider(), appSettings).Wait();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            
            app.UseCors("Cors");

            app.UseAuthorization();

            app.UseEndpoints(endpts =>
            {
                endpts.MapControllers();
            });
            
        }

    }
}

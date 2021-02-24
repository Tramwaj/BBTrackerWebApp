using BBTracker.App;
using BBTracker.App.Interfaces;
using BBTracker.App.Services;
using BBTracker.Contracts.Services;
using BBTracker.Contracts.ViewModels;
using BBTracker.Persistence.Repos;
using BBTracker.Web.Settings;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketStatsWebApp
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
            services
                .AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<FullPlayerDTO>());

            FluentValidation.ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("pl");

            var jwtSettings = new JwtSettings();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.ValidIssuer,
                        ValidAudience = jwtSettings.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey
                        (
                            Encoding.UTF8.GetBytes(jwtSettings.Secret)
                        ),
                        SaveSigninToken = true
                    };
                });

            services.AddSingleton<IPlayerService, PlayerService>();
            services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<IUserService,UserService>();
            services.AddSingleton<IPlayParser, PlayParser>();
            services.AddSingleton<IPlayService, PlayService>();
            services.AddSingleton<IGameListService, GameListService>();

            services.AddSingleton<GameRepo>();
            services.AddSingleton<PlayerRepo>();
            services.AddSingleton<PlayRepo>();
            services.AddSingleton<UserRepo>();

            services.AddSwaggerGen(c => {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }





            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BBTracker v0.1");
                
            });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v0", new OpenApiInfo { Title = "BasketBall Tracker", Version = "v0" });
                
            //});




            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

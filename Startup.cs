using System;
using System.Text;
using AutoMapper;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TutorSearchSystem.Auth_Models;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Services;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.UnitOfWorks;

namespace TutorSearchSystem
{
    public class Startup
    {
        #region Startup
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        public IConfiguration Configuration { get; }

        #region AddScopeToConfig
        public void AddScopeToConfig(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IClassSubjectService, ClassSubjectService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IFeeService, FeeService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ITuteeService, TuteeService>();
            services.AddScoped<ITutorService, TutorService>();
            services.AddScoped<ITutorTransactionService, TutorTransactionService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthContainer, AuthContainer>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ITutorUpdateProfileService, TutorUpdateProfileService>();
            services.AddScoped<IReportTypeService, ReportTypeService>();
            services.AddScoped<ITuteeReportService, TuteeReportService>();
            services.AddScoped<ITutorReportService, TutorReportService>();
            services.AddScoped<ICourseDetailService, CourseDetailService>();
        }
        #endregion
        #region AddSwaggertoConfig
        public void AddSwaggerToConfig(IServiceCollection service)
        {
            service.AddSwaggerGen(
                c => c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tutor Search API",
                    Description = "TutorSearch System ASP.NET Core Web API",
                    TermsOfService = new Uri(ConstSwaggerUrl.TERMS_OF_SERVICE),
                    Contact = new OpenApiContact
                    {
                        Name = "Duong Chinh Ngu",
                        Email = string.Empty,
                        Url = new Uri(ConstSwaggerUrl.CONTRACT),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri(ConstSwaggerUrl.LICENSE),
                    }
                }
                ));
        }
        #endregion
        // This method gets called by the runtime. Use this method to add services to the container.
        #region InitializeJWTokenBearer
        public void InitializeFirebase(IServiceCollection services)
        {
            AuthContainer container = new AuthContainer();

            var tokenKey = container.SecretKey;
            var key = Encoding.ASCII.GetBytes(tokenKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
        #endregion

        public void ConfigureServices(IServiceCollection services)
        {
            // cors
            services.AddCors();
            
            //
            services.AddDbContext<TSDbContext>(opts =>
            opts.UseSqlServer(Configuration["ConnectionString:TutorSearchDBConnection"]));
            //add scope to config
            AddScopeToConfig(services);
            //mapper
            services.AddAutoMapper(typeof(Startup));
            //add controllers
            services.AddControllers();
            // authentication
            InitializeFirebase(services);
            //swagger
            AddSwaggerToConfig(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Tutor Search System V1");
                    c.RoutePrefix = string.Empty;   
                });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Vu360Sol.Repository.Account;
using Vu360Sol.Repository.AssignDoctors;
using Vu360Sol.Repository.Dashboards;
using Vu360Sol.Repository.Doctors;
using Vu360Sol.Repository.EmailConfiguration;
using Vu360Sol.Repository.Logs;
using Vu360Sol.Repository.Notes;
using Vu360Sol.Repository.Practices;
using Vu360Sol.Repository.RequestDemoes;
using Vu360Sol.Repository.SalePersonDashboards;
using Vu360Sol.Repository.SalePersons;
using Vu360Sol.Repository.Search;
using Vu360Sol.Repository.Visitors;
using Vu360Sol.Service;
using Vu360Sol.Service.Account;
using Vu360Sol.Service.AssignDoctors;
using Vu360Sol.Service.Dashboards;
using Vu360Sol.Service.Doctors;
using Vu360Sol.Service.EmailConfiguration;
using Vu360Sol.Service.Logs;
using Vu360Sol.Service.Notes;
using Vu360Sol.Service.Practices;
using Vu360Sol.Service.RequestDemoes;
using Vu360Sol.Service.SalePersonDashboards;
using Vu360Sol.Service.SalePersons;
using Vu360Sol.Service.Search;
using Vu360Sol.Service.Visitors;
using VU360Sol.Database;

namespace VU360Sol.API
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
            services.AddCors(option1 =>
            {
                option1.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin());
            });
            services.AddDbContextPool<VU360SolContext>(options => options.UseSqlServer(Configuration.GetConnectionString("VU360SolConnection")));

            services.AddControllers();
            services.AddMvc().AddXmlSerializerFormatters();
            var config = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new AutoMapperProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<DoctorService>();

            services.AddScoped<ISalePersonRepository, SalePersonRepository>();
            services.AddScoped<SalePersonService>(); 

            services.AddScoped<IRequestDemoRepository, RequestDemoRepository>();
            services.AddScoped<RequestDemoService>();

            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<NoteService>();

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<LogService>();

            services.AddScoped<IVisitorRepository, VisitorRepository>();
            services.AddScoped<VisitorService>();
            
            services.AddScoped<IAssignDoctorRepository, AssignDoctorRepository>();
            services.AddScoped<AssignDoctorService>();

            services.AddScoped<IFailToImportDoctorsLogRepository, FailToImportDoctorsLogRepository>();
            services.AddScoped<FailToImportDoctorsLogService>();

            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<CommonService>();
            
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<DashboardService>();

            services.AddScoped<ISearchingRepository, SearchingRepository>();
            services.AddScoped<SearchingService>();


            services.AddScoped<ISalePersonDashboardRepository, SalePersonDashboardRepository>();
            services.AddScoped<SalePersonDashboardService>();
            
            services.AddScoped<IPracticeRepository, PracticeRepository>();
            services.AddScoped<PracticesSercice>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                 {
                    ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                         .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                     ValidateIssuer = false,
                     ValidateAudience = false
                };
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

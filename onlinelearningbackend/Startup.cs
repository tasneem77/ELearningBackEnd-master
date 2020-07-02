using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using onlinelearningbackend.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using onlinelearningbackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using onlinelearningbackend.DAL;
using onlinelearningbackend.Repo.IManager;
using onlinelearningbackend.Repo.Manager;
using onlinelearningbackend.Manager;
using onlinelearningbackend.Repository.Repo;
using System.Web.Http;
using Newtonsoft.Json;

namespace onlinelearningbackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        string x = "hi";
        public void ConfigureServices(IServiceCollection services)
        {
            
             services.AddCors(options =>
            {
                options.AddPolicy(x,
                builder =>
                {
                    // builder.WithOrigins("","")
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });






            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.Configure<ApplicationSetting>(Configuration.GetSection("ApplicationSetting"));
            services.AddScoped<ICourseManager,CourseManager>();
            services.AddScoped<ITaskSolutionManager, TaskSolutionManager>();
            services.AddScoped<IBranchManager, BranchManager>();
            services.AddScoped<ITrackManager, TrackManager>();
            services.AddScoped<IIntakeManager, IntakeManager>();
            services.AddScoped<ITaskManager, taskManager>();
            services.AddScoped<IMaterialLinkManager, MaterialLinkManager>();
            services.AddScoped<IMaterialTextManager, MaterialTextManager>();
            services.AddScoped<IMaterialVideoManager, MaterialVideoManager>();
            services.AddScoped<ICourseMaterialManger, CourseMaterialManager>();
            services.AddScoped<IProjectMaterialManager, ProjectMaterialManager>();
            services.AddScoped<IProjectManager, ProjectManager>();
            services.AddScoped<IUserProjectManager, UserProjectManager>();
            services.AddScoped<IInstructorManager, InstructorManager>();
            services.AddScoped<IAdminManager, AdminManager>();
            services.AddScoped<IIntakeManager, IntakeManager>();


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentity<MyUserModel, MyRoleModel>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
               // .AddDefaultUI()
                .AddDefaultTokenProviders();

            //services.AddCors();

           // services.AddControllersWithViews();
            //services.AddRazorPages();

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSetting:JWT_Secret"].ToString());
                    //Encoding.UTF8.GetBytes("MySecretKey")   ;

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(jwt=> {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = false;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,

                };
            });

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x);
            //app.UseCors(options => options.AllowAnyOrigin());
            app.UseEndpoints(endpoints =>
            {
                
               // endpoints.MapControllers();
              
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
                
            });

        }
    }
}

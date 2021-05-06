using CloudinaryDotNet;
using InYourInterest.Data;
using InYourInterest.Data.Models;
using InYourInterest.Models;
using InYourInterest.Services.Categories;
using InYourInterest.Services.Cloudinaries;
using InYourInterest.Services.Events;
using InYourInterest.Services.Groups;
using InYourInterest.Services.Mapping;
using InYourInterest.Services.Posts;
using InYourInterest.Services.Providers;
using InYourInterest.Services.Replies;
using InYourInterest.Services.Tags;
using InYourInterest.Services.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace InYourInterest
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
            services.AddDbContext<Context>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("NewConnection"), b => b.MigrationsAssembly("InYourInterest")));
            //("DefaultConnection")), ServiceLifetime.Transient);
            //services.AddDbContext<Context>(options => options.UseSqlServer(Configuration["DefaultConnection"]));

            services.AddIdentity<User, IdentityRole>
           (options =>
           {
               options.Password.RequiredLength = 3;
               options.Password.RequiredUniqueChars = 0;
               options.Password.RequireLowercase = false;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireUppercase = false;
           })
           .AddEntityFrameworkStores<Context>()
           .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);
            ;
            // .AddDefaultTokenProviders();

            // services.AddAutoMapper(typeof(AppProfile).Assembly);
            var config = new AutoMapper.MapperConfiguration(cfg =>
              {
                  cfg.AddProfile(new AppProfile());
              });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<IConfiguration>(this.Configuration);
            services.AddTransient<Services.Providers.IEmailSender>(x => new SendGridEmailSender("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"));
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IEventsService, EventsService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IGroupsService, GroupsService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<ITagsService, TagsService>();
            services.AddTransient<IRepliesService, RepliesService>();
            //var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY"); 
            //services.AddTransient<Services.Providers.IEmailSender>(serviceProvider =>
            //     new SendGridEmailSender(apiKey));
            //services.AddTransient<IEmailSender>(serviceProvider =>
            //      new SendGridEmailSender(this.Configuration["SendGrid:ApiKey"]));

            // services.AddAutoMapper();

            //// Auto Mapper Configurations
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new MappingProfile());
            //});

            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);

            Account cloudinaryCredentials = new Account(
         this.Configuration["Cloudinary:CloudName"],
         this.Configuration["Cloudinary:ApiKey"],
         this.Configuration["Cloudinary:ApiSecret"]);
            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);
            services.AddAuthentication();
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddServerSideBlazor();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                ////    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute(
                //        name: "areaRoute",
                //        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                           "forumCategory",
                           "f/{name:minlength(3)}",
                           new { controller = "Categories", action = "ByName" });
                endpoints.MapControllerRoute(
                    "areaRoute",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                //endpoints.MapControllerRoute(
                //   name: "Admin",
                //   pattern: "{area:Administration}/{controller=Categories}/{action=Create}"); ///{id?}");
            });
        }
    }
}

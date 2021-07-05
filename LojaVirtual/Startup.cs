using LojaVirtual.Database;
using LojaVirtual.Libraries.AutoMapper;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Manager.Frete;
using LojaVirtual.Libraries.Manager.Payment;
using LojaVirtual.Libraries.Manager.Shipping;
using LojaVirtual.Libraries.Middleware;
using LojaVirtual.Libraries.Session;
using LojaVirtual.Libraries.ShoppingKart;
using LojaVirtual.Repositories;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Mail;
using WSCorreios;
using AutoMapper;
using Coravel;
using LojaVirtual.Libraries.Manager.Scheduler.Invocable;

namespace LojaVirtual
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
            services.AddControllersWithViews();

            services.AddAutoMapper(config => config.AddProfile<MappingProfile>());

            services.AddHttpContextAccessor();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<INewsletterRepository, NewsletterRepository>();
            services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IDeliveryAddressRepository, DeliveryAddressRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderSituationRepository, OrderSituationRepository>();

            services.AddRazorPages().AddRazorRuntimeCompilation();

            /*
             * SMTP
             */

            services.AddScoped<SmtpClient>(options =>
            {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email:ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:Username"), Configuration.GetValue<string>("Email:Password")),
                    EnableSsl = true
                };
                return smtp;
            });

            services.AddScoped<CalcPrecoPrazoWSSoap>(options =>
            {
                var service = new CalcPrecoPrazoWSSoapClient(CalcPrecoPrazoWSSoapClient.EndpointConfiguration.CalcPrecoPrazoWSSoap);
                return service;
            });

            services.AddHttpClient();
            services.AddScoped<EmailManage>();
            services.AddScoped<LojaVirtual.Libraries.Cookie.Cookie>();
            services.AddScoped<CookieShoppingKart>();
            services.AddScoped<CookieFrete>();
            services.AddScoped<WSCorreiosCalcularFrete>();
            services.AddScoped<CalculatePackage>();

            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<Session>();
            services.AddScoped<ClientLogin>();
            services.AddScoped<CollaboratorLogin>();
            services.AddScoped<ManagePagarMe>();

            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LojaVirtual;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            services.AddDbContext<LojaVirtualContext>(options => options.UseSqlServer(connection));

            services.AddTransient<OrderPaymentSituationJob>();
            services.AddTransient<OrderDeliveredJob>();
            services.AddTransient<FinishedOrderJob>(); 
            services.AddTransient<OrderRefundDeliveredJob>();

            services.AddScheduler();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMiddleware<AntiForgeryTokenMiddleware>();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //Scheduler - Coravel

            app.ApplicationServices.UseScheduler(scheduler =>
            {
                scheduler.Schedule<OrderPaymentSituationJob>().EveryTenSeconds();
                scheduler.Schedule<OrderDeliveredJob>().EveryTenSeconds();
                scheduler.Schedule<FinishedOrderJob>().EveryTenSeconds();
                scheduler.Schedule<OrderRefundDeliveredJob>().EveryTenSeconds();
            });
        }
    }
}

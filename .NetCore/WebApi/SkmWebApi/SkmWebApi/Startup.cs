using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using SkmWebApi.Model;

namespace SkmWebApi
{
    //public class Startup
    //{
    //    public Startup(IConfiguration configuration)
    //    {
    //        Configuration = configuration;
    //    }

    //    public IConfiguration Configuration { get; }

    //    // This method gets called by the runtime. Use this method to add services to the container.
    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        string conString = @"Server = BSC-PG01M1KE; Database = SKM; Trusted_Connection = True; ConnectRetryCount = 0";
    //        services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(conString));
    //        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    //        services.AddCors(cor => cor.AddDefaultPolicy(p => p.WithOrigins("http://localhost:4200/").AllowAnyHeader().AllowAnyMethod()));
    //    }

    //    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    //    {
    //        if (env.IsDevelopment())
    //        {
    //            app.UseDeveloperExceptionPage();
    //        }
    //        else
    //        {
    //            app.UseHsts();
    //        }

    //        app.UseHttpsRedirection();
    //        app.UseMvc();
    //    }
    //}

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        readonly string AnotherPolicy = "AnotherPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<TodoContext>(opt =>
            //   opt.UseInMemoryDatabase("TodoList"));

            string conString = @"Server = BSC-PG01M1KE; Database = SKM; Trusted_Connection = True; ConnectRetryCount = 0";
            services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(conString));
            // services.AddCors(cor => cor.AddDefaultPolicy(p => p.WithOrigins("http://localhost:4200/").AllowAnyHeader().AllowAnyMethod()));

            services.AddCors(options => {
                options.AddPolicy(AnotherPolicy,
                    builder => {
                        builder.WithOrigins("http://localhost:4200", "https://localhost:5001").AllowAnyHeader().AllowAnyMethod();
                    });

                options.AddDefaultPolicy(builder => {
                    builder.WithOrigins("http://localhost:4200", "https://localhost:5001");
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // services.AddMvc ().AddJsonOptions (opt => {
            //     opt.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy () };
            // });

            services.AddMvc()
                .AddJsonOptions(opt => {
                    var resolver = opt.SerializerSettings.ContractResolver;
                    if (resolver != null)
                    {
                        var res = resolver as DefaultContractResolver;
                        res.NamingStrategy = null; // <<!-- this removes the camelcasing
                    }
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for 
                // production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseCors (AnotherPolicy);
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

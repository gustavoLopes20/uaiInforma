using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.WebCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace backend
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
            //config db
            var connection = Configuration["ConexaoMySql:MySqlConnectionString"];
            services.AddDbContext<BackendContext>(options =>
                options.UseMySql(connection)
            );
            // Add framework services. AllowSpecificOrigin
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                       builder =>
                       {
                           builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                       });
            });

            // Add framework services.
            services.AddMvc()
            .AddJsonOptions(json => {
                json.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, BackendContext context, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            InicializeDB.Initialize(context);

            app.UseCors((cfg) => {
                cfg.AllowAnyHeader();
                cfg.AllowAnyOrigin();
                cfg.AllowAnyMethod();
            });
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Serve my app-specific default file, if present.
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);
            app.UseStaticFiles();


            //app.UseCors("AllowAllHeaders");
            app.UseMvc();
        }
    }
}

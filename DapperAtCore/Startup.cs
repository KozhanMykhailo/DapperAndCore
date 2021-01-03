using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DapperAtCore
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			string connectionString = "Data Source=DESKTOP-QE04B5G;Initial Catalog=GameStore;Integrated Security=True ; MultipleActiveResultSets=true";
			services.AddControllersWithViews();
			//services.AddSession();
			//services.AddControllers();
			var builder = new ContainerBuilder();
			//builder.RegisterType<DapperGameRepository>().As<IGameRepository>(); - без параметров в конструкторе
			//Ниже способ с перегруженным конструктором
			builder.Register(r => new DapperGameRepository
				(
				conn: connectionString))
				.As<IGameRepository>();
			builder.Populate(services);
			var container = builder.Build();
			return new AutofacServiceProvider(container);
		}
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}

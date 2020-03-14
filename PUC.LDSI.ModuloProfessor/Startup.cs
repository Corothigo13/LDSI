using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PUC.LDSI.DataBase;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.DataBase.Repository;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services;
using PUC.LDSI.Domain.Services.Interfaces;

namespace PUC.LDSI.ModuloProfessor
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AppDbContext>(opc => opc.UseSqlServer(Configuration.GetConnectionString("Conexao"), 
                x => x.MigrationsAssembly("PUC.LDSI.DataBase")));

            services.AddDbContext<SecurityContext>(opc => opc.UseSqlServer(Configuration.GetConnectionString("Conexao"), x => x.MigrationsAssembly("PUC.LDSI.DataBase")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Injeção de Dependência
            //Services
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<IAvaliacaoService, AvaliacaoService>();
            services.AddScoped<ITurmaService, TurmaService>();
            services.AddScoped<IPublicacaoService, PublicacaoService>();
            //Repositórios
            services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IOpcaoAvaliacaoRepository, OpcaoAvaliacaoRepository>();
            services.AddScoped<IQuestaoRepository, QuestaoRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<IPublicacaoRepository, PublicacaoRepository>();
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

using FunctionConsultorio.Domain.Interfaces;
using FunctionConsultorio.Persistence.Context;
using FunctionConsultorio.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionConsultorio.Persistence.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistenceApp(this IServiceCollection services,
                                                    IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            var connectionString = Environment.GetEnvironmentVariable("SQLCONNSTR");
            services.AddDbContext<AppDbContext>(options =>
                     options.UseSqlServer(connectionString, b => b.MigrationsAssembly("ConsultorioFunctions")));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<IProntuarioRepository, ProntuarioRepository>();


        }
    }
}

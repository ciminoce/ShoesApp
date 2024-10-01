using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoesApp.Datos;
using ShoesApp.Datos.Interfaces;
using ShoesApp.Datos.Repositories;
using ShoesApp.Servicios.Interfaces;
using ShoesApp.Servicios.Services;

namespace Gardens2024.Ioc
{
    public static class DI
    {
        public static void ConfigurarServicios(IServiceCollection servicios, IConfiguration configuration)
        {

            servicios.AddScoped<IBrandsRepository, BrandsRepository>();
            servicios.AddScoped<IColoursRepository, ColoursRepository>();
            servicios.AddScoped<IGenresRepository, GenresRepository>();
            servicios.AddScoped<ISportsRepository, SportsRepository>();
            servicios.AddScoped<ISizesRepository, SizesRepository>();
            servicios.AddScoped<IShoesRepository, ShoesRepository>();
            servicios.AddScoped<IShoeColoursRepository, ShoeColoursRepository>();

            servicios.AddScoped<IBrandsService,BrandsService>();
            servicios.AddScoped<IColoursService,ColoursService>();
            servicios.AddScoped<IGenresService, GenresService>();
            servicios.AddScoped<ISportsService, SportsService>();
            servicios.AddScoped<ISizesService, SizesService>();
            servicios.AddScoped<IShoesService, ShoesService>();
            servicios.AddScoped<IShoeColoursService, ShoeColoursService>();


            servicios.AddScoped<IUnitOfWork, UnitOfWork>();
            servicios.AddDbContext<ShoesDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("MyConnection")
                    );
            });


        }
    }
}


using FormBuilderDataLayer.Repository;
using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.FormBuilderRepositories.FormDataRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.FormFieldResultRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.MainFormRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.ResponseRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.SubFormRepos;
using FormBuilderRepositoryLayer.UnitOfWork;
using FormBuilderServiceLayer.Services;
using FormBuilderServiceLayer.UnitOfServices;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderAppLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Adding Database Context
            builder.Services.AddDbContext<FormBuilderContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("ConString")));
            //Adding Units of the program from each layer
            builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            builder.Services.AddScoped<IUnitOfRepositories,UnitOfRepositories>();
            builder.Services.AddScoped<IUnitOfServices,UnitOfServices>();
           //Adding Repositories from Data Layer
            builder.Services.AddScoped<IFormDataRepository, FormDataRespository>();
            builder.Services.AddScoped<IFormFieldResultRepository, FormFieldResultRepository>();
            builder.Services.AddScoped<IMainFormRepository,MainFormRepository>();
            builder.Services.AddScoped<IResponseRepository,ResponseRepository>();
            builder.Services.AddScoped<ISubFormRepository, SubFormRepository>();
            //Adding Services from Service Layer
            builder.Services.AddScoped<FormDataService>();
            builder.Services.AddScoped<FormFieldResultService>();
            builder.Services.AddScoped<MainFormService>();
            builder.Services.AddScoped<ResponseService>();
            builder.Services.AddScoped<SubFormService>();
            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
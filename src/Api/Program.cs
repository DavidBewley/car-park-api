using FluentValidation;
using Api.Validators;
using Core.Configuration;
using Core.Interfaces;
using Core.Processors;
using FluentValidation.AspNetCore;
using Services;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<PriceRequestValidator>();

            builder.Services.AddTransient<ICarParkRepository>(s=> new CarParkRepository(config.Database.ConnectionString));
            builder.Services.AddTransient<AvailabilityProcessor>();
            builder.Services.AddTransient<BookingProcessor>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
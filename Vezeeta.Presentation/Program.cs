
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Application.Contract.AppointmentRepositories;
using Vezeeta.Application.Contract.BookingRepositories;
using Vezeeta.Application.Contract.CountriesRepositries;
using Vezeeta.Application.Contract.DoctorRepositories;
using Vezeeta.Application.Contract.ReviewsRepositories;
using Vezeeta.Application.Contract.ServicesRepositories;
using Vezeeta.Application.Contract.SpecialtyRepositories;
using Vezeeta.Application.Contract.WorkingPlaceRepositories;
using Vezeeta.Application.Services.BookingServices;
using Vezeeta.Application.Services.Countries_Services;
using Vezeeta.Application.Services.DoctorServices;
using Vezeeta.Application.Services.ReviewsServices;
using Vezeeta.Application.Services.ServicesServices;
using Vezeeta.Application.Services.Specialty_Services;
using Vezeeta.Context;
using Vezeeta.Infrastucture.AppointmentRepos;
using Vezeeta.Infrastucture.BookingRepos;
using Vezeeta.Infrastucture.CountriesRepos;
using Vezeeta.Infrastucture.DoctorRepos;
using Vezeeta.Infrastucture.ReviewsRepos;
using Vezeeta.Infrastucture.ServicesRepos;
using Vezeeta.Infrastucture.SpecialtyRepos;
using Vezeeta.Infrastucture.WorkingPlaceRepos;
using Vezeeta.Models;

namespace Vezeeta.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<VezeetaContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("Db"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                  .AddEntityFrameworkStores<VezeetaContext>()
                      .AddDefaultTokenProviders();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<ICountriesRepository, CountriesRepositories>();
            builder.Services.AddScoped<ICountriesServices , CountriesServices>();
            builder.Services.AddScoped<ICountryImagesRepository,CountriesImagesRepository>();
            builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
            builder.Services.AddScoped<ISpecialtyServices, SpecialtyServices>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IDoctorServices, DoctorServices>();
            builder.Services.AddScoped<IWorkingPlaceRepository,WorkingPlaceRepository>();
            builder.Services.AddScoped<IDoctorWorkingPlaceRepository,DoctorWorkingPlaceRepository>();
            builder.Services.AddScoped<ISubSpecialtyRepository, SubSpecialtyRepository>();
            builder.Services.AddScoped<IDoctorSubSpecialtyRepository,DoctorSubSpecialtyRepository>();
            builder.Services.AddScoped<IAppointmentRepository,AppointmentRepository>();
            builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
            builder.Services.AddScoped<IDoctorReviewsRepository,DoctorReviewsRepository>();
            builder.Services.AddScoped<IDoctorReviewesServices,DoctorReviewsServices>();
            builder.Services.AddScoped<IWorkingPlaceImagesRepository,WorkingPlaceImagesRepository>();
            builder.Services.AddScoped<ISubSpecialtyRepository,SubSpecialtyRepository>();
            builder.Services.AddScoped<ISubSpecialtyServices,SubSpecialtyServices>();
            builder.Services.AddScoped<ITeleAppointmentRepository, TeleAppointmentRepository>();
            builder.Services.AddScoped<ITeleTimeSlotRepository, TeleTimeSlotRepository>();
            builder.Services.AddScoped<IDoctorBookingRepository,DoctorBookingRepository>();
            builder.Services.AddScoped<IDoctorBookingServices,DoctorBookingServices>();
            builder.Services.AddScoped<ITeleBookingRepository, TeleBookingRepository>();
            builder.Services.AddScoped<ITeleBookingServices , TeleBookingServices>();
            builder.Services.AddScoped<ISubServicesRepository,SubServicesRepository>();
            builder.Services.AddScoped<IServicesRepository, SevicesRepository>();
            builder.Services.AddScoped<ISubServicesAppointmentRepository, SubServicesAppointmentRepository>();
            builder.Services.AddScoped<ISubServicesBookingRepository,SubServicesBookingRepository>();
            builder.Services.AddScoped<ISubServicesImagesRepository, SubServicesImagesRepository>();
            builder.Services.AddScoped<ISubServicesReviewsRepository, SubServicesReviewsRepository>();
            builder.Services.AddScoped<ISubServicesTimeSlotRepository,SubServicesTimesSlotRepository>();
            builder.Services.AddScoped<IServicesServices, ServicesServices>();
            builder.Services.AddScoped<ISubServicesServices,SubServicesServices>();
            builder.Services.AddScoped<ISubServicesReviewsServices, SubServicesReviewsServices>();
            builder.Services.AddScoped<ISubServicesBookingServices, SubServicesBookingServices>();




            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

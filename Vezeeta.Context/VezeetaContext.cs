using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Context
{
    public class VezeetaContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<CountriesImages> CountriesImages { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TeleAppointments> TeleAppointments { get; set;}
        public DbSet<SubServicesAppointments> SubServicesAppointments { get;set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<TeleTimeSlot> TeleTimeSlots { get; set; }
        public DbSet<SubServicesTimeSlot> SubServicesTimeSlots { get; set;}
        public DbSet<DoctorBooking> DoctorBooking { get; set; }
        public DbSet<TeleBooking> TeleBookings { get; set; }
        public DbSet<SubServicesBooking> SubServicesBookings { get; set;}
        public DbSet<WorkingPlace> WorkingPlaces { get; set;}
        public DbSet<DoctorWorkingPlace> DoctorWorkingPlaces { get; set;}
        public DbSet<WorkingPlaceImages> WorkingPlacesImages { get; set;}
        public DbSet<DoctorReviews> DoctorReviews { get; set;}
        public DbSet<SubServiceReview> SubServiceReviews { get; set; }
        public DbSet<SubServices> SubServices { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Specialty> Specialty { get; set; }
        public DbSet<SubSpecialty> SubSpecialty { get; set; }
        public DbSet<DoctorSubSpecialties> DoctorSubSpecialties { get; set; }
        public DbSet<SubServiceImages> SubServiceImages { get; set; }
        public VezeetaContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Doctor>()
                         .HasOne(d => d.Specialty)
                         .WithMany(s => s.Doctors)
                         .HasForeignKey(d => d.SpecId)
                         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubSpecialty>()
                .HasOne(ss => ss.Specialty)
                .WithMany(s => s.SubSpecialties)
                .HasForeignKey(ss => ss.SpecId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DoctorSubSpecialties>()
                .HasOne(ds => ds.Doctor)
                .WithMany(d => d.DoctorSubSpecialties)
                .HasForeignKey(ds => ds.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DoctorSubSpecialties>()
                .HasOne(ds => ds.SubSpecialty)
                .WithMany(ss => ss.DoctorSubSpecialties)
                .HasForeignKey(ds => ds.SubSpecId)
                .OnDelete(DeleteBehavior.NoAction);




            modelBuilder.Entity<SubServicesBooking>()
        .HasOne(ssb => ssb.SubServices)
        .WithMany(ss => ss.SubServicesBooking)
        .HasForeignKey(ssb => ssb.SubServicesId)
        .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<SubServicesBooking>()
                .HasOne(ssb => ssb.User)
                .WithMany(s => s.SubServicesBooking) 
                .HasForeignKey(ssb => ssb.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<SubServicesBooking>()
                .HasOne(ssb => ssb.SubServiceTimeSlot)
                .WithMany(s => s.SubServicesBookings) 
                .HasForeignKey(ssb => ssb.SubServiceTimeSlotId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<DoctorBooking>()
                .HasOne(db => db.Doctor)
                .WithMany(d => d.DoctorBooking)
                .HasForeignKey(db => db.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DoctorBooking>()
                .HasOne(db => db.User)
                .WithMany(s => s.DoctorBooking)  
                .HasForeignKey(db => db.UserId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<DoctorBooking>()
                .HasOne(db => db.TimeSlot)
                .WithMany(s => s.DoctorBooking)  
                .HasForeignKey(db => db.TimeSlotId)
                .OnDelete(DeleteBehavior.Restrict);




            modelBuilder.Entity<TeleBooking>()
        .HasOne(tb => tb.Doctor)
        .WithMany(d => d.TeleBookings)
        .HasForeignKey(tb => tb.DoctorId)
        .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<TeleBooking>()
                .HasOne(tb => tb.User)
                .WithMany(s => s.TeleBookings)  
                .HasForeignKey(tb => tb.UserId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<TeleBooking>()
                .HasOne(tb => tb.TeleTimeSlot)
                .WithMany(s => s.TeleBooking)  
                .HasForeignKey(tb => tb.TeleTimeSlotId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}

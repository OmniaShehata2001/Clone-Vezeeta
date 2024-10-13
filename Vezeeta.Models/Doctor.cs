using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AboutDoctor { get; set; }
        public string? DoctorImage { get; set; }
        public decimal Fees { get; set; }
        public int WaitingTime { get; set; }
        public string PhoneNumber { get; set; }
        public string SSN { get; set; }
        public int? AppointmentDurationMinutes { get; set; }
        public Gender Gender { get; set; }
        [ForeignKey("Countries")]
        public int CountryId { get; set; }
        public Countries Countries { get; set; }
        public ICollection<DoctorWorkingPlace> WorkingPlaces { get; set;}
        [ForeignKey("Specialty")]
        public int SpecId { get; set; }
        public Specialty Specialty { get; set; }
        public ICollection<DoctorSubSpecialties> DoctorSubSpecialties { get; set; }
        public ICollection<DoctorReviews> DoctorReviews { get; set;}
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<DoctorBooking> DoctorBooking { get; set;}
        public ICollection<TeleAppointments>? TeleAppointments { get; set; }
        public ICollection<TeleBooking>? TeleBookings { get; set; }

    }
}

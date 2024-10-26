using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.AppointmentRepositories;
using Vezeeta.Context;
using Vezeeta.Infrastucture.Repository;
using Vezeeta.Models;

namespace Vezeeta.Infrastucture.AppointmentRepos
{
    public class AppointmentRepository : Repository<Appointment, int>, IAppointmentRepository
    {
        private readonly VezeetaContext _vezeetaContext;

        public AppointmentRepository(VezeetaContext vezeetaContext) : base(vezeetaContext)
        {
            _vezeetaContext = vezeetaContext;
        }

        public Task<IQueryable<Appointment>> GetAppByDoctorId(int DoctorId)
        {
            return Task.FromResult(_vezeetaContext.Appointments.Where(s => s.DoctorId == DoctorId && s.IsDeleted == false));
        }
    }
}
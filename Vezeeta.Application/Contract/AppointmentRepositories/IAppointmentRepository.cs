using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Application.Contract.AppointmentRepositories
{
    public interface IAppointmentRepository : IRepository<Appointment , int>
    {
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Models;

namespace Vezeeta.Application.Contract.ServicesRepositories
{
    public interface ISubServicesAppointmentRepository : IRepository<SubServicesAppointments , int>
    {
    }
}
using Microsoft.EntityFrameworkCore;
using Participation_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Participation_Microservice.DBContext
{
    public class ParticipationContext:DbContext
    {
        public ParticipationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Participation> Participations { get; set; }
    }
}

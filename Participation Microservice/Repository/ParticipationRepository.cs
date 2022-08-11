using Participation_Microservice.DBContext;
using Participation_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Participation_Microservice.Repository
{
    public class ParticipationRepository : IParticipationRepository
    {
        private readonly ParticipationContext _context;
        public ParticipationRepository(ParticipationContext context)
        {
            _context = context;
        }

        public async Task<Participation> CreateParticipation(Participation participation)
        {
            throw new NotImplementedException();
        }

        public async Task<Participation> DeleteParticipation(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Participation>> GetApprovedParticipation()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Participation>> GetDeclinedParticipation()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Participation>> GetParticipation()
        {
            throw new NotImplementedException();
        }

        public async Task<Participation> GetParticipationById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Participation>> GetPendingParticipation()
        {
            throw new NotImplementedException();
        }

        public async Task<Participation> PutParticipation(int id, Participation participation)
        {
            throw new NotImplementedException();
        }
    }
}

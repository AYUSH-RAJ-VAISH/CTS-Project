using Participation_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Participation_Microservice.Repository
{
    public interface IParticipationRepository
    {
        public Task<Participation> CreateParticipation(Participation participation);
        public Task<Participation> DeleteParticipation(int id);
        public Task<Participation> GetParticipationById(int id);
        public Task<IEnumerable<Participation>> GetParticipation();
        public Task<IEnumerable<Participation>> GetApprovedParticipation();
        public Task<IEnumerable<Participation>> GetDeclinedParticipation();
        public Task<IEnumerable<Participation>> GetPendingParticipation();
        public Task<Participation> PutParticipation(int id, Participation participation);
    }
}

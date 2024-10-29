using Candidate_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Services
{
    public interface ICandidateProfileService
    {
        public CandidateProfile GetCandidateProfileById(string id);

        public List<CandidateProfile> GetCandidateProfiles();

        public bool AddCandidateProfile(CandidateProfile candidateProfile);

        public bool DeleteCandidateProfile(string profileID);

        public bool UpdateCandidateProfile(CandidateProfile candidateProfile);
    }
}

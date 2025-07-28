using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sport_Club_Bussiness.DTOs;

namespace Sport_Club_Bussiness.Interface
{
    public interface MemberInstructorInterface
    {
        public Task<List<MemberInstructorDTos>> GetAllAssigment();
        public Task<List<MemberInstructorDTos>> GetAllAssigmentByMemberID(int MemberID);
        public Task<List<MemberInstructorDTos>> GetAllAssigmentByInstructorID(int InstructorID);
        public Task<MemberInstructorDTos?> GetAssignByIdAsync(int memberId, int instructorId);
        public Task<MemberInstructorDTos> AddAsssignAsync(MemberInstructorDTos dto);
        public Task<bool> DeleteAssignAsync(int memberId, int instructorId);
    }
}

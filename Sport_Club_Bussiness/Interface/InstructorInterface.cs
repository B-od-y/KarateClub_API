using System.Collections.Generic;
using System.Threading.Tasks;
using Sport_Club_Bussiness;
using Sport_Club_Bussiness.DTOs; 

namespace Sport_Club_Business.Interface
{
    public interface InstructorInterface
    {
      
       public Task<List<instructorDTos>> GetAllAsync();
       public Task<instructorDTos?> GetByIdAsync(int id);
       public Task<instructorDTos> AddAsync(instructorDTos instructorDto);
       public Task<instructorDTos> UpdateAsync(int id, instructorDTos instructorDto);
       public Task<bool> DeleteAsync(int id);
    }
}
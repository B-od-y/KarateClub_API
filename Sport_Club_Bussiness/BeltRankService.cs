using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sport_Club_Business.Interface;
using Sport_Club_Bussiness.DTOs;
using Sport_Club_Data;
using Sport_Club_Data.Entitys;

namespace Sport_Club_Bussiness
{
    public class BeltRankService : BeltRankInterface
    {
        private readonly AppDbContext _context;

        public BeltRankService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BeltRankDTos>> GetAllAsync()
        {
            return await _context.BeltRankss.Select(b =>
                new BeltRankDTos
                {
                    BeltRankID = b.BeltRankID,
                    BeltName = b.BeltName,
                    BeltTestFees = b.BeltTestFees
                }).ToListAsync();
        }

        public async Task<BeltRankDTos?> GetByIdAsync(int id)
        {
            return await _context.BeltRankss.Where(i => i.BeltRankID == id).Select(b =>
                new BeltRankDTos
                {
                    BeltRankID = b.BeltRankID,
                    BeltName = b.BeltName,
                    BeltTestFees = b.BeltTestFees
                }).FirstOrDefaultAsync();
        }

        public async Task<BeltRankDTos> AddAsync(BeltRankDTos beltrankDto)
        {
            var BRDTO = new BeltRanks
            {
                BeltName = beltrankDto.BeltName,
                BeltTestFees = beltrankDto.BeltTestFees
            };

            await _context.BeltRankss.AddAsync(BRDTO);
            await _context.SaveChangesAsync();

            return new BeltRankDTos
            {
                BeltRankID = BRDTO.BeltRankID,
                BeltName = BRDTO.BeltName,
                BeltTestFees = BRDTO.BeltTestFees
            };
        }

        public async Task<BeltRankDTos> UpdateAsync(int id, BeltRankDTos beltrankDto)
        {
            var BeltRank = await _context.BeltRankss.FindAsync(id);
            if (BeltRank == null)
                return null;

            BeltRank.BeltName = beltrankDto.BeltName;
            BeltRank.BeltTestFees = beltrankDto.BeltTestFees;

            await _context.SaveChangesAsync();

            return new BeltRankDTos
            {
                BeltRankID = BeltRank.BeltRankID,
                BeltName = BeltRank.BeltName,
                BeltTestFees = BeltRank.BeltTestFees
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _context.BeltRankss.FindAsync(id);
            if (member == null)
            {
                return false;
            }

            _context.BeltRankss.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

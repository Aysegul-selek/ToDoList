using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Data.IRepositories;
using ToDoAPI.Entities.Entities;

namespace ToDoAPI.Data.Repositories
{
    // StatusRepository
    public class StatusRepository : IStatusRepository
    {
        private readonly TodoDbContext _context;

        public StatusRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Status>> GetAllStatusesAsync()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<Status> GetStatusByIdAsync(int StatusId)
        {
            return await _context.Statuses.FirstOrDefaultAsync(c => c.StatusId == StatusId);
        }

        public async Task<Status> CreateStatusAsync(Status Status)
        {
            _context.Statuses.Add(Status);
            await _context.SaveChangesAsync();
            return Status;
        }

        public async Task<Status> UpdateStatusAsync(Status Status)
        {
            _context.Statuses.Update(Status);
            await _context.SaveChangesAsync();
            return Status;
        }

        public async Task<bool> DeleteStatusAsync(int StatusId)
        {
            var Status = await _context.Statuses.FirstOrDefaultAsync(c => c.StatusId == StatusId);
            if (Status != null)
            {
                _context.Statuses.Remove(Status);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

}

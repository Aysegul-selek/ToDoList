using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities.Auth;
using ToDoAPI.Entities.Entities;

namespace ToDoAPI.Data.IRepositories
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetAllStatusesAsync();
        Task<Status> GetStatusByIdAsync(int StatusId);
        Task<Status> CreateStatusAsync(Status Status);
        Task<Status> UpdateStatusAsync(Status Status);
        Task<bool> DeleteStatusAsync(int StatusId);
    }
}

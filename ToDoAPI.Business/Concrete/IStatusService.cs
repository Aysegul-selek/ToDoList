using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Entities;
using ToDoAPI.Entities.DTOs;

namespace ToDoAPI.Business.Concrete
{
    public interface IStatusService
    {
        Task<List<StatusDto>> GetAllStatusesAsync();
        Task<StatusDto> GetStatusByIdAsync(int statusId);
        Task<StatusDto> CreateStatusAsync(StatusDto statusDto);
        Task<StatusDto> UpdateStatusAsync(StatusDto statusDto);
        Task<bool> DeleteStatusAsync(int statusId);
    }
}

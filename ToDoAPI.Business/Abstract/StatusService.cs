using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Business.Concrete;
using ToDoAPI.Data.IRepositories;
using ToDoAPI.Entities.DTOs;
using ToDoAPI.Entities.Entities;

namespace ToDoAPI.Business.Abstract
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusService(IStatusRepository StatusRepository, IMapper mapper)
        {
            _statusRepository = StatusRepository;
            _mapper = mapper;
        }

        public async Task<StatusDto> CreateStatusAsync(StatusDto StatusDto)
        {
            var Status = _mapper.Map<Status>(StatusDto);
            var createdStatus = await _statusRepository.CreateStatusAsync(Status);
            return _mapper.Map<StatusDto>(createdStatus);
        }

        public async Task<bool> DeleteStatusAsync(int StatusId)
        {
            return await _statusRepository.DeleteStatusAsync(StatusId);
        }

        public async Task<List<StatusDto>> GetAllStatusesAsync()
        {
            var categories = await _statusRepository.GetAllStatusesAsync();
            return _mapper.Map<List<StatusDto>>(categories);
        }

        public async Task<StatusDto> GetStatusByIdAsync(int StatusId)
        {
            var Status = await _statusRepository.GetStatusByIdAsync(StatusId);
            return _mapper.Map<StatusDto>(Status);
        }

        public async Task<StatusDto> UpdateStatusAsync(StatusDto StatusDto)
        {
            var Status = _mapper.Map<Status>(StatusDto);
            var updatedStatus = await _statusRepository.UpdateStatusAsync(Status);
            return _mapper.Map<StatusDto>(updatedStatus);
        }
    }
    }

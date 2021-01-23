using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Malzamaty.Model;
namespace Malzamaty.Services
{
    public interface IScheduleService : IBaseService<Schedule,Guid>
    {
        Task<IEnumerable<Schedule>> GetUserSchedules(Guid Id);
        Task<IEnumerable<Schedule>> GetUserSchedules(int PageNumber, int Count, Guid Id);
        Task<bool> IsBewteenTwoDates(DateTimeOffset dt, DateTimeOffset start, DateTimeOffset end);
    }

    public class ScheduleService : IScheduleService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ScheduleService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<Schedule>> All(int PageNumber, int Count) =>await _repositoryWrapper.Schedule.FindAll(PageNumber, Count);
        public async Task<Schedule> Create(Schedule Schedule) =>await
             _repositoryWrapper.Schedule.Create(Schedule);
        public async Task<Schedule> Delete(Guid id)=> await
        _repositoryWrapper.Schedule.Delete(id);

        public async Task<Schedule> FindById(Guid id)=> await
        _repositoryWrapper.Schedule.FindById(id);

        public async Task<IEnumerable<Schedule>> GetUserSchedules(Guid Id) =>await _repositoryWrapper.Schedule.GetUserSchedules(Id);

        public async Task<IEnumerable<Schedule>> GetUserSchedules(int PageNumber, int Count, Guid Id) => await
        _repositoryWrapper.Schedule.GetUserSchedules(PageNumber,Count,Id);

        public async Task<bool> IsBewteenTwoDates(DateTimeOffset dt, DateTimeOffset start, DateTimeOffset end) =>await
       _repositoryWrapper.Schedule.IsBewteenTwoDates(dt,start,end);

        public async Task<Schedule> Modify(Guid id, Schedule Schedule)
        {
            var ScheduleModelFromRepo =await _repositoryWrapper.Schedule.FindById(id);
            if (ScheduleModelFromRepo == null)
            {
                return null;
            }
            ScheduleModelFromRepo.StartStudy = Schedule.StartStudy;
            ScheduleModelFromRepo.FinishStudy = Schedule.FinishStudy;
            ScheduleModelFromRepo.Subject =await _repositoryWrapper.Subject.FindById(Schedule.Subject.ID);
            _repositoryWrapper.Save();
            return  Schedule;
        }

    }
}
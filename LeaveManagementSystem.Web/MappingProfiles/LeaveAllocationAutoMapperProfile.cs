using LeaveManagementSystem.Web.Models.LeaveAllocations;
using AutoMapper;
using LeaveManagementSystem.Web.Models.Periods;
using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.MappingProfiles
{
    public class LeaveAllocationAutoMapperProfile : Profile
    {
        public LeaveAllocationAutoMapperProfile()
        {
            CreateMap<LeaveAllocation, LeaveAllocationVM>();
            CreateMap<LeaveAllocation, LeaveAllocationEditVM>();
            CreateMap<Period, PeriodVM>();
            CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();
            CreateMap< ApplicationUser, EmployeeListVM > ();
        }
    }
}

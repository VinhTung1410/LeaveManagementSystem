using LeaveManagementSystem.Web.Models.LeaveAllocations;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public interface ILeaveAllocationsService
    {
        Task AllocationLeave(string employeeId);
        Task<List<LeaveAllocation>> GetAllocations();
        Task<EmployeeAllocationVM> GetEmployeeAllocation();
    }
}

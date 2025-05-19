using LeaveManagementSystem.Web.Models.LeaveAllocations;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public interface ILeaveAllocationsService
    {
        Task AllocationLeave(string employeeId);
        Task EditAllocation(LeaveAllocationEditVM allocationEditVM);
        Task<EmployeeAllocationVM> GetEmployeeAllocation(string? userId);
        Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId);
        Task<List<EmployeeListVM>> GetEmployees();
    }
}

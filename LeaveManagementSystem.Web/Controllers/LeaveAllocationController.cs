using Microsoft.AspNetCore.Mvc;
using LeaveManagementSystem.Web.Services.LeaveAllocations;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveAllocationController(ILeaveAllocationsService _leaveAllocationsService) : Controller
    {
        public async Task<IActionResult> Details()
        {
            var employeeVm = await _leaveAllocationsService.GetEmployeeAllocation();
            return View(employeeVm);
        }
    }
}


using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public class LeaveAllocationsService(ApplicationDbContext _context, IHttpContextAccessor _httpContextAccessor
        ,UserManager<ApplicationUser> _userManager, IMapper _mapper) : ILeaveAllocationsService
    {
        public async Task AllocationLeave(string employeeId)
        {
            var leaveTypes = await _context.LeaveTypes.ToListAsync();
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(a => a.EndDate.Year == currentDate.Year);
            var monthsRemaining = period.EndDate.Month - currentDate.Month;

            foreach (var leaveType in leaveTypes)
            {
                var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
                var leaveAllocation = new LeaveAllocation
                {
                    Days = (int)Math.Ceiling(accuralRate * monthsRemaining),
                    PeriodId = period.Id,
                    LeaveTypeId = leaveType.Id,
                    EmployeeId = employeeId
                };
                _context.Add(leaveAllocation);
                
            }
            await _context.SaveChangesAsync();
        }


        public async Task<List<LeaveAllocation>> GetAllocations()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            var currentDate = DateTime.Now;
            //var period = await _context.Periods.SingleAsync(a => a.EndDate.Year == currentDate.Year);
            //var leaveAllocations = await _context.LeaveAllocations
            //    .Include(a => a.LeaveType)
            //    .Include(a => a.Period)
            //    .Where(a => a.EmployeeId == user.Id && a.PeriodId == period.Id)
            //    .ToListAsync();
            var leaveAllocations = await _context.LeaveAllocations
                .Include(a => a.LeaveType)
            .Include(a => a.Period)
                .Where(a => a.EmployeeId == user.Id && a.Period.EndDate.Year == currentDate.Year)
                .ToListAsync();
            return leaveAllocations;
        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocation()
        {
            var allocations = await GetAllocations();
            var allocationVmList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            var employeeVm = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                LeaveAllocations = allocationVmList
            };
            return employeeVm;
        }
    }
}

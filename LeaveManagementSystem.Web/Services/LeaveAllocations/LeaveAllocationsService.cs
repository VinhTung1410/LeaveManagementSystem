
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
            var leaveTypes = await _context.LeaveTypes
                .Where(a => !a.LeaveAllocations.Any(q => q.EmployeeId == employeeId))
                .ToListAsync();
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

        public async Task<EmployeeAllocationVM> GetEmployeeAllocation(string? userId)
        {
            var user = string.IsNullOrEmpty(userId)
            ? await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User)
            : await _userManager.FindByIdAsync(userId);
            var allocations = await GetAllocations(user.Id);
            var allocationVmList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
            var leaveTypesCount = await _context.LeaveTypes.CountAsync();

            var employeeVm = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                LeaveAllocations = allocationVmList,
                IsCompletedAllocation = leaveTypesCount == allocations.Count
            };
            return employeeVm;
        }

        public async Task<List<EmployeeListVM>> GetEmployees()
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);
            var employees = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());

            return employees;
        }

        public async Task EditAllocation(LeaveAllocationEditVM allocationEditVM)
        {
            //var leaveAllocation = await GetEmployeeAllocation(allocationEditVM.Id);
            //if (leaveAllocation == null)
            //{
            //    throw new Exception("Leave allocation not found");
            //}
            //leaveAllocation.Days = allocationEditVM.Days;
            // _context.Update(leaveAllocation);
            // _context.Entry(leaveAllocation).State = EntityState.Modified;

            await _context.LeaveAllocations
                .Where(a => a.Id == allocationEditVM.Id)
                .ExecuteUpdateAsync(a => a.SetProperty(e=>e.Days, allocationEditVM.Days));
        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
        {
            var allocation = await _context.LeaveAllocations
                .Include(a => a.LeaveType)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == allocationId);

            var model = _mapper.Map<LeaveAllocationEditVM>(allocation);

            return model;

        }

        private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
        {
            string employeeId = string.Empty;
            if (!string.IsNullOrEmpty(userId))
            {
                employeeId = userId;
            }
            else
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
                employeeId = user.Id;
            }

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
                .Where(a => a.EmployeeId == userId && a.Period.EndDate.Year == currentDate.Year)
                .ToListAsync();
            return leaveAllocations;
        }

        private async Task<bool> AllocationExists(string userId, int periodId, int leaveTypeId)
        {
            var exists = await _context.LeaveAllocations.AnyAsync(q =>
                q.EmployeeId == userId
                && q.LeaveTypeId == leaveTypeId
                && q.PeriodId == periodId
            );

            return exists;
        }

        public Task<bool> DayExceedMaximum(int id, int days)
        {
            throw new NotImplementedException();
        }
    }
}

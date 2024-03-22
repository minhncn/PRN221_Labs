using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pos_System.Repository.Interfaces;
using SignalRAssignment.Domain.Data;
using SignalRAssignment.Domain.Models;
using SignalRAssignment.Services.Interfaces;
using System.Linq.Expressions;

namespace SignalRAssignment.Services.Implements
{
    public class AccountService : BaseService<AccountService>, IAccountService
    {
        public AccountService(IUnitOfWork<ShoppingDbContext> unitOfWork, ILogger<AccountService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<Account> LoginByUsernameAndPassword(string userName, string password)
        {
            Expression<Func<Account, bool>> searchFilter = p =>
                p.UserName.Trim().Equals(userName.Trim()) 
                && p.Password.Trim().Equals(password.Trim());
            Account account = await _unitOfWork.GetRepository<Account>()
                .SingleOrDefaultAsync(predicate: searchFilter);
            if (account != null)
            {
                return account;
            }
            return null;
        }

        private bool VerifyPassword(Account account, string password)
        {
            return account.Password == password;
        }
    }
}

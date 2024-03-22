using SignalRAssignment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRAssignment.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Account> LoginByUsernameAndPassword(string userName, string password);
    }
}

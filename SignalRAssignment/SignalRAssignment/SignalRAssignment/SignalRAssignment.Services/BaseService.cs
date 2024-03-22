using Microsoft.Extensions.Logging;
using Pos_System.Repository.Interfaces;
using SignalRAssignment.Domain.Data;

namespace SignalRAssignment.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected IUnitOfWork<ShoppingDbContext> _unitOfWork;
        protected ILogger<T> _logger;
        public BaseService(IUnitOfWork<ShoppingDbContext> unitOfWork, ILogger<T> logger) 
        { 
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
    }

}

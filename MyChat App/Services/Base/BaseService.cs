using Domain.Interfaces.Base;
using Utilities.Interfaces;

namespace MyChat_App.Services.Base
{
    public abstract class BaseService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IUnitOfWork _unitOfWork;

        protected CurrentUserInfo User
        {
            get
            {
                var userInfo = _serviceProvider.GetService<ICurrentUserInfo>();
                if (userInfo != null && userInfo.Id != 0)
                {
                    return new CurrentUserInfo(userInfo.Id, userInfo.Name, userInfo.Email);
                }
                return null;
            }
        }

        protected BaseService(IServiceProvider serviceProvider
            , IUnitOfWork unitOfWork)
        {
            _serviceProvider = serviceProvider;
            _unitOfWork = unitOfWork;
        }
    }
}

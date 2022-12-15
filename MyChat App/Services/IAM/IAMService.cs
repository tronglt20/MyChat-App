using Domain.Base.Utilities;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Base;
using MyChat_App.ViewModels.IAM.Requests;

namespace MyChat_App.Services.IAM
{
    public class IAMService
    {
        private readonly IUserRegistrationRepository _userRegistrationRepo;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public IAMService(IUserRegistrationRepository userRegistrationRepo
            , ITokenGenerator tokenGenerator
            , IUnitOfWork unitOfWork)
        {
            _userRegistrationRepo = userRegistrationRepo;
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task SignUpRequestAsync(SendSignupAccountRequest request)
        {
            var token = await _tokenGenerator.GenerateTokenAsync(48);

            var requestItem = new UserRegistration(request.Email, token);

            await _userRegistrationRepo.InsertAsync(requestItem);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Base;
using MyChat_App.Services.Base;
using MyChat_App.ViewModels.IAM.Requests;
using MyChat_App.ViewModels.IAM.Responses;
using Utilities.DTOs;
using Utilities.Interfaces;
using ErrorMessages = Domain.Entities.ErrorMessages;

namespace MyChat_App.Services
{
    public class IAMService : BaseService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserRegistrationRepository _userRegistrationRepo;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IAuthenticationService _authenticationService;

        public IAMService(IUserRegistrationRepository userRegistrationRepo
            , IAuthenticationService authenticationService
            , ITokenGenerator tokenGenerator
            , IUserRepository userRepo
            , IUnitOfWork unitOfWork
            , IServiceProvider serviceProvider
            ) : base(serviceProvider, unitOfWork)
        {
            _userRegistrationRepo = userRegistrationRepo;
            _tokenGenerator = tokenGenerator;
            _userRepo = userRepo;
            _authenticationService = authenticationService;
        }

        public CurrentUserInfoResponse GetCurrentUser()
        {
            if (User == null)
                return null;

            return new CurrentUserInfoResponse
            {
                Id = User.Id,
                Name = User.Name,
                Email = User.Email,
            };
        }

        public async Task SignUpRequestAsync(SendSignupAccountRequest request)
        {
            var token = await _tokenGenerator.GenerateTokenAsync(48);

            var requestItem = new UserRegistration(request.Email, token);

            await _userRegistrationRepo.InsertAsync(requestItem);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<LoginResult> SignInAsync(SignInRequest request)
        {
            var user = await ValidateOnSignInAsync(request);

            var result = await _authenticationService
                .GetLoginResultAsync(user.Id.ToString()
                , user.Name
                , user.Email);

            return result;
        }

        #region Helper

        private async Task<User> ValidateOnSignInAsync(SignInRequest request)
        {
            var user = await _userRepo.GetAsync(request.Email);

            if (user == null)
                throw new BadHttpRequestException(ErrorMessages.UserOrPasswordIncorrect);

            var validPassword = await _userRepo.CheckPasswordAsync(user.Id, request.Password);
            if (!validPassword)
                throw new BadHttpRequestException(ErrorMessages.UserOrPasswordIncorrect);


            return user;
        }

        #endregion
    }
}

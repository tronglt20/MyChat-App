using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Base;
using MyChat_App.ViewModels.IAM.Requests;
using System.Security.Claims;
using Utilities.DTOs;
using Utilities.Interfaces;
using ErrorMessages = Domain.Entities.ErrorMessages;

namespace MyChat_App.Services.IAM
{
    public class IAMService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserRegistrationRepository _userRegistrationRepo;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;

        public IAMService(IUserRegistrationRepository userRegistrationRepo
            , ITokenGenerator tokenGenerator
            , IUnitOfWork unitOfWork
            , IUserRepository userRepo
            , IAuthenticationService authenticationService)
        {
            _userRegistrationRepo = userRegistrationRepo;
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
            _userRepo = userRepo;
            _authenticationService = authenticationService;
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

        private async Task<User> ValidateOnSignInAsync(SignInRequest request)
        {
            var user = await _userRepo.GetAsync(request.Email);

            if (user == null)
                throw new BadHttpRequestException(ErrorMessages.UserOrPasswordIncorrect);

            var validPassword = await _userRepo.CheckPasswordAsync(user.Id, request.Password);
            if(!validPassword)
                throw new BadHttpRequestException(ErrorMessages.UserOrPasswordIncorrect);


            return user;
        }
    }
}


namespace ResponsiveWebApplicationRazor_Pages.Api
{
    public interface IAuthApi
    {
        Task<SessionModel> LoginAsync(LoginRequestModel loginRequestModel);
        Task<int> RegisterAsync(RegisterRequestModel registerRequestModel);
    }
}

namespace FW.Service.Users
{
    public enum UserLoginResult
    {
        Success = 1,
        NotExists = 2,
        WrongPassword = 3,
        NotActived = 4,
        Deleted = 5,
        NotRegister = 6
    }
}

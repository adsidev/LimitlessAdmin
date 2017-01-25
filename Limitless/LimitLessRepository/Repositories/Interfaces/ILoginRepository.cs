using LimitlessEntity.Results.Security;

namespace LimitLessRepository.Repositories.Interfaces
{
    interface ILoginRepository<T> : IRepository<T> where T : class
    {
        UserData GetUserData(LoginRequest model);
    }
}

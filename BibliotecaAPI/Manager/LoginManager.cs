using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using System;
using static BibliotecaAPI.Models.LoginResult;

namespace BibliotecaAPI.Manager
{
    public class LoginManager
    {
        private UsersRepository _usersRepository;
        private const int LOGIN_FAILED_LIMIT = 3;

        public LoginManager(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public LoginResult Authentication(string username, string password)
        {

            var user = _usersRepository.Login(username, password);
            if (user != null)
            {
                if (user.IsLockout)
                {
                    if (DateTime.Now <= user.LockoutDate?.AddMinutes(15))
                    {
                        return ErrorResult(UserBlockedException.USER_BLOCKED_EXCEPTION);

                    }
                    else
                    {
                        user.Unlock();
                    }
                }

                return SucessResult(user);
            }

            var userExistsForUsername = _usersRepository.GetbyUsernameBOOL(username);

            if (userExistsForUsername)
            {
                user = _usersRepository.GetbyUsername(username);

                user.IncrementFailedAttempts();

                if (user.FailedAttempts > LOGIN_FAILED_LIMIT)
                {
                    user.Lock();

                    return ErrorResult(UserBlockedException.USER_BLOCKED_EXCEPTION);
                }

                return ErrorResult(InvalidPasswordException.INVALID_PASSWORD_EXCEPTION);

            }
            return ErrorResult(AuthenticationException.INVALID_AUTHENTICATION_EXCEPTION);
        }
    }
}

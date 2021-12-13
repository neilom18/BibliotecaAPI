using BibliotecaAPI.Models;
using BibliotecaAPI.Models.Login;
using BibliotecaAPI.Repositories;
using System;
using static BibliotecaAPI.Models.Login.LoginResult;

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
                        return LoginResult.ErrorResult(UserBlockedException.USER_BLOCKED_EXCEPTION);

                    }
                    else
                    {
                        user.IsLockout = false;
                        user.LockoutDate = null;
                        user.FailedAttempts = 0;
                    }
                }

                return LoginResult.SucessResult(user);
            }

            var userExistsForUsername = _usersRepository.GetbyUsernameBOOL(username);

            if (userExistsForUsername)
            {
                user = _usersRepository.GetbyUsername(username);

                user.FailedAttempts++;

                if (user.FailedAttempts > LOGIN_FAILED_LIMIT)
                {
                    user.IsLockout = true;
                    user.LockoutDate = DateTime.Now;

                    return LoginResult.ErrorResult(UserBlockedException.USER_BLOCKED_EXCEPTION);
                }

                return LoginResult.ErrorResult(InvalidPasswordException.INVALID_PASSWORD_EXCEPTION);

            }
            return LoginResult.ErrorResult(AuthenticationException.INVALID_AUTHENTICATION_EXCEPTION);
        }
    }
}

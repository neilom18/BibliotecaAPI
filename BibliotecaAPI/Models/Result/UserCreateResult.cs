using System;

namespace BibliotecaAPI.Models
{
    public class UserCreateResult
    {
        public User? User { get; set; }
        public bool Errors { get; set; }
        public UserCreateException? CreateException { get; set; }

        public static UserCreateResult ErrorResult(UserCreateException exception)
        {
            return new UserCreateResult
            {
                User = null,
                Errors = true,
                CreateException = exception,
            };
        }

        public static UserCreateResult SucessResult(User user)
        {
            return new UserCreateResult
            {
                User = user,
                Errors = false,
                CreateException = null,
            };
        }

        public class UserCreateException : Exception
        {
            private const string MESSAGE = "Não foi possivel cadastrar esse usuário.";
            public static UserCreateException USER_CREATE_EXCEPTION = new UserCreateException(MESSAGE);

            public UserCreateException(string message) : base(message) { }
            public UserCreateException(Exception inner) : base(MESSAGE, inner) { }
            public UserCreateException(string message, Exception inner) : base(message, inner) { }
        }

        public class UserAddressExcpetion : UserCreateException
        {
            private const string MESSAGE = "Não foi possivel encontrar esse endereço, informe o endereço completo.";
            public static UserAddressExcpetion USER_ADDRESS_EXCEPTION = new UserAddressExcpetion(MESSAGE);

            public UserAddressExcpetion(string message) : base(message) { }
            public UserAddressExcpetion(Exception inner) : base(inner) { }
            public UserAddressExcpetion(string message, Exception inner) : base(message, inner) { }
        }

        public class UsernameUsedExcpetion : UserCreateException
        {
            private const string MESSAGE = "Esse nome de usuário já está sendo utilizado!";
            public static UsernameUsedExcpetion USERNAME_USED_EXCEPTION = new UsernameUsedExcpetion(MESSAGE);

            public UsernameUsedExcpetion(string message) : base(message) { }
            public UsernameUsedExcpetion(Exception inner) : base(inner) { }
            public UsernameUsedExcpetion(string message, Exception inner) : base(message, inner) { }
        }
    }
}

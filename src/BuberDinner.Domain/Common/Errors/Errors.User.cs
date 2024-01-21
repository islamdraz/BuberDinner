using ErrorOr;

namespace BuberDinner.Domain.Common.Errors;


public static partial class Errors
{
    public static class User
    {
        public static Error DublicateEmail => Error.Conflict(code: "User.DuplicateEmail", description: "User is used before");
    }
}
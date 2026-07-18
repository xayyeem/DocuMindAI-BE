using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Common
{
    public class Result<T> : Result
    {
        private readonly T? _value;

        private Result(T value)
            : base(true, Error.None)
        {
            _value = value;
        }

        private Result(Error error)
            : base(false, error)
        {
            _value = default;
        }

        public T Value =>
            IsSuccess
                ? _value!
                : throw new InvalidOperationException(
                    "Cannot access value of a failed result.");

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static new Result<T> Failure(Error error)
        {
            return new Result<T>(error);
        }
    }
}

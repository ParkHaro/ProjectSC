using System;

namespace Sc.Foundation
{
    /// <summary>
    /// 성공/실패를 명시적으로 전달하는 결과 래퍼 구조체
    /// </summary>
    /// <typeparam name="T">성공 시 반환할 값의 타입</typeparam>
    public readonly struct Result<T>
    {
        /// <summary>성공 여부</summary>
        public bool IsSuccess { get; }

        /// <summary>실패 여부</summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>성공 시 값</summary>
        public T Value { get; }

        /// <summary>실패 시 에러 코드</summary>
        public ErrorCode Error { get; }

        /// <summary>에러 메시지</summary>
        public string Message { get; }

        private Result(bool success, T value, ErrorCode error, string message)
        {
            IsSuccess = success;
            Value = value;
            Error = error;
            Message = message;
        }

        /// <summary>
        /// 성공 Result 생성
        /// </summary>
        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, ErrorCode.None, null);
        }

        /// <summary>
        /// 실패 Result 생성 (에러 코드)
        /// </summary>
        public static Result<T> Failure(ErrorCode error)
        {
            return new Result<T>(false, default, error, ErrorMessages.GetMessage(error));
        }

        /// <summary>
        /// 실패 Result 생성 (에러 코드 + 커스텀 메시지)
        /// </summary>
        public static Result<T> Failure(ErrorCode error, string customMessage)
        {
            return new Result<T>(false, default, error, customMessage);
        }

        /// <summary>
        /// 성공 시 액션 실행
        /// </summary>
        public Result<T> OnSuccess(Action<T> action)
        {
            if (IsSuccess)
            {
                action?.Invoke(Value);
            }

            return this;
        }

        /// <summary>
        /// 실패 시 액션 실행
        /// </summary>
        public Result<T> OnFailure(Action<ErrorCode, string> action)
        {
            if (IsFailure)
            {
                action?.Invoke(Error, Message);
            }

            return this;
        }

        /// <summary>
        /// 값 타입 변환
        /// </summary>
        public Result<TNew> Map<TNew>(Func<T, TNew> mapper)
        {
            if (IsFailure)
            {
                return Result<TNew>.Failure(Error, Message);
            }

            return Result<TNew>.Success(mapper(Value));
        }

        /// <summary>
        /// 암시적 변환 (성공 케이스 간소화)
        /// </summary>
        public static implicit operator Result<T>(T value)
        {
            return Success(value);
        }
    }
}

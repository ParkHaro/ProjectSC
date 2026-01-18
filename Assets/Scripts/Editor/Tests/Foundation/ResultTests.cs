using NUnit.Framework;
using Sc.Foundation;

namespace Sc.Editor.Tests.Foundation
{
    /// <summary>
    /// Result<T> 패턴 단위 테스트
    /// </summary>
    [TestFixture]
    public class ResultTests
    {
        #region Success Tests

        [Test]
        public void Success_CreatesSuccessResult_WithValue()
        {
            var result = Result<int>.Success(42);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.IsFailure, Is.False);
            Assert.That(result.Value, Is.EqualTo(42));
            Assert.That(result.Error, Is.EqualTo(ErrorCode.None));
        }

        [Test]
        public void Success_WorksWithReferenceTypes()
        {
            var result = Result<string>.Success("테스트 문자열");

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.EqualTo("테스트 문자열"));
        }

        [Test]
        public void ImplicitConversion_CreatesSuccessResult()
        {
            Result<string> result = "암시적 변환";

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.EqualTo("암시적 변환"));
        }

        #endregion

        #region Failure Tests

        [Test]
        public void Failure_CreatesFailureResult_WithErrorCode()
        {
            var result = Result<int>.Failure(ErrorCode.InsufficientGold);

            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Error, Is.EqualTo(ErrorCode.InsufficientGold));
            Assert.That(result.Value, Is.EqualTo(default(int)));
        }

        [Test]
        public void Failure_WithCustomMessage_UsesCustomMessage()
        {
            var result = Result<string>.Failure(ErrorCode.NetworkTimeout, "커스텀 메시지");

            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(ErrorCode.NetworkTimeout));
            Assert.That(result.Message, Is.EqualTo("커스텀 메시지"));
        }

        #endregion

        #region OnSuccess Tests

        [Test]
        public void OnSuccess_ExecutesAction_WhenSuccess()
        {
            int capturedValue = 0;

            Result<int>.Success(100)
                .OnSuccess(v => capturedValue = v);

            Assert.That(capturedValue, Is.EqualTo(100));
        }

        [Test]
        public void OnSuccess_DoesNotExecuteAction_WhenFailure()
        {
            int capturedValue = 0;

            Result<int>.Failure(ErrorCode.SaveFailed)
                .OnSuccess(v => capturedValue = v);

            Assert.That(capturedValue, Is.EqualTo(0));
        }

        [Test]
        public void OnSuccess_ReturnsSameResult_ForChaining()
        {
            var original = Result<int>.Success(50);
            var returned = original.OnSuccess(_ => { });

            Assert.That(returned.IsSuccess, Is.EqualTo(original.IsSuccess));
            Assert.That(returned.Value, Is.EqualTo(original.Value));
        }

        #endregion

        #region OnFailure Tests

        [Test]
        public void OnFailure_ExecutesAction_WhenFailure()
        {
            ErrorCode capturedError = ErrorCode.None;
            string capturedMessage = null;

            Result<int>.Failure(ErrorCode.ParseFailed)
                .OnFailure((code, msg) =>
                {
                    capturedError = code;
                    capturedMessage = msg;
                });

            Assert.That(capturedError, Is.EqualTo(ErrorCode.ParseFailed));
            Assert.That(capturedMessage, Is.Not.Null);
        }

        [Test]
        public void OnFailure_DoesNotExecuteAction_WhenSuccess()
        {
            bool actionCalled = false;

            Result<int>.Success(10)
                .OnFailure((_, _) => actionCalled = true);

            Assert.That(actionCalled, Is.False);
        }

        #endregion

        #region Map Tests

        [Test]
        public void Map_TransformsValue_WhenSuccess()
        {
            var result = Result<int>.Success(10)
                .Map(v => v * 2);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.EqualTo(20));
        }

        [Test]
        public void Map_PropagatesError_WhenFailure()
        {
            var result = Result<int>.Failure(ErrorCode.LoadFailed)
                .Map(v => v.ToString());

            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(ErrorCode.LoadFailed));
        }

        [Test]
        public void Map_ChangesType_Successfully()
        {
            var result = Result<int>.Success(42)
                .Map(v => $"값: {v}");

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.EqualTo("값: 42"));
        }

        #endregion

        #region Chaining Tests

        [Test]
        public void Chaining_WorksCorrectly_ForSuccessPath()
        {
            int successValue = 0;
            bool failureCalled = false;

            Result<int>.Success(25)
                .OnSuccess(v => successValue = v)
                .OnFailure((_, _) => failureCalled = true);

            Assert.That(successValue, Is.EqualTo(25));
            Assert.That(failureCalled, Is.False);
        }

        [Test]
        public void Chaining_WorksCorrectly_ForFailurePath()
        {
            int successValue = 0;
            ErrorCode capturedError = ErrorCode.None;

            Result<int>.Failure(ErrorCode.InventoryFull)
                .OnSuccess(v => successValue = v)
                .OnFailure((code, _) => capturedError = code);

            Assert.That(successValue, Is.EqualTo(0));
            Assert.That(capturedError, Is.EqualTo(ErrorCode.InventoryFull));
        }

        #endregion
    }
}

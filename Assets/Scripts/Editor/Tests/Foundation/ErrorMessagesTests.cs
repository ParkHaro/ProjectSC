using NUnit.Framework;
using Sc.Foundation;

namespace Sc.Editor.Tests.Foundation
{
    /// <summary>
    /// ErrorMessages 단위 테스트
    /// </summary>
    [TestFixture]
    public class ErrorMessagesTests
    {
        [TearDown]
        public void TearDown()
        {
            // 테스트 후 LocalizeFunc 초기화
            ErrorMessages.LocalizeFunc = null;
        }

        #region GetKey Tests

        [Test]
        public void GetKey_ReturnsCorrectKey_ForGameErrors()
        {
            Assert.That(ErrorMessages.GetKey(ErrorCode.InsufficientGold),
                Is.EqualTo("error.game.insufficient_gold"));
            Assert.That(ErrorMessages.GetKey(ErrorCode.InsufficientGem),
                Is.EqualTo("error.game.insufficient_gem"));
            Assert.That(ErrorMessages.GetKey(ErrorCode.InsufficientStamina),
                Is.EqualTo("error.game.insufficient_stamina"));
        }

        [Test]
        public void GetKey_ReturnsCorrectKey_ForNetworkErrors()
        {
            Assert.That(ErrorMessages.GetKey(ErrorCode.NetworkDisconnected),
                Is.EqualTo("error.network.disconnected"));
            Assert.That(ErrorMessages.GetKey(ErrorCode.NetworkTimeout),
                Is.EqualTo("error.network.timeout"));
            Assert.That(ErrorMessages.GetKey(ErrorCode.ServerError),
                Is.EqualTo("error.network.server_error"));
        }

        [Test]
        public void GetKey_ReturnsCorrectKey_ForDataErrors()
        {
            Assert.That(ErrorMessages.GetKey(ErrorCode.SaveFailed),
                Is.EqualTo("error.data.save_failed"));
            Assert.That(ErrorMessages.GetKey(ErrorCode.LoadFailed),
                Is.EqualTo("error.data.load_failed"));
            Assert.That(ErrorMessages.GetKey(ErrorCode.ParseFailed),
                Is.EqualTo("error.data.parse_failed"));
        }

        [Test]
        public void GetKey_ReturnsEmptyString_ForNone()
        {
            Assert.That(ErrorMessages.GetKey(ErrorCode.None), Is.Empty);
        }

        [Test]
        public void GetKey_ReturnsEmptyString_ForUnmappedCode()
        {
            // 존재하지 않는 ErrorCode (캐스팅으로 생성)
            var unmappedCode = (ErrorCode)99999;
            Assert.That(ErrorMessages.GetKey(unmappedCode), Is.Empty);
        }

        #endregion

        #region GetMessage Tests - Without LocalizeFunc

        [Test]
        public void GetMessage_ReturnsKey_WhenLocalizeFuncIsNull()
        {
            ErrorMessages.LocalizeFunc = null;

            var message = ErrorMessages.GetMessage(ErrorCode.InsufficientGold);

            Assert.That(message, Is.EqualTo("error.game.insufficient_gold"));
        }

        [Test]
        public void GetMessage_ReturnsEmptyString_ForNone()
        {
            var message = ErrorMessages.GetMessage(ErrorCode.None);

            Assert.That(message, Is.Empty);
        }

        #endregion

        #region GetMessage Tests - With LocalizeFunc

        [Test]
        public void GetMessage_UsesLocalizeFunc_WhenSet()
        {
            ErrorMessages.LocalizeFunc = key => $"[Localized] {key}";

            var message = ErrorMessages.GetMessage(ErrorCode.NetworkTimeout);

            Assert.That(message, Is.EqualTo("[Localized] error.network.timeout"));
        }

        [Test]
        public void GetMessage_PassesCorrectKey_ToLocalizeFunc()
        {
            string receivedKey = null;
            ErrorMessages.LocalizeFunc = key =>
            {
                receivedKey = key;
                return key;
            };

            ErrorMessages.GetMessage(ErrorCode.SaveFailed);

            Assert.That(receivedKey, Is.EqualTo("error.data.save_failed"));
        }

        [Test]
        public void GetMessage_FallsBackToKey_WhenLocalizeFuncReturnsNull()
        {
            // LocalizeFunc가 null 반환 시 key로 폴백 (null 병합 연산자)
            ErrorMessages.LocalizeFunc = _ => null;

            var message = ErrorMessages.GetMessage(ErrorCode.InsufficientGold);

            Assert.That(message, Is.EqualTo("error.game.insufficient_gold"));
        }

        #endregion

        #region ErrorCode Coverage Tests

        [Test]
        public void AllSystemErrors_HaveKeys()
        {
            Assert.That(ErrorMessages.GetKey(ErrorCode.SystemInitFailed), Is.Not.Empty);
            Assert.That(ErrorMessages.GetKey(ErrorCode.ConfigLoadFailed), Is.Not.Empty);
        }

        [Test]
        public void AllAuthErrors_HaveKeys()
        {
            Assert.That(ErrorMessages.GetKey(ErrorCode.LoginFailed), Is.Not.Empty);
            Assert.That(ErrorMessages.GetKey(ErrorCode.SessionExpired), Is.Not.Empty);
            Assert.That(ErrorMessages.GetKey(ErrorCode.InvalidToken), Is.Not.Empty);
        }

        [Test]
        public void AllUIErrors_HaveKeys()
        {
            Assert.That(ErrorMessages.GetKey(ErrorCode.ScreenLoadFailed), Is.Not.Empty);
            Assert.That(ErrorMessages.GetKey(ErrorCode.PopupLoadFailed), Is.Not.Empty);
        }

        #endregion
    }
}

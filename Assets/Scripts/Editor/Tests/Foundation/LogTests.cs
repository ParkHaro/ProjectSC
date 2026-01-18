using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Sc.Foundation;

namespace Sc.Editor.Tests.Foundation
{
    /// <summary>
    /// Log 시스템 단위 테스트
    /// </summary>
    [TestFixture]
    public class LogTests
    {
        private TestLogOutput _testOutput;

        [SetUp]
        public void SetUp()
        {
            // Initialize 먼저 호출하여 내부 상태 초기화
            // (WriteInternal에서 자동 Initialize 시 _outputs.Clear() 방지)
            Log.Initialize();
            _testOutput = new TestLogOutput();
            Log.AddOutput(_testOutput);
        }

        [TearDown]
        public void TearDown()
        {
            Log.RemoveOutput(_testOutput);
            _testOutput = null;
        }

        #region Log Level Tests

        [Test]
        public void Debug_WritesMessage_WithDebugLevel()
        {
            Log.Debug("테스트 메시지", LogCategory.System);

            Assert.That(_testOutput.LastLevel, Is.EqualTo(LogLevel.Debug));
            Assert.That(_testOutput.LastMessage, Is.EqualTo("테스트 메시지"));
        }

        [Test]
        public void Info_WritesMessage_WithInfoLevel()
        {
            Log.Info("정보 메시지", LogCategory.System);

            Assert.That(_testOutput.LastLevel, Is.EqualTo(LogLevel.Info));
            Assert.That(_testOutput.LastMessage, Is.EqualTo("정보 메시지"));
        }

        [Test]
        public void Warning_WritesMessage_WithWarningLevel()
        {
            Log.Warning("경고 메시지", LogCategory.System);

            Assert.That(_testOutput.LastLevel, Is.EqualTo(LogLevel.Warning));
            Assert.That(_testOutput.LastMessage, Is.EqualTo("경고 메시지"));
        }

        [Test]
        public void Error_WritesMessage_WithErrorLevel()
        {
            // Unity가 Debug.LogError를 처리하므로 예상 로그 명시
            LogAssert.Expect(LogType.Error, "[System] 에러 메시지");

            Log.Error("에러 메시지", LogCategory.System);

            Assert.That(_testOutput.LastLevel, Is.EqualTo(LogLevel.Error));
            Assert.That(_testOutput.LastMessage, Is.EqualTo("에러 메시지"));
        }

        #endregion

        #region Log Category Tests

        [Test]
        public void Log_WritesCorrectCategory_ForEachCategory()
        {
            var categories = new[]
            {
                LogCategory.System,
                LogCategory.Network,
                LogCategory.Data,
                LogCategory.UI,
                LogCategory.Battle,
                LogCategory.Gacha
            };

            foreach (var category in categories)
            {
                Log.Info($"{category} 카테고리 메시지", category);
                Assert.That(_testOutput.LastCategory, Is.EqualTo(category),
                    $"카테고리 {category} 검증 실패");
            }
        }

        #endregion

        #region Output Management Tests

        [Test]
        public void AddOutput_AddsNewOutput_Successfully()
        {
            var anotherOutput = new TestLogOutput();
            Log.AddOutput(anotherOutput);

            Log.Info("테스트");

            Assert.That(anotherOutput.LastMessage, Is.EqualTo("테스트"));

            Log.RemoveOutput(anotherOutput);
        }

        [Test]
        public void RemoveOutput_RemovesOutput_Successfully()
        {
            var tempOutput = new TestLogOutput();
            Log.AddOutput(tempOutput);
            Log.RemoveOutput(tempOutput);

            Log.Info("제거 후 메시지");

            Assert.That(tempOutput.LastMessage, Is.Null);
        }

        [Test]
        public void AddOutput_IgnoresNull()
        {
            Assert.DoesNotThrow(() => Log.AddOutput(null));
        }

        [Test]
        public void AddOutput_IgnoresDuplicate()
        {
            int callCount = 0;
            var countingOutput = new CountingLogOutput(() => callCount++);

            Log.AddOutput(countingOutput);
            Log.AddOutput(countingOutput); // 중복 추가 시도

            Log.Info("테스트");

            Assert.That(callCount, Is.EqualTo(1), "중복 Output이 추가되면 안 됨");

            Log.RemoveOutput(countingOutput);
        }

        #endregion

        #region IsEnabled Tests

        [Test]
        public void IsEnabled_ReturnsTrue_ForValidLevelWithoutConfig()
        {
            Assert.That(Log.IsEnabled(LogLevel.Debug), Is.True);
            Assert.That(Log.IsEnabled(LogLevel.Info), Is.True);
            Assert.That(Log.IsEnabled(LogLevel.Warning), Is.True);
            Assert.That(Log.IsEnabled(LogLevel.Error), Is.True);
        }

        #endregion

        #region Test Helpers

        private class TestLogOutput : ILogOutput
        {
            public LogLevel LastLevel { get; private set; }
            public LogCategory LastCategory { get; private set; }
            public string LastMessage { get; private set; }

            public void Write(LogLevel level, LogCategory category, string message)
            {
                LastLevel = level;
                LastCategory = category;
                LastMessage = message;
            }
        }

        private class CountingLogOutput : ILogOutput
        {
            private readonly System.Action _onWrite;

            public CountingLogOutput(System.Action onWrite)
            {
                _onWrite = onWrite;
            }

            public void Write(LogLevel level, LogCategory category, string message)
            {
                _onWrite?.Invoke();
            }
        }

        #endregion
    }
}

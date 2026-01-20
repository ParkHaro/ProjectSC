using NUnit.Framework;
using Sc.Data;

namespace Sc.Editor.Tests.Data
{
    /// <summary>
    /// EventMissionProgress 단위 테스트.
    /// 미션 진행률 계산 검증.
    /// </summary>
    [TestFixture]
    public class EventMissionProgressTests
    {
        #region CreateDefault Tests

        [Test]
        public void CreateDefault_InitializesWithMissionId()
        {
            var progress = EventMissionProgress.CreateDefault("mission_001");

            Assert.That(progress.MissionId, Is.EqualTo("mission_001"));
        }

        [Test]
        public void CreateDefault_InitializesCurrentCountZero()
        {
            var progress = EventMissionProgress.CreateDefault("mission_001");

            Assert.That(progress.CurrentCount, Is.EqualTo(0));
        }

        [Test]
        public void CreateDefault_InitializesIsCompletedFalse()
        {
            var progress = EventMissionProgress.CreateDefault("mission_001");

            Assert.That(progress.IsCompleted, Is.False);
        }

        [Test]
        public void CreateDefault_InitializesIsClaimedFalse()
        {
            var progress = EventMissionProgress.CreateDefault("mission_001");

            Assert.That(progress.IsClaimed, Is.False);
        }

        #endregion

        #region GetProgressRatio Tests

        [Test]
        public void GetProgressRatio_ReturnsZero_WhenRequiredCountZero()
        {
            var progress = new EventMissionProgress
            {
                MissionId = "mission_001",
                CurrentCount = 5
            };

            var ratio = progress.GetProgressRatio(0);

            Assert.That(ratio, Is.EqualTo(0f));
        }

        [Test]
        public void GetProgressRatio_ReturnsZero_WhenRequiredCountNegative()
        {
            var progress = new EventMissionProgress
            {
                MissionId = "mission_001",
                CurrentCount = 5
            };

            var ratio = progress.GetProgressRatio(-1);

            Assert.That(ratio, Is.EqualTo(0f));
        }

        [Test]
        public void GetProgressRatio_ReturnsCorrectRatio()
        {
            var progress = new EventMissionProgress
            {
                MissionId = "mission_001",
                CurrentCount = 5
            };

            var ratio = progress.GetProgressRatio(10);

            Assert.That(ratio, Is.EqualTo(0.5f));
        }

        [Test]
        public void GetProgressRatio_ReturnsOne_WhenExactlyMet()
        {
            var progress = new EventMissionProgress
            {
                MissionId = "mission_001",
                CurrentCount = 10
            };

            var ratio = progress.GetProgressRatio(10);

            Assert.That(ratio, Is.EqualTo(1f));
        }

        [Test]
        public void GetProgressRatio_CapsAtOne_WhenExceeded()
        {
            var progress = new EventMissionProgress
            {
                MissionId = "mission_001",
                CurrentCount = 15
            };

            var ratio = progress.GetProgressRatio(10);

            Assert.That(ratio, Is.EqualTo(1f));
        }

        [Test]
        public void GetProgressRatio_ReturnsZero_WhenNoProgress()
        {
            var progress = new EventMissionProgress
            {
                MissionId = "mission_001",
                CurrentCount = 0
            };

            var ratio = progress.GetProgressRatio(10);

            Assert.That(ratio, Is.EqualTo(0f));
        }

        #endregion
    }
}

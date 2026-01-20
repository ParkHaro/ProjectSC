using System.Collections.Generic;
using NUnit.Framework;
using Sc.Data;

namespace Sc.Editor.Tests.Data
{
    /// <summary>
    /// LiveEventProgress 단위 테스트.
    /// 미션 진행 상태 관리 검증.
    /// </summary>
    [TestFixture]
    public class LiveEventProgressTests
    {
        private LiveEventProgress _progress;

        [SetUp]
        public void SetUp()
        {
            _progress = LiveEventProgress.CreateDefault("event_001");
        }

        #region CreateDefault Tests

        [Test]
        public void CreateDefault_InitializesWithEventId()
        {
            var progress = LiveEventProgress.CreateDefault("test_event");

            Assert.That(progress.EventId, Is.EqualTo("test_event"));
        }

        [Test]
        public void CreateDefault_InitializesEmptyMissionProgresses()
        {
            var progress = LiveEventProgress.CreateDefault("test_event");

            Assert.That(progress.MissionProgresses, Is.Not.Null);
            Assert.That(progress.MissionProgresses.Count, Is.EqualTo(0));
        }

        [Test]
        public void CreateDefault_InitializesHasVisitedFalse()
        {
            var progress = LiveEventProgress.CreateDefault("test_event");

            Assert.That(progress.HasVisited, Is.False);
        }

        [Test]
        public void CreateDefault_InitializesFirstVisitTimeZero()
        {
            var progress = LiveEventProgress.CreateDefault("test_event");

            Assert.That(progress.FirstVisitTime, Is.EqualTo(0));
        }

        #endregion

        #region GetMissionProgress Tests

        [Test]
        public void GetMissionProgress_ReturnsNull_WhenMissionNotFound()
        {
            var result = _progress.GetMissionProgress("nonexistent_mission");

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetMissionProgress_ReturnsNull_WhenMissionProgressesIsNull()
        {
            _progress.MissionProgresses = null;

            var result = _progress.GetMissionProgress("mission_001");

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetMissionProgress_ReturnsMission_WhenExists()
        {
            _progress.MissionProgresses = new List<EventMissionProgress>
            {
                new EventMissionProgress
                {
                    MissionId = "mission_001",
                    CurrentCount = 5,
                    IsCompleted = false,
                    IsClaimed = false
                }
            };

            var result = _progress.GetMissionProgress("mission_001");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.MissionId, Is.EqualTo("mission_001"));
            Assert.That(result.Value.CurrentCount, Is.EqualTo(5));
        }

        #endregion

        #region UpdateMissionProgress Tests

        [Test]
        public void UpdateMissionProgress_AddsMission_WhenNotExists()
        {
            var missionProgress = new EventMissionProgress
            {
                MissionId = "mission_001",
                CurrentCount = 3,
                IsCompleted = false,
                IsClaimed = false
            };

            _progress.UpdateMissionProgress(missionProgress);

            Assert.That(_progress.MissionProgresses.Count, Is.EqualTo(1));
            Assert.That(_progress.MissionProgresses[0].MissionId, Is.EqualTo("mission_001"));
        }

        [Test]
        public void UpdateMissionProgress_UpdatesMission_WhenExists()
        {
            _progress.MissionProgresses = new List<EventMissionProgress>
            {
                new EventMissionProgress
                {
                    MissionId = "mission_001",
                    CurrentCount = 3,
                    IsCompleted = false,
                    IsClaimed = false
                }
            };

            var updatedProgress = new EventMissionProgress
            {
                MissionId = "mission_001",
                CurrentCount = 10,
                IsCompleted = true,
                IsClaimed = false
            };

            _progress.UpdateMissionProgress(updatedProgress);

            Assert.That(_progress.MissionProgresses.Count, Is.EqualTo(1));
            Assert.That(_progress.MissionProgresses[0].CurrentCount, Is.EqualTo(10));
            Assert.That(_progress.MissionProgresses[0].IsCompleted, Is.True);
        }

        [Test]
        public void UpdateMissionProgress_CreatesListIfNull()
        {
            _progress.MissionProgresses = null;

            var missionProgress = new EventMissionProgress
            {
                MissionId = "mission_001",
                CurrentCount = 1
            };

            _progress.UpdateMissionProgress(missionProgress);

            Assert.That(_progress.MissionProgresses, Is.Not.Null);
            Assert.That(_progress.MissionProgresses.Count, Is.EqualTo(1));
        }

        #endregion

        #region GetCompletedMissionCount Tests

        [Test]
        public void GetCompletedMissionCount_ReturnsZero_WhenNoMissions()
        {
            var count = _progress.GetCompletedMissionCount();

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void GetCompletedMissionCount_ReturnsZero_WhenMissionProgressesIsNull()
        {
            _progress.MissionProgresses = null;

            var count = _progress.GetCompletedMissionCount();

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void GetCompletedMissionCount_ReturnsCorrectCount()
        {
            _progress.MissionProgresses = new List<EventMissionProgress>
            {
                new EventMissionProgress { MissionId = "m1", IsCompleted = true },
                new EventMissionProgress { MissionId = "m2", IsCompleted = false },
                new EventMissionProgress { MissionId = "m3", IsCompleted = true },
                new EventMissionProgress { MissionId = "m4", IsCompleted = true }
            };

            var count = _progress.GetCompletedMissionCount();

            Assert.That(count, Is.EqualTo(3));
        }

        #endregion

        #region GetClaimableMissionCount Tests

        [Test]
        public void GetClaimableMissionCount_ReturnsZero_WhenNoMissions()
        {
            var count = _progress.GetClaimableMissionCount();

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void GetClaimableMissionCount_ReturnsZero_WhenMissionProgressesIsNull()
        {
            _progress.MissionProgresses = null;

            var count = _progress.GetClaimableMissionCount();

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void GetClaimableMissionCount_ReturnsCorrectCount()
        {
            _progress.MissionProgresses = new List<EventMissionProgress>
            {
                new EventMissionProgress { MissionId = "m1", IsCompleted = true, IsClaimed = false }, // claimable
                new EventMissionProgress { MissionId = "m2", IsCompleted = true, IsClaimed = true },  // already claimed
                new EventMissionProgress { MissionId = "m3", IsCompleted = false, IsClaimed = false }, // not completed
                new EventMissionProgress { MissionId = "m4", IsCompleted = true, IsClaimed = false }  // claimable
            };

            var count = _progress.GetClaimableMissionCount();

            Assert.That(count, Is.EqualTo(2));
        }

        #endregion

        #region IsAllMissionsCompleted Tests

        [Test]
        public void IsAllMissionsCompleted_ReturnsTrue_WhenAllCompleted()
        {
            _progress.MissionProgresses = new List<EventMissionProgress>
            {
                new EventMissionProgress { MissionId = "m1", IsCompleted = true },
                new EventMissionProgress { MissionId = "m2", IsCompleted = true },
                new EventMissionProgress { MissionId = "m3", IsCompleted = true }
            };

            var result = _progress.IsAllMissionsCompleted(3);

            Assert.That(result, Is.True);
        }

        [Test]
        public void IsAllMissionsCompleted_ReturnsFalse_WhenNotAllCompleted()
        {
            _progress.MissionProgresses = new List<EventMissionProgress>
            {
                new EventMissionProgress { MissionId = "m1", IsCompleted = true },
                new EventMissionProgress { MissionId = "m2", IsCompleted = false }
            };

            var result = _progress.IsAllMissionsCompleted(3);

            Assert.That(result, Is.False);
        }

        #endregion
    }
}

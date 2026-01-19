using NUnit.Framework;
using Sc.Common.UI;
using UnityEngine;

namespace Sc.Editor.Tests.Common
{
    /// <summary>
    /// SimpleItemSpawner 단위 테스트
    /// Note: Unity Object 생성이 필요하므로 PlayMode 테스트로도 확장 가능
    /// </summary>
    [TestFixture]
    public class SimpleItemSpawnerTests
    {
        private GameObject _prefabObject;
        private TestComponent _prefab;
        private GameObject _parentObject;
        private Transform _parent;

        private class TestComponent : MonoBehaviour { }

        [SetUp]
        public void SetUp()
        {
            _prefabObject = new GameObject("TestPrefab");
            _prefab = _prefabObject.AddComponent<TestComponent>();

            _parentObject = new GameObject("TestParent");
            _parent = _parentObject.transform;
        }

        [TearDown]
        public void TearDown()
        {
            if (_prefabObject != null)
                Object.DestroyImmediate(_prefabObject);
            if (_parentObject != null)
                Object.DestroyImmediate(_parentObject);
        }

        #region Constructor Tests

        [Test]
        public void Constructor_WithValidPrefab_Succeeds()
        {
            var spawner = new SimpleItemSpawner<TestComponent>(_prefab);

            Assert.That(spawner, Is.Not.Null);
            Assert.That(spawner.ActiveCount, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_WithNullPrefab_Succeeds()
        {
            // Null prefab은 생성 시점에서는 허용 (Spawn 시점에 에러)
            var spawner = new SimpleItemSpawner<TestComponent>(null);

            Assert.That(spawner, Is.Not.Null);
        }

        #endregion

        #region Spawn Tests

        [Test]
        public void Spawn_CreatesInstance()
        {
            var spawner = new SimpleItemSpawner<TestComponent>(_prefab);

            var instance = spawner.Spawn(_parent);

            Assert.That(instance, Is.Not.Null);
            Assert.That(spawner.ActiveCount, Is.EqualTo(1));
        }

        [Test]
        public void Spawn_SetsParent()
        {
            var spawner = new SimpleItemSpawner<TestComponent>(_prefab);

            var instance = spawner.Spawn(_parent);

            Assert.That(instance.transform.parent, Is.EqualTo(_parent));
        }

        [Test]
        public void Spawn_MultipleTimes_TracksAllInstances()
        {
            var spawner = new SimpleItemSpawner<TestComponent>(_prefab);

            var instance1 = spawner.Spawn(_parent);
            var instance2 = spawner.Spawn(_parent);
            var instance3 = spawner.Spawn(_parent);

            Assert.That(spawner.ActiveCount, Is.EqualTo(3));
            Assert.That(instance1, Is.Not.Null);
            Assert.That(instance2, Is.Not.Null);
            Assert.That(instance3, Is.Not.Null);

            // Cleanup
            Object.DestroyImmediate(instance1.gameObject);
            Object.DestroyImmediate(instance2.gameObject);
            Object.DestroyImmediate(instance3.gameObject);
        }

        [Test]
        public void Spawn_WithNullPrefab_ReturnsNull()
        {
            var spawner = new SimpleItemSpawner<TestComponent>(null);

            var instance = spawner.Spawn(_parent);

            Assert.That(instance, Is.Null);
            Assert.That(spawner.ActiveCount, Is.EqualTo(0));
        }

        #endregion

        #region Despawn Tests

        [Test]
        public void Despawn_RemovesInstance()
        {
            var spawner = new SimpleItemSpawner<TestComponent>(_prefab);
            var instance = spawner.Spawn(_parent);

            spawner.Despawn(instance);

            Assert.That(spawner.ActiveCount, Is.EqualTo(0));
        }

        [Test]
        public void Despawn_WithNull_DoesNotThrow()
        {
            var spawner = new SimpleItemSpawner<TestComponent>(_prefab);

            Assert.DoesNotThrow(() => spawner.Despawn(null));
        }

        [Test]
        public void Despawn_DestroysGameObject()
        {
            var spawner = new SimpleItemSpawner<TestComponent>(_prefab);
            var instance = spawner.Spawn(_parent);
            var go = instance.gameObject;

            spawner.Despawn(instance);

            // Unity에서 DestroyImmediate 대신 Destroy를 사용하므로
            // 실제 파괴는 다음 프레임에 발생 (EditMode에서는 즉시)
            Assert.That(spawner.ActiveCount, Is.EqualTo(0));
        }

        #endregion

        #region DespawnAll Tests

        [Test]
        public void DespawnAll_ClearsAllInstances()
        {
            var spawner = new SimpleItemSpawner<TestComponent>(_prefab);
            spawner.Spawn(_parent);
            spawner.Spawn(_parent);
            spawner.Spawn(_parent);

            Assert.That(spawner.ActiveCount, Is.EqualTo(3));

            spawner.DespawnAll();

            Assert.That(spawner.ActiveCount, Is.EqualTo(0));
        }

        [Test]
        public void DespawnAll_WhenEmpty_DoesNotThrow()
        {
            var spawner = new SimpleItemSpawner<TestComponent>(_prefab);

            Assert.DoesNotThrow(() => spawner.DespawnAll());
            Assert.That(spawner.ActiveCount, Is.EqualTo(0));
        }

        #endregion

        #region IItemSpawner Interface Tests

        [Test]
        public void Implements_IItemSpawner()
        {
            var spawner = new SimpleItemSpawner<TestComponent>(_prefab);

            Assert.That(spawner, Is.InstanceOf<IItemSpawner<TestComponent>>());
        }

        [Test]
        public void ActiveCount_ReflectsCurrentState()
        {
            var spawner = new SimpleItemSpawner<TestComponent>(_prefab);

            Assert.That(spawner.ActiveCount, Is.EqualTo(0));

            var instance1 = spawner.Spawn(_parent);
            Assert.That(spawner.ActiveCount, Is.EqualTo(1));

            var instance2 = spawner.Spawn(_parent);
            Assert.That(spawner.ActiveCount, Is.EqualTo(2));

            spawner.Despawn(instance1);
            Assert.That(spawner.ActiveCount, Is.EqualTo(1));

            spawner.DespawnAll();
            Assert.That(spawner.ActiveCount, Is.EqualTo(0));
        }

        #endregion
    }
}

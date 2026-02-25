using System.Collections.Generic;
using Internal.Scripts.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.Environment.Platforms
{
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] private Platform _widePlatformPrefab;
        [SerializeField] private Platform _narrowPlatformPrefab;

        [SerializeField] private int _poolSizePerType = 10;
        [SerializeField] private float _minX = -2.2f;
        [SerializeField] private float _maxX = 2.2f;
        [SerializeField] private float _minYGap = 1.2f;
        [SerializeField] private float _maxYGap = 2.0f;
        [SerializeField] private float _despawnYOffset = -6f;
        [SerializeField] private float _spawnYOffset = 6f;
        
        [Range(0f, 1f)]
        [SerializeField] private float _wideChance = 0.6f; // 60% wide, 40% narrow

        private Camera _camera;
        private Transform _playerTransform;
        private float _highestSpawnedY;

        private Queue<Platform> _widePool = new();
        private Queue<Platform> _narrowPool = new();
        private List<Platform> _activePlatforms = new();

        [Inject]
        private void Construct(Camera cam, PlayerDoodleController player)
        {
            _camera = cam;
            _playerTransform = player.transform;
        }

        public void Initialize()
        {
            for (int i = 0; i < _poolSizePerType; i++)
            {
                _widePool.Enqueue(CreatePooled(_widePlatformPrefab));
                _narrowPool.Enqueue(CreatePooled(_narrowPlatformPrefab));
            }

            _highestSpawnedY = _playerTransform.position.y - 1f;
            SpawnInitialPlatforms();
        }

        private void Update()
        {
            SpawnIfNeeded();
            DespawnIfNeeded();
        }

        private void SpawnInitialPlatforms()
        {
            // Spawn a platform right under the player first
            var startPlatform = GetFromPool(true);
            startPlatform.transform.position = new Vector3(0f, _playerTransform.position.y - 0.5f, 0f);
            startPlatform.gameObject.SetActive(true);
            _activePlatforms.Add(startPlatform);

            var camHalfHeight = _camera.orthographicSize;
            var topY = _playerTransform.position.y + camHalfHeight + _spawnYOffset;

            while (_highestSpawnedY < topY)
                SpawnNext();
        }

        private void SpawnIfNeeded()
        {
            var camTop = _camera.transform.position.y + _camera.orthographicSize + _spawnYOffset;
            while (_highestSpawnedY < camTop)
                SpawnNext();
        }

        private void DespawnIfNeeded()
        {
            var camBottom = _camera.transform.position.y - _camera.orthographicSize + _despawnYOffset;

            for (var i = _activePlatforms.Count - 1; i >= 0; i--)
            {
                var p = _activePlatforms[i];
                if (p.transform.position.y < camBottom)
                {
                    ReturnToPool(p);
                    _activePlatforms.RemoveAt(i);
                }
            }
        }

        private void SpawnNext()
        {
            var isWide = Random.value < _wideChance;
            var gap = Random.Range(_minYGap, _maxYGap);
            var x = Random.Range(_minX, _maxX);
            var y = _highestSpawnedY + gap;

            var platform = GetFromPool(isWide);
            platform.transform.position = new Vector3(x, y, 0f);
            platform.gameObject.SetActive(true);
            _activePlatforms.Add(platform);

            _highestSpawnedY = y;
        }

        private Platform GetFromPool(bool wide)
        {
            var pool = wide ? _widePool : _narrowPool;
            var prefab = wide ? _widePlatformPrefab : _narrowPlatformPrefab;

            if (pool.Count > 0)
                return pool.Dequeue();

            return CreatePooled(prefab);
        }

        private void ReturnToPool(Platform platform)
        {
            platform.gameObject.SetActive(false);
            bool isWide = platform.IsWide;
            (isWide ? _widePool : _narrowPool).Enqueue(platform);
        }

        private Platform CreatePooled(Platform prefab)
        {
            var obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
            return obj;
        }
    }
}
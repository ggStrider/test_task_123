using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Internal.Scripts.Core.Data.Services;
using Internal.Scripts.Gameplay.Player;
using Internal.Scripts.Installers.Signals;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Gameplay.Observers
{
    public class PlayerLevelScoreRecorder : MonoBehaviour
    {
        [SerializeField] private float _updateTickRate = 0.02f;
        
        private CancellationTokenSource _recordingCts;
        
        private IPlayerDataService _playerDataService;
        private Transform _playerTransform;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(IPlayerDataService playerDataService, SignalBus signalBus,
            PlayerDoodleController playerDoodleController)
        {
            _playerDataService = playerDataService;
            _signalBus = signalBus;
            _playerTransform = playerDoodleController.transform;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerLoseSignal>(StopRecording);
            StartRecording();
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<PlayerLoseSignal>(StopRecording);
        }

        private void StartRecording()
        {
            _recordingCts = new();
            TryRecord(_recordingCts.Token).Forget();
        }
        
        private void StopRecording()
        {
            _recordingCts?.Cancel();
            _recordingCts?.Dispose();
        }

        private async UniTaskVoid TryRecord(CancellationToken recordingCtsToken)
        {
            try
            {
                while (!recordingCtsToken.IsCancellationRequested)
                {
                    _playerDataService.TryChangeCurrentLevelScore(_playerTransform.position.y);
                    await UniTask.WaitForSeconds(_updateTickRate, cancellationToken: recordingCtsToken);
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                _playerDataService.TryAddToRecordsCurrentLevelScore();
            }
        }
    }
}
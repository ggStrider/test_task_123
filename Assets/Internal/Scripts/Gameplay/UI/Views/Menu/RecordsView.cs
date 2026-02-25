using Internal.Scripts.Core.Data.Services;
using Internal.Scripts.Gameplay.UI.Views.Additional;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Scripts.Gameplay.UI.Views
{
    public class RecordsView : BaseUIPanel
    {
        [SerializeField] private RecordUI _recordPrefab;
        [SerializeField] private VerticalLayoutGroup _parent;

        private IPlayerDataService _playerDataService;

        [Inject]
        private void Construct(IPlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }
        
        public void OnEnable()
        {
            InitializeAllRecords();
        }

        private void InitializeAllRecords()
        {
            var recordsLength = _playerDataService.Records.Count;
            for (var i = 0; i < recordsLength; i++)
            {
                var recordUI = Instantiate(_recordPrefab, _parent.transform);

                var recordToInit = _playerDataService.Records[i];
                recordUI.Initialize(i, recordToInit);
            }
        }
    }
}
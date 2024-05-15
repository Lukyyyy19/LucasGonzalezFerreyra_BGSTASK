using Player;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : SingeltonMonoBehaviour<GameManager>
    {
        private PlayerData _playerData;
        private Transform _player;
        [SerializeField] private Transform _chest;
        [SerializeField] private GameObject _coinEarnedPrefab;
        protected override void Awake()
        {
            base.Awake();
            SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
        }

        public void InitializePlayerData(PlayerData playerData,Transform player)
        {
            _playerData = playerData;
            _player = player;
        }
        public void ChestDelivered(int coins)
        {
            Instantiate(_coinEarnedPrefab, _player.position,quaternion.identity);
            _playerData.UpdateCoins(coins);
            _chest.gameObject.SetActive(false);
            _chest.transform.position = Vector2.zero;
            _chest.gameObject.SetActive(true);
        }
       
    }
}

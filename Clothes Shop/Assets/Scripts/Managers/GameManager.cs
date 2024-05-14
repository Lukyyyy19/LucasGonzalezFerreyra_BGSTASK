using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : SingeltonMonoBehaviour<GameManager>
    {
        
        protected override void Awake()
        {
            base.Awake();
            SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
        }

       
    }
}

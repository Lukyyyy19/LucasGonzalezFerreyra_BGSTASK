using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
        }
    }
}

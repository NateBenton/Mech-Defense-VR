using UnityEngine;
using UnityEngine.SceneManagement;

namespace _NBGames.Scripts.Misc
{
    public class SceneTransitionTest : MonoBehaviour
    {
        public string levelToOpen;

        public void ButtonPressed()
        {
            SceneManager.LoadScene(levelToOpen);
        }
    }
}

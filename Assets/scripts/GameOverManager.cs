using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{ 
    public void GoToTitle()
    {
        SceneManager.LoadScene(0);
    }
}

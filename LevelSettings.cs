using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSettings : MonoBehaviour
{
    [SerializeField] float timeDelay = 2f;

    public void QuitGame()
    {
        Application.OpenURL("about:blank");
    }

    public void LoadStartScene()
    {
        FindObjectOfType<GameSession>().DestroyGameSession();
        SceneManager.LoadScene(0);
        FindObjectOfType<MusicPlayer>().DestroyMusicPlayer();
    }

    public void LoadNextScene()
    {
        int numScenes = SceneManager.sceneCountInBuildSettings;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentScene + 1);        
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayGameOver());
        FindObjectOfType<MusicPlayer>().DestroyMusicPlayer();        
    }

    public IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(timeDelay);
        SceneManager.LoadScene("GameOver");
    }

    public void LoadWinScene()
    {
        FindObjectOfType<MusicPlayer>().DestroyMusicPlayer();
        SceneManager.LoadScene("Win");
    }

}

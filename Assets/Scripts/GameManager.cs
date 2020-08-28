using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool autoPlay = false;
    [SerializeField] [Range(0.1f,10f)]
    private float gameSpeed = 1f;

    private void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public bool IsAutoPlay()
    {
        return autoPlay;
    }

    public void LoadLevel(string name)
    {
        Debug.Log("New Level load: " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void BrickDestroyed()
    {
        if (Brick.breakableBlocksCount <= 0)
        {
            LoadNextLevel();
        }
    }

}

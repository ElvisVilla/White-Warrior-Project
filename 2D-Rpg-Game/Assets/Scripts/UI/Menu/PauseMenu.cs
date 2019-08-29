using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI = null;
    public GameObject OptionsMenuUI = null;
    public GameObject HudPanel = null;
    Player player = null;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    // Update is called once per frame
    void Update () {
		
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume ()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        HudPanel.SetActive(true);
    }

    public void Pause ()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        HudPanel.SetActive(false);
    }

    public void Options()
    {
        PauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(true);
    }

    public void SetScene (int Index)
    {
        Resume();
        StartCoroutine(LoadAsyncOperation(Index));
    }

    IEnumerator LoadAsyncOperation(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        yield return new WaitForEndOfFrame();
    }

    public void SetMainMenu (int index)
    {
        SceneManager.LoadScene(index);
    }
}   


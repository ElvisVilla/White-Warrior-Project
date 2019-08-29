using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSettings : MonoBehaviour
{
    private bool inManuQuitWindow = false;

    public GameObject QuitGamePanel = null;
    public GameObject MainMenuOptions = null;
    public GameObject CreditsPanel = null;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!inManuQuitWindow)
            {
                PreviewQuitGame();
            }
            else
            {
                DontQuitGame();
            }
        }
    }

    public void PreviewQuitGame ()
    {
        inManuQuitWindow = true;
        MainMenuOptions.SetActive(false);
        QuitGamePanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }

    public void DontQuitGame ()
    {
        inManuQuitWindow = false;
        MainMenuOptions.SetActive(true);
        QuitGamePanel.SetActive(false);
    }

    public void SetLevel (int Index)
    {
        StartCoroutine(LoadAsync(Index));
    }

    IEnumerator LoadAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        yield return new WaitForEndOfFrame();
    }

    public void QuitGame()
    {

        Application.Quit();
    }
}

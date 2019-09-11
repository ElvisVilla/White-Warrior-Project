using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSettings : MonoBehaviour
{
    private bool isManuQuitWindow = false;

    public GameObject MainMenuOptions = null;
    public GameObject QuitGameDialog = null;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isManuQuitWindow)
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
        isManuQuitWindow = true;
        MainMenuOptions.SetActive(false);
        QuitGameDialog.SetActive(true);
    }

    public void DontQuitGame ()
    {
        isManuQuitWindow = false;
        MainMenuOptions.SetActive(true);
        QuitGameDialog.SetActive(false);
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

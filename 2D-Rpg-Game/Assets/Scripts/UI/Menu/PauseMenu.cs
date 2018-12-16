using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject HudPanel;
    Player player;

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
        SceneManager.LoadScene(Index);
    }

    public void SetMainMenu (int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SetMobile()
    {
        if(player.Motor.inputMode == Movement.InputType.Teclado)
        {
            player.Motor.inputMode = Movement.InputType.Joystick;
        }
    }

    public void SetKeyword()
    {
        if (player.Motor.inputMode == Movement.InputType.Joystick)
        {
            player.Motor.inputMode = Movement.InputType.Teclado;
        }
    }
}   


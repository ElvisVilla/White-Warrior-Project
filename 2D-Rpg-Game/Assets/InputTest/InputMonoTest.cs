using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMonoTest : MonoBehaviour, PlayerControls.IAbilitiesMapActions
{
    PlayerControls playerControls;

    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.AbilitiesMap.SetCallbacks(this);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void OnAbilityOne(InputAction.CallbackContext context)
    {
        Debug.Log("Ability 1");
    }

    public void OnAbilityTwo(InputAction.CallbackContext context)
    {
        Debug.Log("Ability 2");
    }

    public void OnAbilityThree(InputAction.CallbackContext context)
    {
        Debug.Log("Ability 3");
    }

    public void OnAbilityFour(InputAction.CallbackContext context)
    {
        Debug.Log("Ability 4");
    }
}

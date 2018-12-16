using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogIA : MonoBehaviour {

    [TextArea] public string[] combatInteractions;
    [TextArea] public string[] chaseInteractions;
    public TextMeshPro dialogbox;
    public GameObject canvas;
    public float timeToShow;
    int rotation = 0;

    /*El canvas rota junto con el gameobject del enemigo, el enemigo cuando va hacia la izqueirda rota 180, por lo tanto debemos rotar el canvas
     * de nuevo a 0, cuando el personaje rota
    */

    public void OnCombatInteractions()
    {
        StopAllCoroutines();
        StartCoroutine(Interactions(timeToShow, combatInteractions));
    }

    public void OnChaseInteractions()
    {
        StopAllCoroutines();
        StartCoroutine(Interactions(timeToShow, chaseInteractions));
    }

    public IEnumerator Interactions(float seconds, string[] interactions)
    {
        int index = Random.Range(0, 3);
        string actualMessage = interactions[index]; //Verificar porque no cambia.
        canvas.gameObject.SetActive(true);
        dialogbox.text = actualMessage;
        yield return new WaitForSeconds(seconds);
        canvas.gameObject.SetActive(false);
    }
}

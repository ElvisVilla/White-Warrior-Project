using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Spell : MonoBehaviour, IPointerDownHandler
{
    //Spell action debe obtener la habilidad actual en el slot.
    //No necesitamos ningun index ya que la habilidad que vamos a utilizar
    private Image ownImage;
    private Image coolDown;
    public Ability ability;

    public int index;
    public Player player;

    void Awake ()
    {
        ownImage = GetComponent<Image>();
        coolDown = transform.GetChild(0).GetComponent<Image>();
        coolDown.fillAmount = 0f;

        //Aplicamos transparencia ya que no habilidad por mostrar
        if (ability == null)
            ownImage.color = new Color(0, 0, 0, 0);
        else
            ownImage.sprite = ability.Icon;
    }

    void Update()
    {   
        //Color.White nos renderiza la imagen con su propio color, sin tinte.
        if (ability != null)
        {
            ownImage.color = Color.white;
            ownImage.sprite = ability.Icon;
        }
        else
            ownImage.color = new Color(0, 0, 0, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(ability != null && !player.Health.IsDead)
        {
            player.SetAbilityToEvent(ability);
            ability.Action(player);

            if (ability.IsOnCoolDown)
            {
                coolDown.fillAmount = 1f;
                coolDown.DOFillAmount(0f, ability.ColdDown - 0.2f);
            }
        }
    }

    public void SetSpell(Player player, int index, Ability ability)
    {
        this.index = index;
        this.player = player;
        this.ability = ability;
    }
}

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
    private Image disableImage;
    public Ability ability;
    public Color OnPress;

    public int index;
    public Player player;

    void Awake ()
    {
        ownImage = GetComponent<Image>();
        disableImage = transform.GetChild(0).GetComponent<Image>();
        coolDown = transform.GetChild(1).GetComponent<Image>();
        coolDown.fillAmount = 0f;

        //Aplicamos transparencia ya que no hay habilidad por mostrar
        if (ability == null)
            ownImage.color = new Color(0f, 0f, 0f, 0f);

        else if(ability != null)
        {
            ownImage.sprite = ability.Icon;
            if (!ability.RuneController.GotRunes(ability.RuneCost))
            {
                disableImage.enabled = true;
            }
        }
    }

    void Update()
    {
        //Color.White nos renderiza la imagen con su propio color, sin tinte.
        if (ability != null)
        {
            ownImage.color = Color.white;
            ownImage.sprite = ability.Icon;
            if (!ability.RuneController.GotRunes(ability.RuneCost))
            {
                disableImage.enabled = true;
            }
            else
                disableImage.enabled = false;
        }
        else
            ownImage.color = new Color(0f,0f,0f,0f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(ability != null && !player.Health.IsDead)
        {
            player.SetAbilityToEvent(ability);
            ability.Action(player);

            if (ability.IsCoolDown)
            {
                coolDown.fillAmount = 1f;
                coolDown.DOFillAmount(0f, ability.CoolDownSeconds - 0.2f).SetEase(Ease.Linear);
                ownImage.DOColor(OnPress, 0.15f);
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

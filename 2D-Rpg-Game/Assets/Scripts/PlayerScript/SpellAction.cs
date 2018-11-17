using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class SpellAction : MonoBehaviour, IPointerDownHandler
{
    //Spell action debe obtener la habilidad actual en el slot.
    //No necesitamos ningun index ya que la habilidad que vamos a utilizar
    private Image ownImage;
    private Image childImage;
    public Ability ability;
    [HideInInspector] public int index;
    [HideInInspector] public Player player;

    void Awake ()
    {
        ownImage = GetComponent<Image>();
        childImage = transform.GetChild(0).GetComponent<Image>();
        childImage.fillAmount = 0f;

        if (ability == null)
        {
            ownImage.color = new Color(0, 0, 0, 0);
        }
        else
        {
            ownImage.sprite = ability.Icon;
        }
    }

    void Update()
    {
        if (ability != null)
        {
            ownImage.color = Color.white;
            ownImage.sprite = ability.Icon;
        }
        else
        {
            ownImage.color = new Color(0, 0, 0, 0);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(ability != null)
        {
            ability.Action(player);
            if (ability.IsOnCoolDown)
            {
                childImage.fillAmount = 1f;
                childImage.DOFillAmount(0f, ability.ColdDown - 0.2f);
            }
        }
    }

    public void SetSpellAbility(Player player, int index, Ability ability)
    {
        this.index = index;
        this.player = player;
        this.ability = ability;
    }
}

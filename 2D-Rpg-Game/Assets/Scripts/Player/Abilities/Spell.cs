using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Spell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] RuneController runeController = null;
    [SerializeField] Image disableImage = null;
    [SerializeField] Image coolDownImage = null;
    [SerializeField] Color OnPressColor = new Color();
    [SerializeField] Color transparent = new Color();

    private Image ownImage = null;

    [Header("Debug porpuses")]
    [SerializeField] Ability ability = null;
    [SerializeField] Player player = null;
    public int index = 0;

    void Start()
    {
        runeController = FindObjectOfType<RuneController>(); //TODO: Es necesario cambiar esto.
        ownImage = GetComponent<Image>();
        ownImage.sprite = ability?.Icon;

        ability?.Init(player, runeController);
    }

    public void SetSpell(Player player, int index, Ability ability)
    {
        this.index = index;
        this.player = player;
        this.ability = ability;
    }

    void Update()
    {
        ability?.AbilityUpdate(player);

        //Este codigo debe ser refactorizado mediante eventos, y no debe debe comprbarse por update.
        Visuals();
    }

    private void Visuals()
    {
        if (ability != null)
        {
            ownImage.color = Color.white; //No tint
            ownImage.sprite = ability?.Icon;
            if (!runeController.GotRunes(ability.RuneCost))
            {
                disableImage.enabled = true;
            }
            else
                disableImage.enabled = false;
        }
        else
        {
            ownImage.color = transparent;
            disableImage.gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(ability != null)
        {
            //No comprobaremos las runas aca, al morir el jugador reiniciara las runas.
            if (/*!player.Health.IsDead &&*/ runeController.GotRunes(ability.RuneCost))
            {
                player.SetCurrentAbility(ability);
                ability.OnAbilityPressed(player, () => 
                {
                    CoolDownEffect();
                    runeController.UseRunes(ability.RuneCost);
                });
            }
        }
    }

    public void CoolDownEffect()
    {
        coolDownImage.fillAmount = 1f;
        coolDownImage.DOFillAmount(endValue: 0f, duration: ability.CoolDownSeconds - 0.2f).SetEase(Ease.Linear);
        ownImage.DOColor(OnPressColor, duration: 0.15f);
    }
}

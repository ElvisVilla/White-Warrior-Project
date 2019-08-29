using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Vector2 weaponProjection;
    Ability ability;

    [Space(10)]
    public Transform weapon;
    [SerializeField] private CharacterStats m_playerStats = null;

    [Space(10)]
    public List<Ability> abilities;

    public CharacterStats Stats => m_playerStats;

    public PlayerHealth Health { get; private set; }
    public Animator Anim { get; private set; }
    public Movement Movement { get; private set; }

    private PlayerControls playerControls;

    void Awake()
    {
        Movement = GetComponent<Movement>();
        Health = GetComponent<PlayerHealth>();
        Anim = GetComponentInChildren<Animator>();

        //Player Inputs.
        playerControls = new PlayerControls();
        playerControls.MovementMap.SetCallbacks(Movement);
    }

    void Update()
    {
        m_playerStats.Init(this);
    }

    #region Animation Event For Physics Attacks.
    /// <summary>
    /// Animation Event in animations clips just works with methods that remainds in Monobehaviour, so we call this method on player
    /// physics animations attack
    /// </summary>
    public void AnimationLogicEvent()
    {
        ability.OnCollisionLogic(this);
    }

    /// <summary>
    /// Ability field hold the corresponding ability that the player set when he press ability in action bar, this method comes before 
    /// AnimationLogicEvent to set ability.
    /// </summary>
    /// <param name="ability"></param>
    public void SetCurrentAbility(Ability ability)
    {
        if(this.ability != ability)
            this.ability = ability;
    }
    #endregion

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if(ability != null)
        {
            weaponProjection.Set(ability.Range, 1.5f);
            Gizmos.DrawWireCube(weapon.position, weaponProjection);
            return;
        }
        Gizmos.DrawWireCube(weapon.position, new Vector3(2f, 1f));
    }
}

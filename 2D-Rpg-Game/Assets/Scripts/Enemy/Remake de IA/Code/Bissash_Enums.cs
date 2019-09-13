namespace Bissash.IA
{
    public enum StateType
    {
        Patrol,
        Combat,
        Chase,
        Dead,
    }
}

namespace Bissash
{
    public enum FacingSide
    {
        Right = 0,
        Left = 1,
    }

    public enum MovementState
    {
        Controllable,
        NonControllable,
        PerformingAbility,
    }

    public enum CombatMode
    {
        Stun,
        Disoriented,
        NockBack,
        RegularCombat,
        Dead,
        Slowd,
    }

    public enum ResetMode
    {
        Manual,
        Automagic
    }
}



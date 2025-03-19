using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(UnitAnimEvent))]
[DisallowMultipleComponent]
public class UnitAnim : MonoBehaviour
{
    Animator anim;
    UnitAnimEvent animEvent;
    Unit unit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        animEvent = GetComponent<UnitAnimEvent>();
        unit = GetComponent<Unit>();
    }

    private void OnEnable()
    {
        anim.runtimeAnimatorController = unit.unitDetails.animationController;
        animEvent.OnStartUnitAnim += AnimEvent_OnStartUnitAnim;
        //animEvent.OnStopUnitAnim += AnimEvent_OnStopUnitAnim;
    }

    private void OnDisable()
    {
        animEvent.OnStartUnitAnim -= AnimEvent_OnStartUnitAnim;
    }

    private void AnimEvent_OnStartUnitAnim(AnimArgs args)
    {
        anim.speed = args.speed;
        switch (args.typeAnim)
        {
            case UnitState.Idle:
                IdleStart();
                break;
            case UnitState.Run:
                RunStart();
                break;
            case UnitState.Attack:
                AttackStart();
                break;
            case UnitState.Hurt:
                HurtStart();
                break;
            case UnitState.FirstSkill:
                FirstSkillStart();
                break;
            case UnitState.SecondSkill:
                SecondSkillStart();
                break;
            case UnitState.Dead:
                DeadStart();
                break;
            default:
                Debug.Log("Unexpected unit state");
                break;
        }
    }

    /*    private void AnimEvent_OnStopUnitAnim(AnimArgs args)
        {
            switch (args.typeAnim)
            {
                case UnitState.Idle:
                    IdleStop();
                    break;
                case UnitState.Run:
                    RunStop();
                    break;
                case UnitState.Attack:
                    AttackStop();
                    break;
                case UnitState.Hurt:
                    HurtStop();
                    break;
                case UnitState.FirstSkill:
                    FirstSkillStop();
                    break;
                case UnitState.SecondSkill:
                    SecondSkillStop();
                    break;
                case UnitState.Dead:
                    DeadStop();
                    break;
                default:
                    Debug.Log("Unexpected unit state");
                    break;
            }
        }*/


    public void IdleStart()
    {
        anim.SetBool("Idle", true);
        anim.SetBool("Attack", false);
        anim.SetBool("Hurt", false);
        anim.SetBool("Run", false);
        anim.SetBool("Dead", false);
        anim.SetBool("FirstSkill", false);
        anim.SetBool("SecondSkill", false);
    }

    //public void IdleStop() => anim.SetBool("Idle", false);

    public void AttackStart()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Attack", true);
        anim.SetBool("Hurt", false);
        anim.SetBool("Run", false);
        anim.SetBool("Dead", false);
        anim.SetBool("FirstSkill", false);
        anim.SetBool("SecondSkill", false);
    }

    //public void AttackStop() => anim.SetBool("Attack", false);

    public void HurtStart()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Hurt", true);
        anim.SetBool("Run", false);
        anim.SetBool("Dead", false);
        anim.SetBool("FirstSkill", false);
        anim.SetBool("SecondSkill", false);
    }

    //public void HurtStop() => anim.SetBool("Hurt", false);

    public void RunStart()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Hurt", false);
        anim.SetBool("Run", true);
        anim.SetBool("Dead", false);
        anim.SetBool("FirstSkill", false);
        anim.SetBool("SecondSkill", false);
    }

    //public void RunStop() => anim.SetBool("Run", false);

    public void DeadStart()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Hurt", false);
        anim.SetBool("Run", false);
        anim.SetBool("Dead", true);
        anim.SetBool("FirstSkill", false);
        anim.SetBool("SecondSkill", false);
    }

    //public void DeadStop() => anim.SetBool("Dead", false);

    public void FirstSkillStart()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Hurt", false);
        anim.SetBool("Run", false);
        anim.SetBool("Dead", false);
        anim.SetBool("FirstSkill", true);
        anim.SetBool("SecondSkill", false);
    }

    //public void FirstSkillStop() => anim.SetBool("FirstSkill", false);

    public void SecondSkillStart()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Hurt", false);
        anim.SetBool("Run", false);
        anim.SetBool("Dead", false);
        anim.SetBool("FirstSkill", false);
        anim.SetBool("SecondSkill", true);
    }

    //public void SecondSkillStop() => anim.SetBool("SecondSkill", false);

    #region UNITY_EDITOR
#if UNITY_EDITOR
    private void OnValidate()
    {

    }
#endif
    #endregion
}
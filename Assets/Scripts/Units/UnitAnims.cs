using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
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
        animEvent.OnStopUnitAnim += AnimEvent_OnStopUnitAnim;
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

    private void AnimEvent_OnStopUnitAnim(AnimArgs args)
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
    }


    public void IdleStart() => anim.SetTrigger("Idle");

    public void IdleStop() => anim.ResetTrigger("Idle");

    public void AttackStart() => anim.SetTrigger("Attack");

    public void AttackStop() => anim.ResetTrigger("Attack");

    public void HurtStart() => anim.SetTrigger("Hurt");

    public void HurtStop() => anim.ResetTrigger("Hurt");

    public void RunStart() => anim.SetTrigger("Run");

    public void RunStop() => anim.ResetTrigger("Run");

    public void DeadStart() => anim.SetTrigger("Dead");

    public void DeadStop() => anim.ResetTrigger("Dead");

    public void FirstSkillStart() => anim.SetTrigger("FirstSkill");

    public void FirstSkillStop() => anim.ResetTrigger("FirstSkill");

    public void SecondSkillStart() => anim.SetTrigger("SecondSkill");

    public void SecondSkillStop() => anim.ResetTrigger("SecondSkill");

    #region UNITY_EDITOR
#if UNITY_EDITOR
    private void OnValidate()
    {
        
    }
#endif
    #endregion
}
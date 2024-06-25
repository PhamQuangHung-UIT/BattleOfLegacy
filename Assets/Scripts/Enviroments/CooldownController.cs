using UnityEngine;

[RequireComponent(typeof(CooldownControllerEvent))]
[DisallowMultipleComponent]
public class CooldownController : MonoBehaviour
{
    private Animator anim;
    private CooldownControllerEvent _event;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _event = GetComponent<CooldownControllerEvent>();
        _event.OnCooldownStart += OnCooldownStart;
    }

    private void OnCooldownStart(CooldownEventArgs args)
    {
        anim.speed = 1 / args.timeInterval;
        anim.fireEvents = true;
        anim.Play("Play");
    }
}

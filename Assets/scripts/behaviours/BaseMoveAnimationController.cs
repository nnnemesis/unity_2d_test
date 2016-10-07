using UnityEngine;

public class BaseMoveAnimationController : MonoBehaviour {

    private Animator Animator;
    private IEventTire EventTire;

    void Start()
    {
        EventTire = this.GetEventTire();
        Animator = GetComponent<Animator>();
        EventTire.AddEventListener(TireEventType.ChangedMoveStateEvent, OnPlayerChangedMoveStateEvent);
        EventTire.AddEventListener(TireEventType.ChangedJumpStateEvent, OnPlayerChangedJumpStateEvent);
    }

    void OnDestory()
    {

    }

    private void OnPlayerChangedMoveStateEvent(object param)
    {
        object[] _params = (object[])param;
        var newState = (MoveState)_params[0];
        if (newState == MoveState.Idle)
        {
            Animator.SetTrigger("MoveIdle");
        }
        else if (newState == MoveState.Left || newState == MoveState.Right)
        {
            var shiftWalk = (bool)_params[1];
            if (shiftWalk)
            {
                Animator.SetTrigger("MoveShiftWalk");
            }
            else
            {
                Animator.SetTrigger("MoveWalk");
            }
        }
    }

    private void OnPlayerChangedJumpStateEvent(object param)
    {
        JumpState newState = (JumpState)param;
        if (newState == JumpState.Jump)
        {
            Animator.SetTrigger("Jump");
        }
        else if(newState == JumpState.Fall)
        {
            Animator.SetTrigger("Fall");
        }
        else if(newState == JumpState.Grounded)
        {
            Animator.SetTrigger("Grounded");
        }
    }

}

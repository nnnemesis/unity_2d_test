using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour, ITireEventListener {

    private PlayerState PlayerState;
    private IEventTire EventTire;
    public Transform LeftHandTransform;
    public GameObject InitWeapon;   // test

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        PlayerState = GetComponent<PlayerState>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
        EventTire.AddEventListener(TireEventType.PlayerChangedMoveStateEvent, this);
        EventTire.AddEventListener(TireEventType.PlayerChangedJumpStateEvent, this);
    }

    void Start() {
        GameObject testWeapon = (GameObject)Instantiate(InitWeapon, Vector3.zero, Quaternion.identity);
        testWeapon.transform.SetParent(LeftHandTransform, false);
        PlayerState.CurrentWeapon = testWeapon.GetComponent<IWeapon>();
    }
	
	void Update()
    {
	    
	}

    void FixedUpdate()
    {
        if(PlayerState.JumpState != PlayerJumpState.Grounded)
        {
            float velocityY = PlayerState.Rigidbody.velocity.y;
            if (velocityY < 0)    // raycast only when fall!
            {
                if(velocityY < -5)
                {
                    PlayerState.JumpState = PlayerJumpState.Fall;
                }                    
                Vector2 PlayerPosition = PlayerState.Transform.position;
                bool grounded = Physics2D.Linecast(PlayerPosition, PlayerPosition + PlayerState.GroundCheckVector, LayerMask.GetMask("Ground"));
                if (grounded)
                {
                    PlayerState.JumpState = PlayerJumpState.Grounded;
                }
            }
        }                    

        if (PlayerState.MoveState == PlayerMoveState.Walk || PlayerState.MoveState == PlayerMoveState.ShiftWalk)
        {
            var moveForse = Vector2.right * PlayerState.CurrentMoveForse * PlayerState.Direction;
            //Debug.Log("moveForse " + moveForse);
            PlayerState.Rigidbody.AddForce(moveForse);
        }
            
    }

    public void OnTireEvent(TireEvent ev)
    {
        //Debug.Log("PLAYER controller on tire event "+ev.Type);
        if(ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
        else if(ev.Type == TireEventType.PlayerChangedMoveStateEvent)
        {
            OnPlayerChangedMoveStateEvent((PlayerChangedMoveStateEvent)ev);
        }
        else if (ev.Type == TireEventType.PlayerChangedJumpStateEvent)
        {
            OnPlayerChangedJumpStateEvent((PlayerChangedJumpStateEvent)ev);
        }
    }

    private void OnPlayerChangedMoveStateEvent(PlayerChangedMoveStateEvent e)
    {
        if (PlayerState.MoveState == PlayerMoveState.Walk)
            PlayerState.CurrentMoveForse = PlayerState.WalkMoveForse;
        else if(PlayerState.MoveState == PlayerMoveState.ShiftWalk)
            PlayerState.CurrentMoveForse = PlayerState.ShiftWalkMoveForse;
        else if (PlayerState.MoveState == PlayerMoveState.Idle)
            PlayerState.CurrentMoveForse = 0f;
    }

    private void OnPlayerChangedJumpStateEvent(PlayerChangedJumpStateEvent e)
    {
        if(PlayerState.JumpState == PlayerJumpState.Jump)
        {
            PlayerState.Rigidbody.AddForce(Vector2.up * PlayerState.JumpForse);
        }
    }

    private void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
        if (actions[ControlAction.WalkForward])
        {
            PlayerState.Direction = 1;
            var rotation = transform.rotation;
            rotation.SetLookRotation(Vector3.forward);
            transform.rotation = rotation;
            if (actions[ControlAction.Shift])
            {
                PlayerState.MoveState = PlayerMoveState.ShiftWalk;
            }
            else
            {
                PlayerState.MoveState = PlayerMoveState.Walk;
            }
        }
        else if (actions[ControlAction.WalkBack])
        {
            PlayerState.Direction = -1;
            var rotation = transform.rotation;
            rotation.SetLookRotation(Vector3.back);
            transform.rotation = rotation;
            if (actions[ControlAction.Shift])
            {
                PlayerState.MoveState = PlayerMoveState.ShiftWalk;
            }
            else
            {
                PlayerState.MoveState = PlayerMoveState.Walk;
            }
        }
        else
        {
            PlayerState.MoveState = PlayerMoveState.Idle;
        }

        if (PlayerState.JumpState == PlayerJumpState.Grounded)
        {
            if (actions[ControlAction.Jump])
            {
                PlayerState.JumpState = PlayerJumpState.Jump;
            }
        }

        if(PlayerState.CurrentWeapon != null)
        {
            if (actions[ControlAction.MainAttack])
            {
                Debug.Log("START ATTACK");
                PlayerState.CurrentWeapon.StartUsing();
            }
            else
            {
                Debug.Log("STOP ATTACK");
                PlayerState.CurrentWeapon.StopUsing();
            }
        }
    }
}

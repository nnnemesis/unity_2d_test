using UnityEngine;
using System.Collections;

public class CameraFollowController : MonoBehaviour {

    private CameraFollowState State;
    private Transform PlayerTransform;

    void Awake()
    {
        State = GetComponent<CameraFollowState>();
    }

	void Start () {
        TryFindPlayer();
    }
	
    void TryFindPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            PlayerTransform = player.transform;
        }
    }

	void Update () {
        var playerPos = PlayerTransform.position;
        var pos = transform.position;

        var xDelta = pos.x - playerPos.x;
        var xDeltaAbs = Mathf.Abs(xDelta);
        var diff = xDeltaAbs - State.HorizontalDeadZone;
        if (diff > 0)
        {
            pos.x -= diff * Mathf.Sign(xDelta);
        }

        var yDelta = pos.y - playerPos.y;
        var yDeltaAbs = Mathf.Abs(yDelta);
        diff = yDeltaAbs - State.VerticalDeadZone;
        if (diff > 0)
        {
            pos.y -= diff * Mathf.Sign(yDelta);
        }

        transform.position = pos;
	}
}

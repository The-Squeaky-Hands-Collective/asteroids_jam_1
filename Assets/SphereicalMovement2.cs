using UnityEngine;
using System.Collections;

public class SphereicalMovement2 : MonoBehaviour
{
    public GameObject world;

    void Start()
    {

    }

    https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
        https://forum.unity3d.com/threads/super-mario-galaxy-gravity.122385/
        https://forum.unity3d.com/threads/spherical-movement-c.122754/
        https://www.gamedev.net/forums/topic/471604-how-to-controltrack-spherical-movement/

    void Update()
    {
        Vector3 up = (transform.position - world.transform.position).normalized;

        Input.GetAxis("Horizontal")

        transform.RotateAround(Vector3.zero, Vector3.up, gamePadState.ThumbSticks.Left.X * _fHPlayerSpeed * Time.deltaTime);
        transform.Translate(0, gamePadState.ThumbSticks.Left.Y * _fVPlayerSpeed * Time.deltaTime, 0);
        transform.LookAt(Vector3.zero);
    }
}

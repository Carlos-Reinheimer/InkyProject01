using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 0.01f;
    public Transform defaultTarget;

    private GameObject target;
    private Quaternion goal;
    private Vector3 direction;
    private Transform lookTarget;

    private void FixedUpdate() {
        target = GameObject.Find("Focus");

        lookTarget = target ? target.transform : defaultTarget;
        direction = (lookTarget.transform.position - transform.position).normalized; // direction
        goal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, goal, smoothSpeed);
    }
}

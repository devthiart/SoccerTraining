using System;
using System.Collections;
using UnityEngine;

public class BallGoalkeeper : MonoBehaviour
{
    private Transform myTransform;
    private Vector3 myStartPosition;
    private Transform cameraPosition;
    private Rigidbody rb;
    private bool onKick = false;

    [SerializeField]
    private Vector3 KickDirection = new Vector3(0.3f, 1, -1);
    [SerializeField]
    private float KickForce = 10f;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        myStartPosition = myTransform.position;
        rb = GetComponent<Rigidbody>();

        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform;
        // Debug.Log(cameraPosition.position.ToString());
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            onKick = true;
        }

        if (Input.GetButtonDown("Cancel") || myTransform.position.z <= cameraPosition.position.z) {
            GetBackToStartPosition();
        }

    }

    private void FixedUpdate()
    {
        if (onKick == true)
        {
            KickDirection = NewKickDirection();
            KickForce = NewKickForce();
            Kick(KickDirection, KickForce);
            onKick = false;
        }
    }

    public void Kick()
    {
        GetBackToStartPosition();
        onKick = true;
    }

    private void Kick(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void GetBackToStartPosition()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        myTransform.SetPositionAndRotation(myStartPosition, Quaternion.identity);
        Debug.Log(myTransform.position.ToString());
    }

    private Vector3 NewKickDirection()
    {
        return new Vector3(
            UnityEngine.Random.Range(-1.1f, 1.1f),
            UnityEngine.Random.Range(0f, 1.1f),
            -1
        );
    }

    private float NewKickForce()
    {
        return UnityEngine.Random.Range(10f, 20f);
    }
}

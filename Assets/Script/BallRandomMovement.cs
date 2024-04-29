using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallRandomMovement : MonoBehaviour
{
    public float MinKickForce = 2f;
    public float MaxKickForce = 5f;
    public float FrequencyKick = 0.5f;
    public Camera MainCamera;

    private Transform myTransform;
    private Vector3 myStartPosition;
    private bool isMoving = false;
    private Rigidbody rb;
    private Renderer myRenderer;
    private float kickForce = 0f;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myStartPosition = myTransform.position;
        rb = GetComponent<Rigidbody>();
        myRenderer = GetComponent<Renderer>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            GetBackToStartPosition();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = !isMoving;

            if (isMoving)
            {
                StartCoroutine(RandomMovement());
            } 
            else
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    IEnumerator RandomMovement()
    {
        while (isMoving)
        {
            Debug.Log("Running Random Movement...");
            Vector3 direction = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(0f, 1f),
                Random.Range(-0.01f, 0.01f)
            ).normalized;

            if (IsInCameraView() == false)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                direction = (myStartPosition - rb.position).normalized;
            }

            kickForce = Random.Range(MinKickForce, MaxKickForce);

            rb.AddForce(direction * kickForce, ForceMode.Impulse);

            yield return new WaitForSeconds(FrequencyKick);
        }
    }

    private bool IsInCameraView()
    {
        bool isVisible = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(MainCamera), myRenderer.bounds);

        return isVisible;
    }

    private void GetBackToStartPosition()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        myTransform.SetPositionAndRotation(myStartPosition, Quaternion.identity);
    }
}

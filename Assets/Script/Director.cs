using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    public bool RandomKick = false;
    private BallGoalkeeper ball;
    private GameObject panelTutorial;

    void Start()
    {
        ball = FindAnyObjectByType<BallGoalkeeper>();
        panelTutorial = GameObject.FindWithTag("Tutorial");

        if (RandomKick)
        {
            InvokeRepeating("KickBall", 1.0f, 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            panelTutorial.SetActive(false);
        }
    }

    private void KickBall()
    {
        ball.Kick();
    }
}

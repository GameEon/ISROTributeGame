using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    public bool rocketLaunched = false;
    Rocket rocket;

    void Awake()
    {
        rocket = FindObjectOfType<Rocket>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1) && rocketLaunched)
        {
            rocket.DetachNextModule();
        }

        if(Input.GetMouseButtonDown(0) && !rocketLaunched)
        {
            StartCoroutine(LaunchRocketAfterDelay());
            // rocketLaunched = true;
        }
    }

    IEnumerator LaunchRocketAfterDelay()
    {
        yield return new WaitForSeconds(3);
        rocketLaunched = true;
    }

}

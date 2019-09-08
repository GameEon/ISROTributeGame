using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject[] rocketModules;
    GameObject rocketReached;
    Rigidbody2D rigidbody;
    SpriteRenderer fire;
    Vector2 thrustForce;
    int detachModuleNumber = 0;
    float cameraEffectSize;
    public float thrustY = 100;
    bool canDetach = false;
    Transform moon;
    Transform selfTransform;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        thrustForce = new Vector2(0, thrustY);
        moon = GameObject.Find("moon").transform;
        selfTransform = transform;
        cameraEffectSize = Camera.main.orthographicSize;
        rocketReached = GameObject.Find("rocketReached");
        fire = rocketModules[0].transform.Find("fire").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(GameManager.Instance.rocketLaunched)
        {
            thrustForce = new Vector2(0, thrustForce.y+Time.deltaTime);
            rigidbody.velocity = thrustForce;
            fire.color = Color.Lerp(fire.color, Color.white, Time.deltaTime);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraEffectSize, Time.deltaTime);
        }
    }

    public void DetachNextModule()
    {
        if(detachModuleNumber < rocketModules.Length - 1 && canDetach)
        {
            rocketModules[detachModuleNumber].transform.parent = null;
            Rigidbody2D r = rocketModules[detachModuleNumber].AddComponent<Rigidbody2D>();
            r.AddTorque(10);
            detachModuleNumber ++;
            if(cameraEffectSize >= 3)
            cameraEffectSize-=1;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Detach")
        {
            canDetach = true;
        }

        if(collider.gameObject.tag == "MoonLimit")
        {
            Camera.main.transform.parent = rocketReached.transform;
            rocketReached.GetComponent<Animator>().enabled = true;
            cameraEffectSize = 5;
            rocketModules[detachModuleNumber].GetComponent<SpriteRenderer>().enabled = false;
            // gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportModule : MonoBehaviour
{
    public Animator[] animators;
    bool hasReleasedSupport = false;
    public float delayBetweenSupports = 0;

    void Awake()
    {
        animators = transform.GetComponentsInChildren<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !hasReleasedSupport)
        {
            StartCoroutine(ReleasedSupport());
        }
    }

    IEnumerator ReleasedSupport()
    {
        for(int i=0;i<animators.Length;i++)
        {
            animators[i].SetInteger("support",1);
            yield return new WaitForSeconds(delayBetweenSupports);
        }
    }
}

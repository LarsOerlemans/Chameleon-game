using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dash : MonoBehaviour
{

    private Rigidbody rb;
    private Chameleon chameleon;
    public Transform orientation;

    public float dashForce;

    public float attackRate = 1.4f;
    float nextAttackTime = 0f;

    public string key = "b";
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        chameleon = GetComponent<Chameleon>();
    }

    // Update is called once per frame
    void Update()
    {
         if(Time.time >= nextAttackTime)
         {
            if(Input.GetKeyDown(key))
            {
                DoDash();
                nextAttackTime = Time.time + 1f / attackRate;
            }
         }
    }

    private void DoDash()
    {
        chameleon.animationState(false);
        chameleon.moveState(false);
        chameleon.GetComponent<Animator>().Play("dash");
        StartCoroutine(EndDash());
        Vector3 forceToApply = orientation.forward * dashForce;
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }

    //timer
    private IEnumerator EndDash(){
        yield return new WaitForSeconds(0.3f);
        chameleon.moveState(true);
        chameleon.animationState(true);
    }
}

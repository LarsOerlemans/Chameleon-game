using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dash : MonoBehaviour
{

    private Rigidbody rb;
    public Transform orientation;

    public float dashForce;

    public float attackRate = 1.4f;
    float nextAttackTime = 0f;

    public string key = "g";
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        Vector3 forceToApply = orientation.forward * dashForce;
        rb.AddForce(forceToApply, ForceMode.Impulse);

    }

}

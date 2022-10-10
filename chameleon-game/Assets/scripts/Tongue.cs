using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{   

    public float tonguespeed = 10;

    public Grapple grapple;
    public Rigidbody rigid;
    public LineRenderer lineRenderer;
    private Transform start;
    private Transform Attack;

    // Start is called before the first frame update
    public void Initialize(Grapple grapple, Transform shootTransform, Transform AttackPoint)
    {
        start = shootTransform;
        Attack = AttackPoint;
        transform.forward = shootTransform.forward;
        this.grapple = grapple;
        rigid = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        rigid.AddForce(transform.forward * tonguespeed * 4f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] positions =  new Vector3[]{
                transform.position,
                start.position
            };
        lineRenderer.SetPositions(positions);
        Attack.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((LayerMask.GetMask("Grapple") & 1 << other.gameObject.layer) > 0)  || (LayerMask.GetMask("Enemy") & 1 << other.gameObject.layer) > 0 ){
            rigid.useGravity = false;
            rigid.isKinematic = true;

            grapple.StartPull();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    public float pullSpeed = 8f;
    public float stopDistance = 4f;
    public GameObject tonguePrefab;
    public Transform shootTransform;
    public AudioSource whip_start;
    public AudioSource whip_end;

    public Tongue tongue;
    public bool pulling;
    public Rigidbody rigid;
    public Chameleon host;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        pulling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(tongue == null && Input.GetMouseButtonDown(0)){
            StopAllCoroutines();
            pulling = false;
            tongue = Instantiate(tonguePrefab, shootTransform.position, Quaternion.identity).GetComponent<Tongue>();
            tongue.Initialize(this, shootTransform);
            whip_start.Play();
            host.animationState(false);
            host.moveState(false);
            host.GetComponent<Animator>().Play("tongue");
            StartCoroutine(DestroyTongueAfterLifeTime());
        }

        if(!pulling || tongue ==null) return;

        if(Vector3.Distance(transform.position, tongue.transform.position) <= stopDistance){
            host.animationState(true);
            host.moveState(true);
            DestroyTongue();
        } else {
            rigid.AddForce((tongue.transform.position - transform.position).normalized * pullSpeed, ForceMode.VelocityChange);
        }

    }

    public void StartPull(){
        pulling = true;
        whip_end.Play();
    }

    private void DestroyTongue(){
        if(tongue == null) return;
        
        pulling = false;
        Destroy(tongue.gameObject);
        tongue = null;
    }

    private IEnumerator DestroyTongueAfterLifeTime(){
        yield return new WaitForSeconds(2f);
        host.animationState(true);
        host.moveState(true);
        DestroyTongue();
    }

}

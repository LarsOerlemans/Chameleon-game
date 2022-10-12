using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackScript : MonoBehaviour
{

    public Transform AttackPoint;
    public AudioSource tail_sound;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public string tail = "g";
    public int attackDamage = 40;
    public GameObject chameleon;
    public Chameleon host;
    public float attackRate = 1.4f;
    float nextAttackTime = 0f;
    public float strength = 50f;
    private Rigidbody rigid;
    public GameObject chameleon2;

    void Start()
    {
        //setup rigidbody
        rigid = chameleon2.GetComponent<Rigidbody>();
        rigid.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(tail))
            {
                chameleon.GetComponent<Animator>().Play("tailswipe");
                host.animationState(false);
                host.moveState(false);
                tail_sound.Play();
                Attack();
                StartCoroutine(EndTailswipe());
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
       
    }

    void Attack()
    {
        // Play Attack Animation
        Collider[] hitEnemies = Physics.OverlapSphere(AttackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamage(attackDamage);
            PlayKnockback(chameleon2);
            
        }

    }
    void OnDrawGizmosSelected()
    {
        if(AttackPoint == null)
        {
            return;
        }
        
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }

    private IEnumerator EndTailswipe(){
        yield return new WaitForSeconds(0.7f);
        host.animationState(true);
        host.moveState(true);
    }

    public void PlayKnockback(GameObject sender)
    {
        Vector3 direction = (transform.position - sender.transform.position).normalized;
        rigid.AddForce(-direction*strength, ForceMode.Impulse);
    }


}

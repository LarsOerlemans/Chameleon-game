using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public Transform AttackPoint;
    public AudioSource tail_sound;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public string tail = "g";
    public int attackDamage = 40;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(tail))
        {
            Attack();
        }
       
    }

    void Attack()
    {
        // Play Attack Animation
        Collider[] hitEnemies = Physics.OverlapSphere(AttackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamage(attackDamage);
            tail_sound.Play();
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

}

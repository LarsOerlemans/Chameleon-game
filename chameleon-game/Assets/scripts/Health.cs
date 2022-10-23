using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int curHealth = 0;
    public int maxHealth = 100;
    public Chameleon host;
    public string winner;
    float nextAttackTime = 10f;
    public GameObject player;

    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown( KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage (int damage)
    {
        curHealth -= damage;
        healthBar.value = curHealth;
        // Play hurt animation

        if (curHealth <= 0)
        {
            Die();
            StartCoroutine(ExecuteAfterTime(2));
        }
    }
    void Die()
    {
        //Die Animation
        host.GetComponent<Animator>().Play("death");
        host.animationState(false);
        host.moveState(false);
        Debug.Log("Dead");
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Name.name = player.name; 
        SceneManager.LoadScene(winner);
    }
}

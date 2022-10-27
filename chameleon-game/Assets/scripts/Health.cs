using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float curHealth = 0f;
    public float maxHealth = 200f;
    public Chameleon host;
    public string winner;
    float nextAttackTime = 10f;
    public GameObject player;
    public float startingPitch = 1.1f;
    public float half = 1.5f;
    public float danger = 1.9f;

    public AudioSource audioSource;

    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        audioSource = GameObject.Find("SoundTrack").GetComponent<AudioSource>();
        audioSource.pitch = startingPitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown( KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage (float damage)
    {
        curHealth -= damage;
        healthBar.value = curHealth/2;
        // Play hurt animation

        if (curHealth <= 50)
        {
            audioSource.pitch = half;

        }
        if (curHealth <= 20)
        {
            audioSource.pitch = danger;

        }

        if (curHealth <= 0)
        {
            Die();
            StartCoroutine(ExecuteAfterTime(2));
        }
    }

    public void Heal(float value){
        curHealth += value;
        healthBar.value = curHealth;

        if (curHealth <= 50)
        {
            audioSource.pitch = half;

        }
        if (curHealth <= 20)
        {
            audioSource.pitch = danger;

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

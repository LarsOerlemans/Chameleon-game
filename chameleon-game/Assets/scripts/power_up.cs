using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_up : MonoBehaviour
{
    public int power;
    private Chameleon chameleon;

    public AudioSource heal_sfx;
    public AudioSource powerup_sfx;
    public AudioSource powerdown_sfx;
    // Start is called before the first frame update
    void Start()
    {
        power = Random.Range(1, 6);
        Debug.Log(power);
        if (power == 1){
           //speed boost
        }
        if (power == 2){
           //damage boost
        }
        if (power == 3){
           //health boost / healing
        }
        if (power == 4){
           //speed decrease
        }
        if (power == 5){
           //damage decrease
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other){
        if((LayerMask.GetMask("Enemy") & 1 << other.gameObject.layer) > 0 ){
            chameleon = other.gameObject.GetComponent<Chameleon>();
            if (power == 1){
                //speed boost
                powerup_sfx.Play();
                chameleon.speedBoost(true);
                Destroy(gameObject);
            }
            if (power == 2){
                //damage boost
                powerup_sfx.Play();
                other.gameObject.GetComponent<Grapple>().DamageMultiplier(1.3f);
                other.gameObject.GetComponent<AttackScript>().DamageMultiplier(1.3f);
                Destroy(gameObject);
            }
            if (power == 3){
                //health boost / healing
                heal_sfx.Play();
                other.gameObject.GetComponent<Health>().Heal(20f);
                Destroy(gameObject);
            }
            if (power == 4){
                //speed decrease
                powerdown_sfx.Play();
                chameleon.speedDecrease(true);
                Destroy(gameObject);
            }
            if (power == 5){
                //damage decrease
                powerdown_sfx.Play();
                other.gameObject.GetComponent<Grapple>().DamageMultiplier(0.7f);
                other.gameObject.GetComponent<AttackScript>().DamageMultiplier(0.7f);
                Destroy(gameObject);
            }
        }
    }

}

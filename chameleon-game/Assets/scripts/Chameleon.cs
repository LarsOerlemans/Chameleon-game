using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 50;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public GameObject chameleon;


    // Start is called before the first frame update
    void Start()
    {
        //code
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0, y).normalized;

        if (direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            chameleon.GetComponent<Animator>().Play("walking");
            controller.Move(direction * speed * Time.deltaTime);
        } else {
            chameleon.GetComponent<Animator>().Play("idle");
        }
          
    }
}

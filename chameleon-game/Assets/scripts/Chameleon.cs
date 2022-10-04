using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : MonoBehaviour
{
    //variables to be set
    public float speed = 10;
    public float grounddrag = 9;
    public float turnSmoothTime = 0.1f;
    public bool animation = true;
    public bool move = true;

    //other variables needed
    float turnSmoothVelocity;
    public GameObject chameleon;
    public Rigidbody rigid;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        //setup rigidbody
        rigid = GetComponent<Rigidbody>();
        rigid.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //set drag
        rigid.drag = grounddrag;

        //get movement input and direction
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0, y).normalized;

        //movement + animation
        if(move == true){
            if (direction.magnitude >= 0.1f){
                //get angle
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                //start animation + speed controll
                if(animation == true){
                    chameleon.GetComponent<Animator>().Play("walking");
                }
                SpeedControl();

                //actual line that does the moving
                rigid.AddForce(direction * speed * 10f, ForceMode.Force);

            } else if (velocity == Vector3.zero){
                //idle animation if not moving
                if(animation == true){
                    chameleon.GetComponent<Animator>().Play("idle");
                }
            }
        }
        
    }

    //keeps track of velocity
    void OnCollisionEnter(Collision collision)
    {
        velocity = collision.relativeVelocity;
    }

    public void animationState(bool state){
        animation = state;
    }

    public void moveState(bool state){
        move = state;
    }

    //make sure we dont exeed the maximum speed
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);

        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rigid.velocity = new Vector3(limitedVel.x, rigid.velocity.y, limitedVel.z);
        }
    }
}

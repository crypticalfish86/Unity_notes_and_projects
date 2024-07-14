using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{

    //our variable declarations must be inside the class
    [SerializeField] float steerSpeed = 1f; //the speed of our steering
    [SerializeField] float moveSpeed = 0.01f; //the speed our car is moving (backwards and forwards)
    [SerializeField] float boostSpeed = 0.01f; //Speed our car goes when our car runs over a boost

    //when touching boost double move speed
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Boost"){
            moveSpeed = boostSpeed;
            Destroy(other.gameObject);
        }
    }

    //slow down from boost after collision with object
    private void OnCollisionEnter2D(Collision2D other) {
        if (moveSpeed >= 30){
            moveSpeed = moveSpeed / 2;
        }
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        /*
            every frame calculate the amount the car is steering
            It is then mutiplied by steerSpeed so we are able to
            change how fast we turn. 

            The value is then finally multipleid by Time.deltaTime to make
            the value frame rate independent
        */
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;

        /*
            Every frame calculate the speed of the car It is 
            then multiplied by moveSpeed so we are able to change
            how fast the car is moving back and forth

            The value is then finally multipleid by Time.deltaTime to make
            the value frame rate independent
        */
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        
        /*Every frame rotate car on the (x, y, z) axis using inputs in unity (directions inverted)*/
        transform.Rotate(0, 0, -steerAmount);

        /*Every frame move our car forward or backwards relative to its y direction*/
        transform.Translate(0, moveAmount , 0);
    }
}

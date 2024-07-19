using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


//Script for player controls
public class PlayerController : MonoBehaviour {

    //movement components (rotation and general physics)
    Rigidbody2D rigidbody; //The rigid body on our object
    [SerializeField] float torqueAmount = 1f; //amount of torque applied to character

    //surface Effector component
    SurfaceEffector2D groundSurfaceEffector; //reference to level surface effector
        private float normalSpeed;//normal speed of surface effector
        private float boostSpeed;//bosted speed of surface effector for boost

    //particle system components
    [SerializeField] ParticleSystem snowTrailEffect; //particle system for the snow trail effect
        private ParticleSystem.EmissionModule snowTrailEmmisionModule; //Emmision module for snowtrail particle emmiter
            private float normalEmission; //current rate of emmision
            private float boostedEmission; //doubled rate of emmision for boost
    
    //controls
    bool canControl; //bool for whether you can or can't control player

    
    
    // Start is called before the first frame update
    private void Start() {
        canControl = true; //initiates player controls
        rigidbody = GetComponent<Rigidbody2D>(); //get rigid body of player
        groundSurfaceEffector = FindObjectOfType<SurfaceEffector2D>(); //get surface effector of ground
        normalSpeed = groundSurfaceEffector.speed; //set normal speed
        boostSpeed = groundSurfaceEffector.speed * 2; //set boosted speed

        //particle emission
        snowTrailEmmisionModule = snowTrailEffect.emission;
        normalEmission = snowTrailEmmisionModule.rateOverTime.constant;
        boostedEmission = snowTrailEmmisionModule.rateOverTime.constant * 2; 
    }

    // Update is called once per frame
    private void Update() {

        //if player controls are enabled we can rotate and boost player
        if (canControl) {
            RotatePlayer();
            RespondToBoost();
        }
    }
    
    //if we push arrows/WASD we will add torque in either direction
    private void RotatePlayer() {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            rigidbody.AddTorque(torqueAmount  * Time.deltaTime);
        } 
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            rigidbody.AddTorque(-torqueAmount * Time.deltaTime);
        }
    }

    //If we push up, then double speed of surface effector and double particle emmision rate, otherwise normal speed and normal particle emmision
    private void RespondToBoost() {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            groundSurfaceEffector.speed = boostSpeed;
            snowTrailEmmisionModule.rateOverTime = new ParticleSystem.MinMaxCurve(boostedEmission);
        }
        else{
            groundSurfaceEffector.speed = normalSpeed;
            snowTrailEmmisionModule.rateOverTime = new ParticleSystem.MinMaxCurve(normalEmission);
        }
    }


    //In the event we want to disable player controls, run this function in a script
    public void DisableControls() {
        canControl = false;
    }
}

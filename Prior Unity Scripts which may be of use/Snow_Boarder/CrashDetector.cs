using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float levelReloadDelaySeconds = 1;
    [SerializeField] ParticleSystem deathEffect; //player explosion
    [SerializeField] GameObject head; //player head
    [SerializeField] GameObject legs;//player legs
    [SerializeField] GameObject snowTrail; //player snowtrail
    [SerializeField] GameObject Camera; //the camera for the game
    [SerializeField] AudioClip crashSFX; //The SFX of the player crashing
    
    private CinemachineBrain cinemachineBrain; //the cinemachine brain for the camera
    private Rigidbody2D rigidBody; //the physics of the player
    private bool hitGroundYet; //needed to prevent double particle effect

    //needed start to have a non static value for rigidBody
    private void Start() {
        cinemachineBrain = Camera.GetComponent<CinemachineBrain>(); //get the logic system of cinemachine
        rigidBody = GetComponent<Rigidbody2D>();//at game start get the physics
        hitGroundYet = false; //needed to prevent double particle effect
    }

    //Triggers when player head touches level ground
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Ground") && !hitGroundYet) {
            hitGroundYet = true; //prevent double play
            PlayCrashEffects();
            DeletePlayer();
            Invoke("ReloadScene", levelReloadDelaySeconds); //reload game
        }
    }
    
    /*
        Plays the effects on player crash:
            1.Disables player controls
            2.sets player velocity to zero
            3.Plays crash SFX
            4.Stops camera motion
            5.Triggers snow crash particle effect
    */
    private void PlayCrashEffects(){
            FindObjectOfType<PlayerController>().DisableControls();//disables player controls
            rigidBody.velocity = Vector3.zero; //stop player object movement
            rigidBody.Sleep(); //puts rigid body to sleep to prevent object starting to move again
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Destroy(cinemachineBrain); //stop camera
            deathEffect.Play(); //particle explosion
    }

    //Deletes player
    private void DeletePlayer(){
            Destroy(head); //delete player head
            Destroy(legs); //delete player bottom
            Destroy(snowTrail);//delete snowboard particle effect
    }

    //Restarts level if snowboarder hits head
    private void ReloadScene(){
        SceneManager.LoadScene("Snow_Boarder");
    }


}

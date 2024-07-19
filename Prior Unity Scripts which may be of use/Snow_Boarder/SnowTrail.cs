using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Start and stop snow trail particle effects on snowboard

public class SnowTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem playerSnowTrail;

    //turn on snowtrail when snowboard and ground interact
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground"){
            playerSnowTrail.Play();
        }
    }

    //turn off snowtrail when snowboard leaves ground
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            playerSnowTrail.Stop();
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float levelReloadDelaySeconds = 1; //Delay for level reload
    [SerializeField] ParticleSystem finishLineEffect; //finish line confetti

    private AudioSource levelFinishSoundEffect; //finish line sound effect

    private bool hasFinished = false; //bool to prevent double trigger play

    //Triggered on reaching finish line
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !hasFinished){
            hasFinished = true;
            triggerLevelFinishEffects();
            Invoke("ReloadScene", levelReloadDelaySeconds);
        }
    }

    /*
        Effects on level finish:
        1.Activate confetti particle effect
        2.Play the finish line sound effect
    **/
    private void triggerLevelFinishEffects(){
            finishLineEffect.Play();
            levelFinishSoundEffect = GetComponent<AudioSource>();
            levelFinishSoundEffect.Play();
    }

    //Reloads level
    private void ReloadScene(){
        SceneManager.LoadScene("Snow_Boarder");
    }
}

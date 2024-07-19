using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBox : MonoBehaviour
{
    //field to determine car colour when package is collected
    [SerializeField] Color32 hasPackageColor = new Color32 (1, 1, 1, 1); 
    
    //field to determine car colour when package isn't collected
    [SerializeField] Color32 noPackageColor = new Color32 (1, 1, 1, 1);
    bool hasPackage = false; //bool on whether player has package
    [SerializeField] float destroyDelay = 0.01f; //Field to determine package obj destruction delay

    SpriteRenderer sprite;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = noPackageColor;
    }

    /*
        OnCollisionEnter2D is a method that executes
        when the object this script is attached to
        collides with something else

        other represents information about the object that 
        this object has collided with
    */
    public void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log("You've hit an object");
    }

    /*
        OnTriggerEnter2D is a method that executes 
        when the object this script is attached to
        overlaps with a "trigger" object

        other represents information about the object 
        that this object has collided with
    */
    private void OnTriggerEnter2D(Collider2D other) {

        //If object collides with package then it is "picked up"
        if (other.tag == "Package" && !hasPackage){
            hasPackage = true;
            Destroy(other.gameObject, destroyDelay);
            sprite.color = hasPackageColor;
            Debug.Log("Package Collected!");
        }
        else if (other.tag == "Customer" && hasPackage){
            hasPackage = false;
            sprite.color = noPackageColor;
            Debug.Log("Package Delivered!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject car;


    /*
        Our Camera should move in relation to the player object,
        Always keeping itself moving so its at the position in the
        centre of the player object
    */
    // Start is called before the first frame update
    void Update()
    {
        transform.position = car.transform.position + new Vector3 (0, 0, -10);
    }
}

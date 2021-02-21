using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : MonoBehaviour
{
    public int requiredSize = 1;
    public GameObject interactSphere;
    Vector3 startLocation;

    PlayerActions carryingMe;

    // Start is called before the first frame update
    void Start()
    {
        startLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pickup(PlayerActions player) {
        carryingMe = player;
    }

    public void Respawn() {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = startLocation;
        if(carryingMe) {
            carryingMe.Drop();
        }
    }

    public void Focus() {
        interactSphere.SetActive(true);
    }

    public void LostFocus() {
        interactSphere.SetActive(false);
    }
}

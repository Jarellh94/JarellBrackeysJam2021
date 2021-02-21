using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Device
{
    public GameObject beam;
    public BoxCollider beamCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Activate()
    {
        base.Activate();
        beam.SetActive(false);
        beamCollider.enabled = false;
    }

    public override void Deactivated()
    {
        base.Deactivated();
        beam.SetActive(true);
        beamCollider.enabled = true;
        
    }

    private void OnTriggerEnter(Collider other) {
        Individual deadMan = other.GetComponent<Individual>();

        if(deadMan) {
            //Spawn death particles
            FindObjectOfType<GameManager>().RespawnPlayer();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public int requiredSize = 1;

    public Device myDevice;

    public Renderer redBit;
    public Material activatedMaterial;
    public Material inactiveMaterial;
    public AudioSource onAudioSource;
    public AudioSource offAudioSource;

    public Individual onMe;
    public Carryable boxOnMe;
    bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(onMe) {
            if(onMe.mySize < requiredSize && isActivated) {
                Deactivate();
            }
            else if(!isActivated && onMe.mySize >= requiredSize) {
                Activate();
            }
        }
        else if(isActivated && !boxOnMe) {
            Deactivate();
        }
    }

    void Activate() {
        myDevice.Activate();
        isActivated = true;
        redBit.material = activatedMaterial;
        if(onAudioSource)
            onAudioSource.Play();
    }

    void Deactivate() {
        myDevice.Deactivated();
        isActivated = false;
        redBit.material = inactiveMaterial;
        if(offAudioSource)
            offAudioSource.Play();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Individual>())
            onMe = other.GetComponent<Individual>();
        if(other.GetComponent<Carryable>())
            boxOnMe = other.GetComponent<Carryable>();

        if(onMe && onMe.mySize >= requiredSize && !isActivated) {
            Activate();
        } 
        else if(boxOnMe && boxOnMe.requiredSize >= requiredSize && !isActivated){
            Activate();
        }else {
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.GetComponent<Individual>()){
            onMe = null;
        }
        else if(other.GetComponent<Carryable>()) {
            boxOnMe = null;
        }

        if(onMe != null && boxOnMe != null && isActivated) {
            Deactivate();
        }
    }
}

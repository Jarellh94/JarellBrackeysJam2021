using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    public float timeOn = 3f;
    public Device myDevice;
    public Renderer redBit;
    public Material activatedMaterial;
    public Material inactiveMaterial;
    public AudioSource tickingSource;
    public AudioSource beepSource;
    bool isActivated = false;

    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivated){
            if(timer < timeOn) {
                timer += Time.deltaTime;
            }
            else {
                myDevice.Deactivated();
                tickingSource.Stop();
                beepSource.Play();
                isActivated = false;
                redBit.material = inactiveMaterial;
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        if(!isActivated) {
            myDevice.Activate();
            
            tickingSource.Play();
            isActivated = true;
            redBit.material = activatedMaterial;
            timer = 0;
        }
    }

    public override void Focus()
    {
        base.Focus();
    }

    public override void LostFocus()
    {
        base.LostFocus();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButtonGroup : Device
{
    public int numButtons;
    int isActivatedNum = 0;
    bool beenActivated = false;
    public Device myDevice;

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
        isActivatedNum++;
        if(isActivatedNum >= numButtons && !beenActivated) {
            myDevice.Activate();
            beenActivated = true;
        }
    }

    public override void Deactivated()
    {
        base.Deactivated();
        if(!beenActivated) {
            isActivatedNum--;
            if(isActivatedNum < numButtons) {
                myDevice.Deactivated();
            }
        }
    }
}

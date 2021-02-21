using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGroup : Device
{
    public int numButtons;
    int isActivatedNum = 0;
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
        if(isActivatedNum >= numButtons) {
            myDevice.Activate();
        }
    }

    public override void Deactivated()
    {
        base.Deactivated();
        isActivatedNum--;
        if(isActivatedNum < numButtons) {
            myDevice.Deactivated();
        }
    }
}

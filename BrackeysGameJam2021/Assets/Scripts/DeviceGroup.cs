using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceGroup : Device
{
    public List<Device> childDevices;

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
        foreach(Device device in childDevices) {
            device.Activate();
        }
    }

    public override void Deactivated()
    {
        base.Deactivated();
        foreach(Device device in childDevices) {
            device.Deactivated();
        }
    }
}

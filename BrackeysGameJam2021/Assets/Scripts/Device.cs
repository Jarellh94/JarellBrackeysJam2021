using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour
{
    public AudioSource openSource;
    public AudioSource closeSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Activate() {
        if(openSource)
            openSource.Play();
    }

    public virtual void Deactivated() {
        if(closeSource)
            closeSource.Play();
    }
}

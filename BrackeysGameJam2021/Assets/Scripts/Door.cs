using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Device
{
    public BoxCollider myCollider;

    Animator animator;

    public int numActivated = 0;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        if(numActivated == 0) {
            base.Activate();
            animator.SetBool("IsOpen", true);
            myCollider.enabled = false;
        }

        numActivated++;
    }

    public override void Deactivated()
    {
        if(numActivated > 0)
            numActivated--;

        if(numActivated <= 0) {
            base.Deactivated();
            animator.SetBool("IsOpen", false);
            myCollider.enabled = true;
        }
        
    }
}

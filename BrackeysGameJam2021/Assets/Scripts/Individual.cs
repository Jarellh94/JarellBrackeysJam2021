using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individual : Interactable
{
    public int mySize = 1;
    public GameObject individualPrefab;
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();
        //Absorb.
    }

    public void AddIndivduals(int numIndividuals) {
        mySize += numIndividuals;
    }

    public void EvictIndividual() {
        mySize--;
    }
}

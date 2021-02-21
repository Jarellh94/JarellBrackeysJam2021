using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject interactSphere;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact() {
        
    }

    public virtual void Focus() {
        interactSphere.SetActive(true);
    }

    public virtual void LostFocus() {
        interactSphere.SetActive(false);
    }
}

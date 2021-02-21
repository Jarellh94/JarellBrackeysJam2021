using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGameTrigger : MonoBehaviour
{
    public MouseControl mouseControl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Individual>()){
            mouseControl.TriggerEndOfGame();
            Destroy(gameObject, 1f);
        }   
    }
}

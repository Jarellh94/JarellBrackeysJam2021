using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool isCurrentCheckpoint = false;
    GameManager manager;
    int individualSize;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerGotNewCheckpoint() {
        isCurrentCheckpoint = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(!isCurrentCheckpoint) {
            Individual newInd = other.GetComponent<Individual>();
            if(newInd){
                isCurrentCheckpoint = true;
                individualSize = newInd.mySize;
                manager.SetCurrentCheckpoint(this);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitWall : Interactable
{
    public int hits = 4;
    public GameObject debris;
    public List<GameObject> cracks;
    
    public AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();
        myAudio.Play();
        hits--;
        if(hits - 1 >= 0)
            cracks[hits-1].SetActive(true);

        if(hits <= 0) {
            Instantiate(debris);
            FindObjectOfType<GameTimer>().StopTimer();
            Destroy(gameObject);
        }
    }
}

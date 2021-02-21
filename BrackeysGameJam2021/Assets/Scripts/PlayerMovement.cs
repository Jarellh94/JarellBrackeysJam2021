using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody rig;
    Vector3 moveVector = Vector3.zero;

    Animator myAnimatior;

    public AudioSource walkAudio;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        myAnimatior = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        float xAxis = 0;
        float zAxis = 0;
        if(Input.GetKey(KeyCode.W)) {
            zAxis += 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(Input.GetKey(KeyCode.A)) {
            xAxis -= 1;
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if(Input.GetKey(KeyCode.S)) {
            zAxis -= 1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if(Input.GetKey(KeyCode.D)) {
            xAxis += 1;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if(xAxis != 0 || zAxis != 0){
            myAnimatior.SetBool("isRunning", true);
            if(moveVector == Vector3.zero) {
                walkAudio.Play();
            }
        }
        else {
            walkAudio.Stop();
            myAnimatior.SetBool("isRunning", false);
        }
        
        moveVector = new Vector3(xAxis, 0, zAxis).normalized;
        // float xAxis = Input.GetAxis("Horizontal");
        // float yAxis = Input.GetAxis("Vertical");
        // moveVector = new Vector3(xAxis, 0, yAxis).normalized * moveSpeed * Time.deltaTime;
    }

    private void FixedUpdate() {
        transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
        //moveVector = Vector3.zero;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public BoxCollider hitBox;
    public Individual me;

    Individual selectedInd;
    Carryable selectedObject;
    Interactable selectedInteraction;
    public GameManager manager;
    MouseControl mouseControl;

    public Transform carryPostion;

    public AudioSource absorbSound;
    public AudioSource evictSound;

    bool isCarrying = false;

    bool controlled = false;
    Animator myAnimator;
    Transform target;
    bool beingAbsorbed = false;
    bool beingEvicted = false;

    ExitWall wall;
    bool isPunching = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        myAnimator = GetComponent<Animator>();
        mouseControl = manager.GetComponent<MouseControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager == null) {
            manager = FindObjectOfType<GameManager>();
        }

        if(controlled && !isPunching) {
            if(Input.GetKeyDown(KeyCode.E)){
                if(isCarrying) {
                    Drop();
                }
                else if(selectedInd)
                    AbsorbIndividual();
                else if(!isCarrying && selectedObject) {
                    if(selectedObject.requiredSize <= me.mySize){
                        PickUp();
                    } else {
                    }
                } else if(selectedInteraction) {
                    if(wall){
                        myAnimator.SetTrigger("punch");
                    } else {
                        selectedInteraction.Interact();
                    }
                }

            }
            else if(Input.GetKeyDown(KeyCode.F)){
                EvictIndividual();
            }
        }

        if(isCarrying) {
            selectedObject.transform.position = carryPostion.position;
        }

    }

    private void FixedUpdate() {
        if(beingAbsorbed) {
            transform.LookAt(target, transform.up);
            transform.Translate(transform.forward * Time.deltaTime * 10f, Space.World);
        }
        else if(beingEvicted) {
            transform.Translate(transform.forward * Time.deltaTime * 10f, Space.World);
        }
    }

    void AbsorbIndividual() {
        selectedInd.GetComponent<PlayerActions>().Absorbed(transform);
        me.AddIndivduals(selectedInd.mySize);
        absorbSound.Play();

        transform.localScale = transform.localScale +  selectedInd.mySize * new Vector3(.25f, .25f, .25f);
        //Destroy(selectedInd.gameObject);
        selectedInd = null;
        //manager.interactText.SetActive(false);
    }

    void EvictIndividual() {
        if(me.mySize > 1){
            me.EvictIndividual();
            manager.SpawnNewInd(transform.position, transform.rotation);
            transform.localScale = transform.localScale - new Vector3(.25f, .25f, .25f);
            evictSound.Play();
        }
    }

    void PickUp() {
        isCarrying = true;
        myAnimator.SetBool("isCarrying", true);
        selectedObject.GetComponent<BoxCollider>().isTrigger = true;
        selectedObject.GetComponent<Rigidbody>().isKinematic = true;
        //manager.interactText.SetActive(false);
    }

    public void Drop() {
        selectedObject.GetComponent<BoxCollider>().isTrigger = false;
        selectedObject.GetComponent<Rigidbody>().isKinematic = false;
        isCarrying = false;
        myAnimator.SetBool("isCarrying", false);
    }

    public void TakeControl() {
        controlled = true;
    }

    public void LoseControl() {
        controlled = false;
        myAnimator.SetBool("isRunning", false);
        GetComponent<PlayerMovement>().walkAudio.Stop();
    }

    public void Absorbed(Transform absorber) { //For safely handling things ind was interacting with before beign absorbed
        myAnimator.SetBool("beingAbsorbed", true);
        GetComponent<CapsuleCollider>().isTrigger = true;
        beingAbsorbed = true;
        target = absorber;
        Destroy(gameObject, 0.15f);
        if(selectedInd){
            selectedInd.LostFocus();
            selectedInd = null;
        }

        if(selectedObject) {
            selectedObject.LostFocus();
            selectedObject = null;
        }
        if(selectedInteraction){
            selectedInteraction.LostFocus();
            selectedInteraction = null;
        }
    }

    public void Evicted() {
        beingEvicted = true;
        if(myAnimator == null) {
            myAnimator = GetComponent<Animator>();
        }
        myAnimator.SetBool("beingAbsorbed", true);
        GetComponent<CapsuleCollider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;

        StartCoroutine(DoneEvicting(0.15f));
    }

    IEnumerator DoneEvicting(float time) {
        yield return new WaitForSeconds(time);

        beingEvicted = false;
        myAnimator.SetBool("beingAbsorbed", false);
        GetComponent<CapsuleCollider>().isTrigger = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void PunchWall() {
        if(me.mySize >= 10) {
            wall.Interact();
            mouseControl.PunchedWall();
            isPunching = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(controlled) {
            Individual item = other.GetComponent<Individual>();
            Carryable carryItem = other.GetComponent<Carryable>();
            Interactable interactingObject = other.GetComponent<Interactable>();

            if(item) {
                selectedInd = item;
                selectedInd.Focus();
                //manager.interactText.SetActive(true);
            }
            else if(carryItem && !isCarrying) {
                selectedObject = carryItem;
                carryItem.Focus();
                carryItem.Pickup(this);
                //manager.interactText.SetActive(true);
            }
            else if(interactingObject) {
                if(other.GetComponent<ExitWall>()) {
                    wall = other.GetComponent<ExitWall>();
                }
                selectedInteraction = interactingObject;
                selectedInteraction.Focus();
                //manager.interactText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(controlled) {
            if(selectedInd){
                selectedInd.LostFocus();
            }
            Carryable carryItem = other.GetComponent<Carryable>();
            if(carryItem) {
                carryItem.LostFocus();
            }
            selectedInd = null;
            if(selectedInteraction){
                selectedInteraction.LostFocus();
                selectedInteraction = null;
            }
            if(!isCarrying) {
                selectedObject = null;
            }
            //manager.interactText.SetActive(false);
        }
    }
}

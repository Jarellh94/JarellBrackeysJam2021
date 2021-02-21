using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public Camera thisCamera;
    public GameObject pauseMenu;
    public GameObject cameraGraphic;

    public GameObject currentlyControlling;
    public float panSpeed = 5f;
    public float keyPanSpeed = 15f;

    Individual hovering;

    bool freeCam = false;
    bool rightMousePressed = false;

    Vector3 lookOffset;
    Vector3 moveVector;

    bool isPaused = true;
    bool inDialogue = false;
    public GameObject dialogueBox;

    public List<GameObject> openingDialogue;
    public List<GameObject> endingDialogue;
    public List<GameObject> punchDialogue;
        
    public GameObject endScreen;

    public List<Door> endGates;

    int currDialogue;

    bool isOpening = true;
    bool isEnding = false;
    bool isPunchingWall = false;

    GameTimer gameTimer;

    // Start is called before the first frame update
    void Start()
    {
        gameTimer = GetComponent<GameTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            if(isPaused)
                UnPause();
            else
                Pause();
        }

        if(!isPaused) {
            if(Input.GetMouseButtonDown(0)){
                Click();
            }
            else if(Input.GetKeyDown(KeyCode.R)){
                freeCam = !freeCam;
                if(freeCam){
                    DisableControl();
                } else {
                    ReEnableControl();
                }
            }
            else if(Input.GetMouseButtonDown(1)) {
                rightMousePressed = true;
                lookOffset = Input.mousePosition;
            }

            if(Input.GetMouseButtonUp(1)){
                rightMousePressed = false;
            }

            //Following Player
            if(!freeCam)
                thisCamera.transform.position = new Vector3(currentlyControlling.transform.position.x, 10, currentlyControlling.transform.position.z - 10);
            else if(rightMousePressed){
                Vector3 cameraDif = Input.mousePosition - lookOffset;
                thisCamera.transform.position -= (new Vector3(cameraDif.x, 0, cameraDif.y)) * Time.deltaTime * panSpeed;
                lookOffset = Input.mousePosition;
            } else {
                float xAxis = 0;
                float zAxis = 0;
                if(Input.GetKey(KeyCode.W)) {
                    zAxis += 1;
                }
                if(Input.GetKey(KeyCode.A)) {
                    xAxis -= 1;
                }
                if(Input.GetKey(KeyCode.S)) {
                    zAxis -= 1;
                }
                if(Input.GetKey(KeyCode.D)) {
                    xAxis += 1;
                }
                moveVector = new Vector3(xAxis, 0, zAxis).normalized;
            }
        } else if(isPaused && inDialogue) {
            if(Input.GetMouseButtonDown(0)){
                Click();
            }
        }

        if(hovering != null){
            hovering.LostFocus();
            hovering = null;
        }

        RaycastHit hit;
        Ray ray = thisCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit)) {
            hovering = hit.transform.GetComponent<Individual>();
            if(hovering) {
                hit.transform.GetComponent<Individual>().Focus();
            }
        }
    }

    private void FixedUpdate() {
        Transform cameraTransform = thisCamera.transform;
        cameraTransform.Translate(moveVector * keyPanSpeed * Time.deltaTime, Space.World);
        if(cameraTransform.position.x > 12) {
            cameraTransform.SetPositionAndRotation(new Vector3(12, cameraTransform.position.y, cameraTransform.position.z), cameraTransform.rotation);
        }
        if(cameraTransform.position.x < -12) {
            cameraTransform.SetPositionAndRotation(new Vector3(-12, cameraTransform.position.y, cameraTransform.position.z), cameraTransform.rotation);
        }
        if(cameraTransform.position.z < -12) {
            cameraTransform.SetPositionAndRotation(new Vector3(cameraTransform.position.x, cameraTransform.position.y, -12), cameraTransform.rotation);
        }
    }

    void Click() {
        if(!inDialogue && !isPaused) {
            RaycastHit hit;
            Ray ray = thisCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;
                // Do something with the object that was hit by the raycast.
                if(objectHit.GetComponent<Individual>()) {
                    currentlyControlling.GetComponent<PlayerActions>().LoseControl();
                    currentlyControlling.GetComponent<PlayerMovement>().enabled = false;

                    currentlyControlling = objectHit.gameObject;
                    currentlyControlling.GetComponent<PlayerActions>().TakeControl();
                    currentlyControlling.GetComponent<PlayerMovement>().enabled = true;
                    freeCam = false;
                    cameraGraphic.SetActive(false);
                }

            }
        } else if(inDialogue){
            if(isOpening) {
                openingDialogue[currDialogue].SetActive(false);
                currDialogue++;
                if(currDialogue< openingDialogue.Count) {
                    openingDialogue[currDialogue].SetActive(true);
                }
                else if(currDialogue >= openingDialogue.Count) {
                    currDialogue = 0;
                    isOpening = false;
                    dialogueBox.SetActive(false);
                    UnPause();
                    gameTimer.StartTimer();
                }
            }
            if(isEnding) {
                endingDialogue[currDialogue].SetActive(false);
                currDialogue++;
                if(endingDialogue.Count > currDialogue) {
                    endingDialogue[currDialogue].SetActive(true);
                }
                else if(currDialogue >= endingDialogue.Count) {
                    currDialogue = 0;
                    isEnding = false;
                    dialogueBox.SetActive(false);
                    foreach(Door dev in endGates) {
                        dev.Activate();
                    }
                    UnPause();
                }
            }
            if(isPunchingWall) {
                punchDialogue[currDialogue].SetActive(false);
                dialogueBox.SetActive(false);
                currDialogue++;
                UnPause();
            }
        }
    }

    public void SetCurrentlyControlling(GameObject ind) {
        currentlyControlling = ind;
        cameraGraphic.SetActive(false);
        if(!isPaused){
            currentlyControlling.GetComponent<PlayerActions>().TakeControl();
            currentlyControlling.GetComponent<PlayerMovement>().enabled = true;
        }
    }
    public void DisableControl() {
        currentlyControlling.GetComponent<PlayerActions>().LoseControl();
        currentlyControlling.GetComponent<PlayerMovement>().enabled = false;
        cameraGraphic.SetActive(true);
    }

    public void ReEnableControl() {
        currentlyControlling.GetComponent<PlayerActions>().TakeControl();
        currentlyControlling.GetComponent<PlayerMovement>().enabled = true;
        cameraGraphic.SetActive(false);
    }
    
    public void Pause() {
            DisableControl();
            cameraGraphic.SetActive(false);
            isPaused = true;
        if(!inDialogue) {
            pauseMenu.SetActive(true);
        }
    }

    public void UnPause() {
        if(!isOpening) {
            ReEnableControl();
            isPaused = false;
            inDialogue = false;
        }
        else {
            inDialogue = true;
            openingDialogue[currDialogue].SetActive(true);
            dialogueBox.SetActive(true);
        }
        pauseMenu.SetActive(false);
    }

    public void TriggerEnding() {
        isEnding = true;
        inDialogue = true;
        currDialogue = 0;
        Pause();
        dialogueBox.SetActive(true);
        endingDialogue[currDialogue].SetActive(true);

    }

    public void PunchedWall() {
        isPunchingWall = true;
        inDialogue = true;
        if(currDialogue < punchDialogue.Count){
            punchDialogue[currDialogue].SetActive(true);
            dialogueBox.SetActive(true);
            Pause();
        }
    }

    public void TriggerEndOfGame() {
        DisableControl();
        cameraGraphic.SetActive(false);
        isPaused = true;
        endScreen.SetActive(true);

    }
}

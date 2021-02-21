using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject interactText;
    public GameObject individualPrefab;
    
    Checkpoint currentCheckpoint;
    MouseControl mouseControl;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newInd = Instantiate(individualPrefab, transform.position + transform.forward * 2, Quaternion.identity);
        mouseControl = GetComponent<MouseControl>();
        mouseControl.SetCurrentlyControlling(newInd);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnNewInd(Vector3 position, Quaternion playerRotation) {
        GameObject newInd = Instantiate(individualPrefab, position, playerRotation);
        newInd.GetComponent<PlayerActions>().Evicted();
    }

    public void SetCurrentCheckpoint(Checkpoint newCheck) {
        currentCheckpoint = newCheck;
    }

    public void RespawnPlayer() {
        mouseControl.currentlyControlling.transform.position = currentCheckpoint.transform.position;
    }
}

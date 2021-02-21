using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CumbleWall : MonoBehaviour
{
    public float crumbleForce = 100f;
    public List<Rigidbody> wallPieces;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Rigidbody rig in wallPieces) {
            rig.AddForce(Vector3.forward * crumbleForce);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

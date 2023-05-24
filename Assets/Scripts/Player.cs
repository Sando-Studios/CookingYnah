using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public float force;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            var val = Input.GetAxis("Horizontal");
            rb.AddForce(new Vector3(val, 0, 0) * force, ForceMode.Force);
        }
        
        if (Input.GetButton("Vertical"))
        {
            var val = Input.GetAxis("Vertical");
            rb.AddForce(new Vector3(0, 0, val) * force, ForceMode.Force);
        }
    }
}

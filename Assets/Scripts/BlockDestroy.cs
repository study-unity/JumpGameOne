using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    Destroy the ground beyond the camera's field of view
     */
    void  OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

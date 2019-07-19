using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassed : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject block;
    float width=0.9f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = GameObject.Instantiate (block, transform.position + new Vector3 (width*50, 0, 0), new Quaternion (0, 0, 0, 0));
    }
}

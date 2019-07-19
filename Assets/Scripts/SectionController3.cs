using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionController3 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject block;
    float width=0.9f;
    void Start()
    {
        GetBlock();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetBlock()
    {
        for(int i=0;i<50;i++)
        {
            GameObject obj = GameObject.Instantiate (block, transform.position + new Vector3 (i*width, 0, 0), new Quaternion (0, 0, 0, 0));
        }
    }
}

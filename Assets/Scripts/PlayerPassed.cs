using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassed : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject block;
    private GameObject section;
    float width = 0.9f;
    int index;
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {

    }

    private void OnTriggerEnter2D (Collider2D other) {
        int nextBlock = index + 50;
        Vector3 p = transform.position + new Vector3 (width * 50, 0, 0);
        GameObject obj = GameObject.Instantiate (block, p, new Quaternion (0, 0, 0, 0));
        PlayerPassed next = obj.GetComponent<PlayerPassed> ();
        next.setIndex (nextBlock);
        next.setSection (section);
        SectionController3 sectionController = section.GetComponent<SectionController3> ();
        if (nextBlock == sectionController.nextObstacle) {
            int r = Random.Range (0, 4);
            if (r==0) {
                sectionController.SetStut (p);
            } else
                sectionController.SetObstacle (p);
            sectionController.getNext ();
        }

    }

    public void setIndex (int index) {
        this.index = index;
    }

    public int getIndex () {
        return index;
    }

    public void setSection (GameObject section) {
        this.section = section;
    }
}
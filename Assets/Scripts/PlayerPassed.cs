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

    /*
     * 在每个砖块上添加一个触发器，当player经过时，则会触发，生成新的砖块，新的砖块的位置为被触发的砖块位置加50
     */
    private void OnTriggerEnter2D (Collider2D other) {
        int nextBlock = index + 50;
        Vector3 p = transform.position + new Vector3 (width * 50, 0, 0);
        GameObject obj = GameObject.Instantiate (block, p, new Quaternion (0, 0, 0, 0));
        PlayerPassed next = obj.GetComponent<PlayerPassed> ();
        next.setIndex (nextBlock);
        next.setSection (section);
        SectionController3 sectionController = section.GetComponent<SectionController3> ();
        //当新生成的砖块位置等于section中下一关障碍的位置时，在新的砖块上放置障碍或道具
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
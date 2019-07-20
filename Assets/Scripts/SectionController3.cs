using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionController3 : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject block;
    public int nextObstacle;
    float width = 0.9f;
    public GameObject[] obstacles;


    void Start () {
        nextObstacle = 0;
        getNext();
        GetBlock ();
    }

    // Update is called once per frame
    void Update () {

    }

    void GetBlock () {
        for (int i = 0; i < 50; i++) {
            Vector3 p = transform.position + new Vector3 (i * width, 0, 0);
            GameObject obj = GameObject.Instantiate (block, p, new Quaternion (0, 0, 0, 0));
            PlayerPassed playerpass = obj.GetComponent<PlayerPassed>();
            playerpass.setIndex(i);
            playerpass.setSection(gameObject);
            if (i == nextObstacle) {
                SetObstacle(p);
                getNext();
            }
            
        }
    }

    public void getNext() {
        nextObstacle=nextObstacle+Random.Range (30, 50);
    }

    public void SetObstacle(Vector3 p) {
        int height = Random.Range (0, 3);
        float baseHeight = 1.24f;
        float obsHeight = 1.4f;
        GameObject obj;
        if (height == 0) {
            Vector3 position = p + new Vector3 (0, baseHeight + obsHeight, 0);
            obj = GameObject.Instantiate (obstacles[0], position, new Quaternion (0, 0, 0, 0));
        } else if (height == 1) {
            Vector3 position = p + new Vector3 (0, baseHeight + 0.4f, 0);
            obj = GameObject.Instantiate (obstacles[1], position, new Quaternion (0, 0, 0, 0));

        } else {
            Vector3 position = p + new Vector3 (0, baseHeight, 0);
            obj = GameObject.Instantiate (obstacles[2], position, new Quaternion (0, 0, 0, 0));
        }
        obj.transform.parent = transform;
    }
}
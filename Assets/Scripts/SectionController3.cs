using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionController3 : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject block;
    public int nextObstacle;
    private int preObstacle;
    float width = 0.9f;
    public GameObject[] obstacles;
    public GameObject[] stuts;

    void Start () {
        nextObstacle = 0;
        getNext ();
        GetBlock ();
    }

    // Update is called once per frame
    void Update () {

    }

    void GetBlock () {
        for (int i = 0; i < 50; i++) {
            Vector3 p = transform.position + new Vector3 (i * width, 0, 0);
            GameObject obj = GameObject.Instantiate (block, p, new Quaternion (0, 0, 0, 0));
            PlayerPassed playerpass = obj.GetComponent<PlayerPassed> ();
            playerpass.setIndex (i);
            playerpass.setSection (gameObject);
            if (i == nextObstacle) {
                int r = Random.Range (0, 4);
                if (r == 0) {
                    SetStut (p);
                } else
                    SetObstacle (p);
                getNext ();
            }
        }
    }

    public void getNext () {
        preObstacle = nextObstacle;
        nextObstacle = preObstacle + Random.Range (30, 50);
    }

    public void SetObstacle (Vector3 p) {
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

    public void SetStut (Vector3 p) {
        float height;
        int i = Random.Range (0, 3);
        int j = Random.Range (0, 3);
        switch (j) {
            case 0:
                height = 1.2f;
                break;
            case 1:
                height = 4f;
                break;
            case 2:
                height = 5.5f;
                break;
            default:
                height = 1.2f;
                break;
        }
        Vector3 position = p + new Vector3 (0, height, 0);
        GameObject obj = GameObject.Instantiate (stuts[i], position, new Quaternion (0, 0, 0, 0));
        obj.transform.parent = transform;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    // Start is called before the first frame update
    public int level;

    void Start () {

    }

    // Update is called once per frame
    void Update () {
        int speed;
        switch (level) {
            case 1:
                speed = 10;
                break;
            case 2:
                speed = 15;
                break;
            default:
                speed = 15;
                break;
        }
        transform.Translate (new Vector2 (speed, 0) * Time.deltaTime);
    }
}
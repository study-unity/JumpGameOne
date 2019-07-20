using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    // Start is called before the first frame update
    public int level;
    private float speed;

    void Start () {
        switch (level) {
            case 1:
                speed = 10;
                break;
            case 2:
                speed = 15;
                break;
            case 3:
                speed= 10;
                break;
            default:
                speed = 15;
                break;
        }
    }

    // Update is called once per frame
    void Update () {
        if(level==3&&speed<25){
            speed=speed+0.1f* Time.deltaTime;
        }
        transform.Translate (new Vector2 (speed, 0) * Time.deltaTime);
    }
}
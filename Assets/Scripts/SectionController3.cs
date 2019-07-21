using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionController3 : MonoBehaviour {
    // Start is called before the first frame update
    
    //生成地面时的砖块
    public GameObject block;

    //下一关生成的障碍的位置
    public int nextObstacle;

    //当前生成障碍的位置
    private int preObstacle;

    //地面的宽度
    float width = 0.9f;

    //不同的障碍
    public GameObject[] obstacles;

    //不同的道具
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
        /*
         * 在游戏开始时，先生成50个砖块组成地面，并根据nextObstacle的位置来确定障碍生成的位置
         */
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

    /*
     * 获取下一关障碍(或道具)的位置
     */
    public void getNext () {
        preObstacle = nextObstacle;
        nextObstacle = preObstacle + Random.Range (30, 50);
    }

    /*
     * 在给定位置随机生成不同的障碍
     */
    public void SetObstacle (Vector3 p) {
        //障碍有3种，高度不同
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

    /*
     * 在给定位置随机生成不同的道具
     */
    public void SetStut (Vector3 p) {
        float height;
        //有3种道具
        int i = Random.Range (0, 3);
        //道具的高度有3种
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
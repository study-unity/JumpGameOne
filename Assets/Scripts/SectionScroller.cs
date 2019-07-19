using UnityEngine;

/*
 * Attached to the section so that everything will scroll sideways.
 * The player does not move in this game, the environment does.
 */
public class SectionScroller : MonoBehaviour {
    public GameObject[] stuts;
    public GameObject[] obstacles;
    public GameObject block;
    public GameObject flag;
    public int level;
    int distance = 20;
    int length=400;

    /*
     * Use the Transform component attached to the section game object and
     * translate it based on delta time.
     */
    private void Update () {

    }
    private void Awake () {
        GetGround ();
        GetObstacles ();
        GetFlag ();
    }
    private void Start () {

    }

    private void GetGround () {
        float l=0.0f;
        do {
            GameObject obj = GameObject.Instantiate (block, transform.position + new Vector3 (l, 0, 0), new Quaternion (0, 0, 0, 0));
            obj.transform.parent = transform;
            l+=0.8f;
        }while(l<length+40);
    }

    private void GetObstacles () {
        int l = Random.Range (25, 30);
        float baseHeight = 1.24f;
        float obsHeight = 1.4f;
        do {
            int height = Random.Range (0, 3);
            GameObject obj;
            if (height == 0) {
                Vector3 position = transform.position + new Vector3 (l, baseHeight + obsHeight, 0);
                obj = GameObject.Instantiate (obstacles[0], position, new Quaternion (0, 0, 0, 0));
            } else if (height == 1) {
                Vector3 position = transform.position + new Vector3 (l, baseHeight + 0.4f, 0);
                obj = GameObject.Instantiate (obstacles[1], position, new Quaternion (0, 0, 0, 0));

            } else {
                Vector3 position = transform.position + new Vector3 (l, baseHeight, 0);
                obj = GameObject.Instantiate (obstacles[2], position, new Quaternion (0, 0, 0, 0));
            }
            obj.transform.parent = transform;
            if (level == 1) {
                GetStuts(l+2,l+distance);
            }
            l += Random.Range (distance, distance + 10);
        } while (l <= length);
    }

    private void GetFlag () {
        GameObject obj = GameObject.Instantiate (flag, transform.position + new Vector3 (length+20, 2, 0), new Quaternion (0, 0, 0, 0));
        obj.transform.parent = transform;
    }

    private void GetStuts (int start, int end) {
        int probable = Random.Range(0,3);
        if(probable==0){
            int i = Random.Range(0,2);
            int p=Random.Range(0,end-start);
            Vector3 position = transform.position + new Vector3 (start+p-100, 1.2f, 0);
            GameObject obj = GameObject.Instantiate (stuts[i], position, new Quaternion (0, 0, 0, 0));
            obj.transform.parent = transform;
        }
    }
}
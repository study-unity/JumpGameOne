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
        for (int i = 0; i < 300; i++) {
            GameObject obj = GameObject.Instantiate (block, transform.position + new Vector3 (0.8f * i - 100, 0, 0), new Quaternion (0, 0, 0, 0));
            obj.transform.parent = transform;
        }
    }

    private void GetObstacles () {
        int distance=15;
        int l = Random.Range (25, 30);
        float baseHeight = 1.24f;
        float obsHeight = 1.4f;
        do {
            int height = Random.Range (0, 3);
            GameObject obj;
            if (height == 0) {
                Vector3 position = transform.position + new Vector3 (l - 100, baseHeight + obsHeight, 0);
                obj = GameObject.Instantiate (obstacles[0], position, new Quaternion (0, 0, 0, 0));
            } else if (height == 1) {
                Vector3 position = transform.position + new Vector3 (l - 100, baseHeight + 0.4f, 0);
                obj = GameObject.Instantiate (obstacles[1], position, new Quaternion (0, 0, 0, 0));

            } else {
                Vector3 position = transform.position + new Vector3 (l - 100, baseHeight, 0);
                obj = GameObject.Instantiate (obstacles[2], position, new Quaternion (0, 0, 0, 0));
            }
            obj.transform.parent = transform;
            l += Random.Range (distance, distance + 10);
        } while (l <= 200);
    }

    private void GetFlag () {
        GameObject obj = GameObject.Instantiate (flag, transform.position + new Vector3 (120, 2, 0), new Quaternion (0, 0, 0, 0));
        obj.transform.parent = transform;
    }

}
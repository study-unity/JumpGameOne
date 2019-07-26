using UnityEngine;

// Generate and manage positions of game objects on or in the ground for chapter 1 & 2.
public class SectionScroller : MonoBehaviour {

    // game props
    public GameObject[] stuts;

    // obstacles
    public GameObject[] obstacles;

    // ground block
    public GameObject block;

    // destination flag
    public GameObject flag;

    // game level(1 or 2)
    public int level;

    // distance between two obstacles
    int distance = 20;
    
    // ground length
    int length=600;

    private void Awake()
    {
        // Generate game scene of chapter 1 & 2 when the game awakes.
        // Chapter 3 is endless, so it's game scene is generated real-time.
        GetGround();
        GetObstacles();
        GetFlag();
    }

    // Generate ground.
    private void GetGround()
    {
        float l=0.0f;
        do{
            GameObject obj = GameObject.Instantiate(block, transform.position + new Vector3(l, 0, 0), new Quaternion(0, 0, 0, 0));
            obj.transform.parent = transform;
            l+=0.8f;
        }while(l<length+40);
    }

    // Generate obstacles.
    private void GetObstacles()
    {
        int l = Random.Range (25, 30);
        float baseHeight = 1.24f;
        float obsHeight = 1.4f;
        
        do{
            // obstacles' heights
            int height = Random.Range (0, 3);
            GameObject obj;
            if(height == 0)
            {
                // obstacle needs to use normal jump
                Vector3 position = transform.position + new Vector3 (l, baseHeight + obsHeight, 0);
                obj = GameObject.Instantiate (obstacles[0], position, new Quaternion (0, 0, 0, 0));
            }
            else if(height == 1)
            {
                // obstacle needs to use crouch
                Vector3 position = transform.position + new Vector3 (l, baseHeight + 0.4f, 0);
                obj = GameObject.Instantiate (obstacles[1], position, new Quaternion (0, 0, 0, 0));
            }
            else
            {
                // obstacle needs to use big jump
                Vector3 position = transform.position + new Vector3 (l, baseHeight, 0);
                obj = GameObject.Instantiate (obstacles[2], position, new Quaternion (0, 0, 0, 0));
            }
            obj.transform.parent = transform;
            
            // Generate game props in chapter 2.
            if (level == 2)
                GetStuts(l+2,l+distance);
            l += Random.Range(distance, distance + 10);
        }while(l <= length);
    }


    // Set destination flag on the ground.
    private void GetFlag()
    {
        GameObject obj = GameObject.Instantiate(flag, transform.position + new Vector3(length+20, 2, 0), new Quaternion(0, 0, 0, 0));
        obj.transform.parent = transform;
    }

    // Set game props in a range.
    private void GetStuts(int start, int end)
    {
        // The probability of generating game props is 1/4.
        int probable = Random.Range(0,4);
        if(probable == 0)
        {
            int i = Random.Range(0,3);
            int p = Random.Range(0,end-start);
            Vector3 position = transform.position + new Vector3(start+p-100, 1.4f, 0);
            GameObject obj = GameObject.Instantiate(stuts[i], position, new Quaternion(0, 0, 0, 0));
            obj.transform.parent = transform;
        }
    }
}
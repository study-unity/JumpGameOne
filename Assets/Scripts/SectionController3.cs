using UnityEngine;

// Generate and manage positions of game objects on or in the ground for chapter 3.
// Chapter 3 need to generate game objects persistently, so we write a unique script for it.
public class SectionController3 : MonoBehaviour {
    
    // ground block
    public GameObject block;

    // next obstacle position
    public int nextObstacle;

    // current obstacle position
    private int preObstacle;

    // width of ground block
    float width = 0.9f;

    // All kinds of obstacles
    public GameObject[] obstacles;

    // All kinds of game props
    public GameObject[] stuts;

    void Start()
    {
        nextObstacle = 0;
        getNext();
        GetBlock();
    }

    // Generate 50 blocks when the game begins, and get the position of obstacles by 'nextObstacle'.
    void GetBlock()
    {
        for (int i = 0; i < 50; i++)
        {
            Vector3 p = transform.position + new Vector3(i * width, 0, 0);
            GameObject obj = GameObject.Instantiate(block, p, new Quaternion(0, 0, 0, 0));
            PlayerPassed playerpass = obj.GetComponent<PlayerPassed>();
            playerpass.setIndex(i);
            playerpass.setSection(gameObject);
            if (i == nextObstacle)
            {
                int r = Random.Range(0, 4);
                if(r == 0)
                    SetStut(p);
                else
                    SetObstacle(p);
                getNext();
            }
        }
    }

    // Get next position of obstacles or game props.
    public void getNext()
    {
        preObstacle = nextObstacle;
        nextObstacle = preObstacle + Random.Range(30, 50);
    }

    // Set different obstacle at a location.
    public void SetObstacle(Vector3 p)
    {
        // Three kinds of obstacles.
        int height = Random.Range (0, 3);
        float baseHeight = 1.24f;
        float obsHeight = 1.4f;
        GameObject obj;
        if (height == 0)
        {
            Vector3 position = p + new Vector3 (0, baseHeight + obsHeight, 0);
            obj = GameObject.Instantiate(obstacles[0], position, new Quaternion(0, 0, 0, 0));
        }
        else if(height == 1)
        {
            Vector3 position = p + new Vector3 (0, baseHeight + 0.4f, 0);
            obj = GameObject.Instantiate(obstacles[1], position, new Quaternion(0, 0, 0, 0));
        }
        else
        {
            Vector3 position = p + new Vector3 (0, baseHeight, 0);
            obj = GameObject.Instantiate(obstacles[2], position, new Quaternion(0, 0, 0, 0));
        }
        obj.transform.parent = transform;
    }

    // Set different game props at a location.
    public void SetStut(Vector3 p)
    {
        // Three kinds of game props.
        float height;
        int i = Random.Range(0, 3);
        int j = Random.Range(0, 3);
        switch(j)
        {
            case 0:
                height = 1.4f;
                break;
            case 1:
                height = 4f;
                break;
            case 2:
                height = 5.5f;
                break;
            default:
                height = 1.4f;
                break;
        }
        Vector3 position = p + new Vector3(0, height, 0);
        GameObject obj = GameObject.Instantiate(stuts[i], position, new Quaternion(0, 0, 0, 0));
        obj.transform.parent = transform;
    }
}
using UnityEngine;

/*
 * Attached to the section so that everything will scroll sideways.
 * The player does not move in this game, the environment does.
 */
public class SectionScroller : MonoBehaviour
{
    public GameObject[] stuts;
    public GameObject[] obstacles;
    public int level;

  /*
   * Use the Transform component attached to the section game object and
   * translate it based on delta time.
   */
  private void Update()
  {
    transform.Translate(new Vector2(-10, 0) * Time.deltaTime);
  }
  private void Awake()
  {
      int distance;
      switch(level)
      {
        case 1:
            distance=20;
            break;
        case 2:
            distance=15;
            break;
        default:
            distance=15;
            break;
      }
      int l=Random.Range(25,30);
      float baseHeight=1.24f;
      float obsHeight=1.4f;
      do{
            int height=Random.Range(0,3);
            GameObject obj;
            if(height==0){
                Vector3 position=transform.position+new Vector3(l-100,baseHeight+obsHeight,0);
                obj = GameObject.Instantiate(obstacles[0],position,new Quaternion(0, 0, 0, 0));
            }
            else if(height==1){
                Vector3 position=transform.position+new Vector3(l-100,baseHeight+0.5f,0);
                obj = GameObject.Instantiate(obstacles[1],position,new Quaternion(0, 0, 0, 0)); 
                
            }
            else{
                Vector3 position=transform.position+new Vector3(l-100,baseHeight,0);
                obj = GameObject.Instantiate(obstacles[2],position,new Quaternion(0, 0, 0, 0)); 
            }
            obj.transform.parent=transform;
            l+=Random.Range(distance,distance+10);
        }while(l<=200);
  }
  private void Start()
  {
      
  }
}

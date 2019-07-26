using UnityEngine;

public class PlayerPassed : MonoBehaviour {

    // Ground block
    public GameObject block;

    // Section is the ground which the block belongs to.
    private GameObject section;

    // width of ground block
    float width = 0.9f;

    // block position
    int index;

    // Generate a new block when the player passes a block.
    private void OnTriggerEnter2D(Collider2D other)
    {
        int nextBlock = index + 50;
        Vector3 p = transform.position + new Vector3(width * 50, 0, 0);
        GameObject obj = GameObject.Instantiate(block, p, new Quaternion(0, 0, 0, 0));
        PlayerPassed next = obj.GetComponent<PlayerPassed>();
        next.setIndex(nextBlock);
        next.setSection(section);
        SectionController3 sectionController = section.GetComponent<SectionController3>();
        
        // Set game props or obstacles on the block if the sectionController says that there is an object on the block.
        if(nextBlock == sectionController.nextObstacle)
        {
            int r = Random.Range(0, 4);
            if(r==0)
                sectionController.SetStut(p);
            else
                sectionController.SetObstacle(p);
            sectionController.getNext();
        }
    }

    public void setIndex(int index) => this.index = index;

    public int getIndex() => index;

    public void setSection(GameObject section) => this.section = section;
}
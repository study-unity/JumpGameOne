using UnityEngine;

// Control the motivation of the player, main camera and background image.
public class Move : MonoBehaviour {

    public int level;
    private float speed;
    public float GetSpeed () => speed;

    // Whether or not a decelerator is used.
    private bool slowDown;
    private float slowTime;

    // Remember origin speed.
    private float realSpeed;

    void Start () {
        slowDown = false;
        
        // Initialize the speed.
        switch (level) {
            case 1:
                speed = 10;
                break;
            case 2:
                speed = 15;
                break;
            case 3:
                speed = 10;
                break;
            default:
                speed = 15;
                break;
        }
    }

    void Update ()
    {
        if (!slowDown)
        {
            // Increase the speed in chapter 3.
            if (level == 3 && speed < 25)
                speed = speed + 0.1f * Time.deltaTime;
        }
        else
        {
            // Restore the origin speed after a use of decelerator.
            if(Time.time>=slowTime+3)
            {
                slowDown=false;
                speed=realSpeed;
            }
        }

        // Move game objects.
        transform.Translate (new Vector2 (speed, 0) * Time.deltaTime);
    }


    // Decrease the speed.
    public void SlowDown()
    {
        if(!slowDown)
        {
            slowDown = true;
            realSpeed=speed;
            speed = speed / 1.5f;
        }
        slowTime = Time.time;
    }
}
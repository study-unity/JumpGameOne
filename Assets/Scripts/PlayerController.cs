using UnityEngine;
using UnityEngine.SceneManagement;

// Behaviour to handle keyboard input and also store the player's current health and other messages.
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    // HP
    private int health;
    
    private bool canJump;

    // Shield number
    private int protectCount;

    // Decelerator number
    private int clockCount;

    // Whether or not the player is protected by a shield.
    bool protect;

    // Time when a shield will lose efficacy.
    float EndTime;

    // Shield image that is the player's child object.
    Transform childShield;

    // An score object to transmit score between scenes.
    public GameObject score;

    // Hit audio
    private AudioSource hitAudio;

    /*
     * Apply initial basical messages of a player and also store the Rigidbody2D reference for
     * future because GetComponent<T> is relatively expensive.
     */
    private void Start()
    {
        health = 6;
        rigidbody2d = GetComponent<Rigidbody2D>();
        hitAudio = GetComponent<AudioSource>();
        protectCount = 0;
        protect = false;
        childShield = transform.GetChild(1);
        childShield.gameObject.SetActive(false);
    }

    /*
     * Remove one health unit from player and if health becomes 0, change
     * scene to the end game scene.
     */
    public void Damage()
    {
        if (!protect)
        {
            // Player doesn't have a shield, so he will lose one point HP.
            health -= 1;
            if (health < 1)
            {
                // Stop the game and send score message to the end scene.
                Instantiate(score).GetComponent<ScoreController>().Score = GetComponent<PlayerHud>().Score;
                SceneManager.LoadScene("EndGame");
            }
        }
        else
        {
            // Player has a shield to protect himself.
            protect = false;
            childShield.gameObject.SetActive(false);
        }

    }

    // Accessor for health variable, used by he HUD to display health.
    public int GetHealth() => health;

    private void Update()
    {
        // Update stats of jump and shield.
        UpdateJump();
        UpdateProtect();
    }

    private void UpdateJump()
    {
        // Control the jump operation.
        if (canJump == true && transform.position[1] < -3)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Normal jump
                rigidbody2d.AddForce(new Vector2(0, 500));
                canJump = false;
            }
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                // Big jump
                rigidbody2d.AddForce(new Vector2(0, 600));
                canJump = false;
            }
        }

        // Control the crouch operation.
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.localScale = new Vector3(1f, 0.6f, 0.5f);
            canJump = false;
        }

        // Restore player's state when people loosen the crouch key.
        if (Input.GetKeyUp(KeyCode.DownArrow))
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Use a shield.
        if (protectCount > 0) {
            if (Input.GetKeyDown(KeyCode.A))
            {
                protectCount--;
                ProtectPlayer();
            }
        }

        // Use a decelerator.
        if (clockCount > 0) {
            if (Input.GetKeyDown(KeyCode.S))
            {
                clockCount--;
                transform.parent.gameObject.GetComponent<Move>().SlowDown();
            }
        }
    }

    /*
     * If the player has collided with the ground, set the canJump flag so that
     * the player can trigger another jump.
     */
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag ("block"))
            canJump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals ("success(Clone)"))
        {
            // Player reach the flag in the destination of one scene.
            string sceneName = SceneManager.GetActiveScene().name;

            // Change scene.
            if (sceneName.Equals("Game"))
                SceneManager.LoadScene("Game2");
            else if (sceneName.Equals("Game2"))
                SceneManager.LoadScene("Game3");
        }
        else if(collision.gameObject.CompareTag("drug"))
        {
            // Player obtain a medical kit.
            Destroy(collision.gameObject);

            // Add 1 point HP.
            if (health < 6) health++;
        }
        else if(collision.gameObject.CompareTag("shield"))
        {
            // Player obtain a shield.
            Destroy(collision.gameObject);

            // Add 1 shield.
            if (protectCount < 3) protectCount++;
        }
        else if(collision.gameObject.CompareTag("clock"))
        {
            // Player obtain a decelerator.
            Destroy(collision.gameObject);

            // Add 1 decelerator.
            if (clockCount < 3) clockCount++;
        }
    }

    // Protect the player for 3 seconds.
    private void ProtectPlayer()
    {
        protect = true;
        EndTime = Time.time + 3;

        // Show the shield image.
        childShield.gameObject.SetActive(true);
    }

    // Check whether or not the shield which is being used is still effective. 
    private void UpdateProtect()
    {
        float currentTime = Time.time;
        if(currentTime > EndTime)
        {
            protect = false;

            // Hide the shield image.
            childShield.gameObject.SetActive (false);
        }
    }

    // Get the number of shields.
    public int GetShieldNum() => protectCount;

    // Get the number of decelerators.
    public int GetClockNum() => clockCount;

    // Play the hit audio.
    public void Hit() => hitAudio.Play();
}

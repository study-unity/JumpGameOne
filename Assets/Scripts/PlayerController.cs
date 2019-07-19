using UnityEngine;
using System;

/*
 * Behaviour to handle keyboard input and also store the player's
 * current health.
 */
public class PlayerController : MonoBehaviour {
    private Rigidbody2D rigidbody2d;
    private int health;
    private bool canJump;
    private bool squat = false;
    public SectionScroller section;
    bool protect;
    float EndTime;
    /*
     * Apply initial health and also store the Rigidbody2D reference for
     * future because GetComponent<T> is relatively expensive.
     */
    private void Start () {
        health = 6;
        rigidbody2d = GetComponent<Rigidbody2D> ();
        protect = false;
    }

    /*
     * Remove one health unit from player and if health becomes 0, change
     * scene to the end game scene.
     */
    public void Damage () {
        if (!protect) {
            health -= 1;

            if (health < 1) {
                Application.LoadLevel ("EndGame");
            }
        }
        else{
            protect=false;
        }

    }

    /*
     * Accessor for health variable, used by he HUD to display health.
     */
    public int GetHealth () {
        return health;
    }

    /*
     * Poll keyboard for when the up arrow is pressed. If the player can jump
     * (is on the ground) then add force to the cached Rigidbody2D component.
     * Finally unset the canJump flag so the player has to wait to land before
     * another jump can be triggered.
     */
    private void Update () {
        UpdateJump ();
        UpdateProtect ();
    }

    private void UpdateJump () {
        if (canJump == true) {
            if (Input.GetKeyDown (KeyCode.UpArrow)) {
                Debug.Log ("UP");
                rigidbody2d.AddForce (new Vector2 (0, 500));
                canJump = false;
            } else if (Input.GetKeyDown (KeyCode.Space)) {
                rigidbody2d.AddForce (new Vector2 (0, 600));
                canJump = false;
            } else if (Input.GetKeyDown (KeyCode.DownArrow)) {
                transform.localScale = new Vector3 (1f, 0.6f, 0.5f);
                squat = true;
            }
        }
        if (Input.GetKeyUp (KeyCode.DownArrow)) {
            transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
            squat = false;
        }
    }
    /*
     * If the player has collided with the ground, set the canJump flag so that
     * the player can trigger another jump.
     */
    private void OnCollisionEnter2D (Collision2D other) {
        if (!squat && other.gameObject.CompareTag ("block"))
            canJump = true;
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.name.Equals ("success(Clone)")) {
            Application.LoadLevel ("PassGame");
        } else if (collision.gameObject.CompareTag ("drug")) {
            if (health < 6) {
                health += 1;
                Destroy (collision.gameObject);
            }
        } else if (collision.gameObject.CompareTag ("shield")) {
            Destroy (collision.gameObject);
            ProtectPlayer ();

        }
    }

    private void ProtectPlayer () {
        protect = true;
        EndTime = (float)(DateTime.UtcNow.Subtract (new DateTime (1970, 1, 1)).TotalMilliseconds) / 1000.0f + 5;
    }

    private void UpdateProtect () {
        float currentTime = (float)(DateTime.UtcNow.Subtract (new DateTime (1970, 1, 1)).TotalMilliseconds) / 1000.0f;
        if (currentTime > EndTime) {
            protect = false;
        }
    }
}
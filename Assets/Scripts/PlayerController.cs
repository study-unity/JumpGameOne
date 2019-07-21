using UnityEngine;
using System;
using UnityEngine.SceneManagement;
/*
 * Behaviour to handle keyboard input and also store the player's
 * current health.
 */
public class PlayerController : MonoBehaviour {
    private Rigidbody2D rigidbody2d;
    private int health;
    private bool canJump;
    private int protectCount;
    private int clockCount;
    private bool clock;
    bool protect;
    float EndTime;
    Transform childShield;

    /*
    * Apply initial health and also store the Rigidbody2D reference for
    * future because GetComponent<T> is relatively expensive.
    */
    private void Start () {
        health = 6;
        rigidbody2d = GetComponent<Rigidbody2D> ();
        protectCount=0;
        protect = false;
        childShield = transform.GetChild(1);
        childShield.gameObject.SetActive(false);
    }

    /*
     * Remove one health unit from player and if health becomes 0, change
     * scene to the end game scene.
     */
    public void Damage () {
        if (!protect) {
            health -= 1;

            if (health < 1) {
                SceneManager.LoadScene("EndGame");
            }
        }
        else{
            protect=false;
            childShield.gameObject.SetActive(false);
        }

    }

    /*
     * Accessor for health variable, used by he HUD to display health.
     */
    public int GetHealth() => health;

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
        if (canJump == true&&transform.position[1]<-3) {
            if (Input.GetKeyDown (KeyCode.UpArrow)) {
                rigidbody2d.AddForce (new Vector2 (0, 500));
                canJump = false;
            } else if (Input.GetKeyDown (KeyCode.Space)) {
                rigidbody2d.AddForce (new Vector2 (0, 600));
                canJump = false;
            }
        }
        if (Input.GetKeyDown (KeyCode.DownArrow)) {
                transform.localScale = new Vector3 (1f, 0.6f, 0.5f);
                canJump=false;
            }
        if (Input.GetKeyUp (KeyCode.DownArrow)) {
            transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
        }
        if(protectCount>0){
            if(Input.GetKeyDown(KeyCode.A)){
                protectCount--;
                ProtectPlayer();
            }
        }
        if(clockCount>0){
            if(Input.GetKeyDown(KeyCode.S)){
                clockCount--;
                transform.parent.gameObject.GetComponent<Move>().SlowDown();
            }
        }
    }
    /*
     * If the player has collided with the ground, set the canJump flag so that
     * the player can trigger another jump.
     */
    private void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.CompareTag ("block"))
            canJump = true;
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.name.Equals ("success(Clone)")) {
            string sceneName = SceneManager.GetActiveScene().name;
            if(sceneName.Equals("Game"))
                SceneManager.LoadScene("Game2");
            else if(sceneName.Equals("Game2"))
                SceneManager.LoadScene("Game3");
        } else if (collision.gameObject.CompareTag ("drug")) {
            Destroy (collision.gameObject);
            if (health < 6) {
                health += 1;
            }
        } else if (collision.gameObject.CompareTag ("shield")) {
            Destroy (collision.gameObject);
            if(protectCount<3){
                protectCount++;
            }
        } else if (collision.gameObject.CompareTag ("clock")) {
            Destroy (collision.gameObject);
            if(clockCount<3){
                clockCount++;
            }
        }
    }

    private void ProtectPlayer () {
        protect = true;
        childShield.gameObject.SetActive(true);
        EndTime = Time.time + 7;
    }

    private void UpdateProtect () {
        float currentTime = Time.time;
        if (currentTime > EndTime) {
            protect = false;
            childShield.gameObject.SetActive(false);
        }
    }

    public int GetShieldNum(){
        return protectCount;
    }
}
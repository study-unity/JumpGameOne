using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Behaviour to handle keyboard input and also store the player's
 * current health.
 */
public class PlayerController : MonoBehaviour {
    private Rigidbody2D rigidbody2d;

    //生命值
    private int health;

    private bool canJump;

    //盾牌数量
    private int protectCount;

    //减速道具数量
    private int clockCount;

    //是否处于盾牌保护状态
    bool protect;

    //盾牌的失效时间
    float EndTime;

    Transform childShield;
    public GameObject score;
    /*
     * Apply initial health and also store the Rigidbody2D reference for
     * future because GetComponent<T> is relatively expensive.
     */
    private void Start () {
        health = 6;
        rigidbody2d = GetComponent<Rigidbody2D> ();
        protectCount = 0;
        protect = false;
        childShield = transform.GetChild (1);
        childShield.gameObject.SetActive (false);
    }

    /*
     * Remove one health unit from player and if health becomes 0, change
     * scene to the end game scene.
     */
    public void Damage () {
        //如果没有盾牌保护，则损失一格生命，若生命归0则结束游戏
        if (!protect) {
            health -= 1;

            if (health < 1) {
                Instantiate(score).GetComponent<ScoreController>().Score = GetComponent<PlayerHud>().Score;
                SceneManager.LoadScene ("EndGame");
            }
        }
        //若有盾牌保护，则将protect标志置为false，且免疫本次伤害
        else {
            protect = false;
            childShield.gameObject.SetActive (false);
        }

    }

    /*
     * Accessor for health variable, used by he HUD to display health.
     */
    public int GetHealth () => health;

    /*
     * Poll keyboard for when the up arrow is pressed. If the player can jump
     * (is on the ground) then add force to the cached Rigidbody2D component.
     * Finally unset the canJump flag so the player has to wait to land before
     * another jump can be triggered.
     */
    private void Update () {
        //更新盾牌的持续状态以及跳跃状态
        UpdateJump ();
        UpdateProtect ();
    }

    private void UpdateJump () {
        //如果能够进行跳跃，则在按下上键时进行小跳，按下空格时进行大跳
        if (canJump == true && transform.position[1] < -3) {
            if (Input.GetKeyDown (KeyCode.UpArrow)) {
                rigidbody2d.AddForce (new Vector2 (0, 500));
                canJump = false;
            } else if (Input.GetKeyDown (KeyCode.Space)) {
                rigidbody2d.AddForce (new Vector2 (0, 600));
                canJump = false;
            }
        }
        //按下键时进行下蹲
        if (Input.GetKeyDown (KeyCode.DownArrow)) {
            transform.localScale = new Vector3 (1f, 0.6f, 0.5f);
            canJump = false;
        }
        //松开下键时则从下蹲状态恢复
        if (Input.GetKeyUp (KeyCode.DownArrow)) {
            transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
        }
        //若持有盾牌数量大于0，则在按下A键时释放盾牌并使盾牌数量减一
        if (protectCount > 0) {
            if (Input.GetKeyDown (KeyCode.A)) {
                protectCount--;
                ProtectPlayer ();
            }
        }
        //若持有减速道具数量大于0，则在按下S键时释放减速道具减速并使道具数量减一
        if (clockCount > 0) {
            if (Input.GetKeyDown (KeyCode.S)) {
                clockCount--;
                transform.parent.gameObject.GetComponent<Move> ().SlowDown ();
            }
        }
    }
    /*
     * If the player has collided with the ground, set the canJump flag so that
     * the player can trigger another jump.
     */
    private void OnCollisionEnter2D (Collision2D other) {
        //碰触到地面时可以再次跳跃
        if (other.gameObject.CompareTag ("block"))
            canJump = true;
    }

    /*检测player是否触发了触发器 */
    private void OnTriggerEnter2D (Collider2D collision) {
        /*若Player触发了旗子的触发器，则代表已经通过这一关，跳转到下一关 */
        if (collision.gameObject.name.Equals ("success(Clone)")) {
            string sceneName = SceneManager.GetActiveScene ().name;
            if (sceneName.Equals ("Game"))
                SceneManager.LoadScene ("Game2");
            else if (sceneName.Equals ("Game2"))
                SceneManager.LoadScene ("Game3");
        } else if (collision.gameObject.CompareTag ("drug")) {
            //如果player碰上了drug，则在生命值未满的情况下回复一格生命值
            Destroy (collision.gameObject);
            if (health < 6) {
                health += 1;
            }
        } else if (collision.gameObject.CompareTag ("shield")) {
            //如果player碰上了盾牌，则获得一个盾牌，可用来防御一次障碍（最多保存三个）
            Destroy (collision.gameObject);
            if (protectCount < 3) {
                protectCount++;
            }
        } else if (collision.gameObject.CompareTag ("clock")) {
            //如果player碰上了沙漏，则获得一次减速机会（最多保存三个）
            Destroy (collision.gameObject);
            if (clockCount < 3) {
                clockCount++;
            }
        }
    }

    /*
     * 使用盾牌保护player，时长为3秒
     */
    private void ProtectPlayer () {
        protect = true;
        childShield.gameObject.SetActive (true);
        EndTime = Time.time + 3;
    }

    /*
     * 更新盾牌的信息（是否过期）
     */
    private void UpdateProtect () {
        float currentTime = Time.time;
        if (currentTime > EndTime) {
            protect = false;
            childShield.gameObject.SetActive (false);
        }
    }

    /* 
     * 获取当前持有的盾牌数量
     */
    public int GetShieldNum () {
        return protectCount;
    }

    /*
     * 获取当前持有的减速道具数量
     */
    public int GetClockNum() {
        return clockCount;
    }
}
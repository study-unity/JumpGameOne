using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    // Start is called before the first frame update
    public int level;
    private float speed;
    public float GetSpeed () => speed;
    private bool slowDown;
    private float slowTime;
    private float realSpeed;

    void Start () {
        slowDown = false;
        //设置初始速度
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

    // Update is called once per frame
    void Update () {
        //如果不在在减速过程中，则在第三关中，speed未到达25时以每秒0.1的速度加速
        if (!slowDown) {
            if (level == 3 && speed < 25) {
                speed = speed + 0.1f * Time.deltaTime;
            }
        } else{
            //减速时间过后，恢复原有速度
            if(Time.time>=slowTime+3){
                slowDown=false;
                speed=realSpeed;
            }
        }
        transform.Translate (new Vector2 (speed, 0) * Time.deltaTime);
    }


    //进行减速，若不在减速状态中则进入减速状态并将速度设为原本的1/1.5，更新减速开始时间
    public void SlowDown () {
        if(!slowDown){
            slowDown = true;
            realSpeed=speed;
            speed = speed / 1.5f;
        }
        slowTime = Time.time;
    }
}
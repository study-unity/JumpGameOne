using UnityEngine;

public class ScoreController:MonoBehaviour{
    private int score;

    public int Score { get => score; set => score = value; }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
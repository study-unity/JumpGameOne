using UnityEngine;

// The script of the object that is used for remembering score between scenes.
public class ScoreController:MonoBehaviour{
    private int score;

    public int Score { get => score; set => score = value; }

    // Keep the object when a scene change occurs.
    void Start() => DontDestroyOnLoad(gameObject);
}
using UnityEngine;

// Show player's score in the end interface.
public class EndShow:MonoBehaviour{
    private GameObject score;
    private Texture2D[] numbers = new Texture2D[10];
    public GUISkin gameSkin;

    void Start()
    {
        // Get the score transmitted by the game scene.
        score = GameObject.Find("Score(Clone)");

        // Get number image textures.
        for(int i = 0;i<10;i++)
			numbers[i] = Resources.Load<Texture2D>("Number"+i.ToString());
    }

    void OnGUI()
    {
        // Adjust the GUIStyle.
        GUIStyle infoStyle = new GUIStyle();
        infoStyle.normal.textColor = new Color(1, 0.92f, 0.016f, 1);
        infoStyle.fontSize = 120;
        infoStyle.font = gameSkin.font;
        infoStyle.fontStyle = FontStyle.Italic;

        // Draw 'Your score:'.
        GUI.Label(new Rect(150,100,100,100),"Your score: ",infoStyle);

        // Draw score.
		string playerScore = score.GetComponent<ScoreController>().Score.ToString();
		for(int i = playerScore.Length-1; i>=0 ;i--)
			GUI.DrawTexture(new Rect(1000-100*(playerScore.Length-i-1),115,100,100),numbers[int.Parse(playerScore[i].ToString())]);
    }
}
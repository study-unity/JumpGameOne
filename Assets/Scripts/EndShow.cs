using UnityEngine;

public class EndShow:MonoBehaviour{
    private GameObject score;
    private Texture2D[] numbers = new Texture2D[10];
    public GUISkin gameSkin;

    void Start()
    {
        score = GameObject.Find("Score(Clone)");
        for(int i = 0;i<10;i++)
			numbers[i] = Resources.Load<Texture2D>("Number"+i.ToString());
    }
    void OnGUI()
    {
        GUIStyle infoStyle = new GUIStyle();
        infoStyle.normal.textColor = new Color(0,0,1,1);
        infoStyle.fontSize = 100;
        infoStyle.font = gameSkin.font;
        infoStyle.fontStyle = FontStyle.Bold;
        GUI.Label(new Rect(180,370,100,100),"Your score: ",infoStyle);
        // 画分数
		string playerScore = score.GetComponent<ScoreController>().Score.ToString();
		for(int i = playerScore.Length-1; i>=0 ;i--)
			GUI.DrawTexture(new Rect(1300-100*(playerScore.Length-i-1),370,100,100),numbers[int.Parse(playerScore[i].ToString())]);
    }
}
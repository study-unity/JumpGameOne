using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * On screen HUD to display current health.
 */
public class PlayerHud : MonoBehaviour
{
	private PlayerController playerController;
	private Texture2D halfHeart;
	private Texture2D heart;
	private Texture2D time;
	private int score;
	private Texture2D shield;
	private Texture2D[] numbers = new Texture2D[10];
	private Move move;
	private float keepTime;
	public GUISkin gameSkin;

    public int Score { get => score; set => score = value; }

    /*
* Load and store the heart images and cache the PlayerController
* component for later.
*/
    private void Start()
	{
		keepTime = 0.0f;
		playerController = GetComponent<PlayerController>();
		move = transform.parent.gameObject.GetComponent<Move>();
		heart = Resources.Load<Texture2D>("heart");
		halfHeart = Resources.Load<Texture2D>("halfHeart");
		time = Resources.Load<Texture2D>("time");
		shield = Resources.Load<Texture2D>("Shield");
		for(int i = 0;i<10;i++)
			numbers[i] = Resources.Load<Texture2D>("Number"+i.ToString());
	}

	private void Update()
	{
		keepTime += Time.deltaTime;
	}
	/*
	* Using the current health from the PlayerController, display the
	* correct number of hearts and half hearts.
	*/
	private void OnGUI()
	{
		// 画生命
		switch(playerController.GetHealth())
		{
			case 1:
				GUI.DrawTexture(new Rect(10, 10, 25, 50), halfHeart);
				break;
			case 2:
				GUI.DrawTexture(new Rect(10, 10, 50, 50), heart);
				break;
			case 3:
				GUI.DrawTexture(new Rect(10, 10, 50, 50), heart);
				GUI.DrawTexture(new Rect(70, 10, 25, 50), halfHeart);
				break;
			case 4:
				GUI.DrawTexture(new Rect(10, 10, 50, 50), heart);
				GUI.DrawTexture(new Rect(70, 10, 50, 50), heart);
				break;
			case 5:
				GUI.DrawTexture(new Rect(10, 10, 50, 50), heart);
				GUI.DrawTexture(new Rect(70, 10, 50, 50), heart);
				GUI.DrawTexture(new Rect(130, 10, 25, 50), halfHeart);
				break;
			default:
				GUI.DrawTexture(new Rect(10, 10, 50, 50), heart);
				GUI.DrawTexture(new Rect(70, 10, 50, 50), heart);
				GUI.DrawTexture(new Rect(130, 10, 50, 50), heart);
				break;
		}

		// 画分数
		string score = ((int)(keepTime*move.GetSpeed()/30)).ToString();
		Score = int.Parse(score);
		for(int i = score.Length-1; i>=0 ;i--)
			GUI.DrawTexture(new Rect(1190-80*(score.Length-i-1),10,80,80),numbers[int.Parse(score[i].ToString())]);

		// 画盾牌
		switch(playerController.GetShieldNum())
		{
			case 1:
				GUI.DrawTexture(new Rect(10,70,50,50),shield);
				break;
			case 2:
				GUI.DrawTexture(new Rect(10,70,50,50),shield);
				GUI.DrawTexture(new Rect(70,70,50,50),shield);
				break;
			case 3:
				GUI.DrawTexture(new Rect(10,70,50,50),shield);
				GUI.DrawTexture(new Rect(70,70,50,50),shield);
				GUI.DrawTexture(new Rect(130,70,50,50),shield);				
				break;
			default:
				break;
		}

		// 画减速
		switch(playerController.GetClockNum())
		{
			case 1:
				GUI.DrawTexture(new Rect(10,130,50,50),time);
				break;
			case 2:
				GUI.DrawTexture(new Rect(10,130,50,50),time);
				GUI.DrawTexture(new Rect(70,130,50,50),time);
				break;
			case 3:
				GUI.DrawTexture(new Rect(10,130,50,50),time);
				GUI.DrawTexture(new Rect(70,130,50,50),time);
				GUI.DrawTexture(new Rect(130,130,50,50),time);				
				break;
			default:
				break;
		}

		// 画欢迎信息
		if(keepTime<8)
		{
			GUIStyle infoStyle = new GUIStyle();
			string sceneName = SceneManager.GetActiveScene().name;
			if(sceneName.Equals("Game"))
			{
				infoStyle.fontSize = 50;
				infoStyle.normal.textColor = new Color(0,1,1,1);
				infoStyle.font = gameSkin.font;
				infoStyle.fontStyle = FontStyle.Bold;
				GUI.Label(new Rect(460,120,30,30),"Chapter 1:",infoStyle);
				infoStyle.fontStyle = FontStyle.BoldAndItalic;
				GUI.Label(new Rect(700,120,30,30),"Library",infoStyle);
				infoStyle.fontSize = 30;
				infoStyle.fontStyle = FontStyle.Normal;
				GUI.Label(new Rect(320,180,30,30),"This section is a prologue, you can know this game basically.",infoStyle);
				infoStyle.fontSize = 35;
				infoStyle.fontStyle = FontStyle.Italic;
				GUI.Label(new Rect(520,220,30,30),"Now leave the library!",infoStyle);
			}
			else if (sceneName.Equals("Game2"))
			{
				infoStyle.fontSize = 50;
				infoStyle.normal.textColor = new Color(0,0,1,1);
				infoStyle.font = gameSkin.font;
				infoStyle.fontStyle = FontStyle.Bold;
				GUI.Label(new Rect(480,120,30,30),"Chapter 2:",infoStyle);
				infoStyle.fontStyle = FontStyle.BoldAndItalic;
				GUI.Label(new Rect(730,120,30,30),"City",infoStyle);
				infoStyle.fontSize = 30;
				infoStyle.fontStyle = FontStyle.Normal;
				GUI.Label(new Rect(240,180,30,30),"In this section you can get some game props and the speed is faster.",infoStyle);
				infoStyle.fontSize = 35;
				infoStyle.fontStyle = FontStyle.Italic;
				GUI.Label(new Rect(540,220,30,30),"Now leave the city!",infoStyle);
			}
			else
			{
				infoStyle.fontSize = 50;
				infoStyle.normal.textColor = new Color(0,0,0,1);
				infoStyle.font = gameSkin.font;
				infoStyle.fontStyle = FontStyle.Bold;
				GUI.Label(new Rect(460,120,30,30),"Chapter 3:",infoStyle);
				infoStyle.fontStyle = FontStyle.BoldAndItalic;
				GUI.Label(new Rect(700,120,30,30),"Nature",infoStyle);
				infoStyle.fontSize = 30;
				infoStyle.fontStyle = FontStyle.Normal;
				GUI.Label(new Rect(223,180,30,30),"Now game is in the endless mode, but the speed will increase slowly with time.",infoStyle);
				infoStyle.fontSize = 35;
				infoStyle.fontStyle = FontStyle.Italic;
				GUI.Label(new Rect(490,220,30,30),"Enjoy yourself in the nature!",infoStyle);
			}
		}
	}
}

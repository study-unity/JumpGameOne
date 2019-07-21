using UnityEngine;
/*
 * On screen HUD to display current health.
 */
public class PlayerHud : MonoBehaviour
{
  private PlayerController playerController;
  private Texture2D halfHeart;
  private Texture2D heart;
  private Texture2D[] numbers = new Texture2D[10];
  private Move move;
  /*
   * Load and store the heart images and cache the PlayerController
   * component for later.
   */
  private void Start()
  {
    playerController = GetComponent<PlayerController>();
    move = transform.parent.gameObject.GetComponent<Move>();
    heart = Resources.Load<Texture2D>("heart");
    halfHeart = Resources.Load<Texture2D>("halfHeart");
    for(int i = 0;i<10;i++)
    {
      numbers[i] = Resources.Load<Texture2D>("Number"+i.ToString());
    }
  }

  /*
   * Using the current health from the PlayerController, display the
   * correct number of hearts and half hearts.
   */
  private void OnGUI()
  {
    if(playerController.GetHealth() == 6)
    {
      GUI.DrawTexture(new Rect(10, 10, 60, 50), heart);
      GUI.DrawTexture(new Rect(70, 10, 60, 50), heart);
      GUI.DrawTexture(new Rect(130, 10, 60, 50), heart);
    }
    else if(playerController.GetHealth() == 5)
    {
      GUI.DrawTexture(new Rect(10, 10, 60, 50), heart);
      GUI.DrawTexture(new Rect(70, 10, 60, 50), heart);
      GUI.DrawTexture(new Rect(130, 10, 30, 50), halfHeart);
    }
    else if(playerController.GetHealth() == 4)
    {
      GUI.DrawTexture(new Rect(10, 10, 60, 50), heart);
      GUI.DrawTexture(new Rect(70, 10, 60, 50), heart);
    }
    else if(playerController.GetHealth() == 3)
    {
      GUI.DrawTexture(new Rect(10, 10, 60, 50), heart);
      GUI.DrawTexture(new Rect(70, 10, 30, 50), halfHeart);
    }
    else if(playerController.GetHealth() == 2)
    {
      GUI.DrawTexture(new Rect(10, 10, 60, 50), heart);
    }
    else if(playerController.GetHealth() == 1)
    {
      GUI.DrawTexture(new Rect(10, 10, 30, 50), halfHeart);
    }

    string score = ((int)(Time.time*move.GetSpeed()/40)).ToString();
    for(int i = score.Length-1; i>=0 ;i--)
    {
      GUI.DrawTexture(new Rect(1100-70*(score.Length-i-1),10,80,80),numbers[int.Parse(score[i].ToString())]);
    }
  }
}

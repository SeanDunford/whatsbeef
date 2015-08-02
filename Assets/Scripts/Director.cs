using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {
	BronsonController bronson; 
	GhostFaceController ghostFace; 
	GeneratorScript generator; 
	public float initialSpeed = 5.0f; 
	public float lvlSpeedModifier = 0.1f; 
	public float enrageSpeedModifier = 0.02f; 
	public int blastOffCount = 5; 
	int currLvl = 0; 
	public Font myFont;
	public Texture2D tweetTexture, ghostTexture, menuTexture;
	public int score = 0, maxScore;
	public string scoreText = "Distance: 0m";
	public bool dead = false;

	// Use this for initialization
	void Start () {
		GameObject bronBron = GameObject.FindGameObjectWithTag("bronBron"); 
		bronson = bronBron.GetComponent<BronsonController>(); 
		if (!bronson.invincible) {
			GameObject gFace = GameObject.FindGameObjectWithTag ("gFace"); 
			ghostFace = gFace.GetComponent<GhostFaceController> (); 
			ghostFace.setFollowObject (bronBron);

			generator = this.GetComponent<GeneratorScript> (); 
			maxScore = PlayerPrefs.GetInt ("maxScore");
			InvokeRepeating ("AddToScore", 0.0f, 0.2f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!bronson.invincible) {
			if (generator.lvl > currLvl) {
				currLvl = generator.lvl; 
				updateSpeed (); 
			}
			if (bronson.tweets == 0) {
				generator.generateDTweet = false; 
			} else {
				generator.generateDTweet = true;
			}
			ghostFace.hangBack = bronson.blastingOff; 	
			ghostFace.speed = bronson.speed; 
		}
	}
	void OnGUI(){
		if (!bronson.invincible) {
			if (!bronson.dead) {
				DisplayEnrageLvl ();
				DisplayTweetCount ();
				DisplayScore ();
			}
		} else {
			DisplayMenu();
		}
	}
	void DisplayEnrageLvl(){
		Rect ghostRect = new Rect(8, Screen.height - 42, 32, 32);
		GUI.DrawTexture(ghostRect, ghostTexture);                         
		
		GUIStyle style = new GUIStyle();
		style.fontSize = 25;
		style.font = myFont;
		style.normal.textColor = new Color (241f/255f, 50f/255f, 36f/255f);
		
		Rect labelRect = new Rect(ghostRect.xMax, ghostRect.y + 4, 60, 32);
		GUI.Label(labelRect, "" + ghostFace.enrageLvl, style);
	}


	void DisplayTweetCount() {
		Rect tweetRect = new Rect(Screen.width - 70, Screen.height - 42, 32, 32);
		GUI.DrawTexture(tweetRect, tweetTexture);                         
		
		GUIStyle style = new GUIStyle();
		style.fontSize = 25;
		style.font = myFont;
		style.normal.textColor = new Color(0f, 0.6745098039f, 0.9294117647f);

		Rect labelRect = new Rect(tweetRect.xMax, tweetRect.y + 4, 60, 32);
		GUI.Label(labelRect, "" + bronson.tweets, style);
	}

	void DisplayScore() {
		GUIStyle style = new GUIStyle();
		style.fontSize = 30;
		style.normal.textColor = Color.white;
		style.font = myFont;
		style.alignment = TextAnchor.MiddleCenter;

		Rect labelRect = new Rect(10, 10, Screen.width - 20, 40);
		GUI.Label(labelRect, scoreText, style);
	}

	void updateSpeed(){
		float speed = initialSpeed + (lvlSpeedModifier * currLvl) + (enrageSpeedModifier * ghostFace.getEnragelvl()); 
		bronson.speed = speed; 
		ghostFace.speed = speed; 
	}
	public void increaseEnrage(){
		ghostFace.increaseEnrage(); 
	}
	public void decreaseEnrage(){
		ghostFace.decreaseEnrage(); 
	}
	public void blastingOff(){
		ghostFace.updateEnrageLvl(0); 
	}	
	public void AddToScore () {
		if (!bronson.dead) {
			score++;
			scoreText = "Distance: " + score + "m";
			if (score > maxScore) {
				maxScore = score;
				PlayerPrefs.SetInt("maxScore", maxScore);
			}
		}
	}
	public void blastingOff(bool blast) {
		CancelInvoke ("AddToScore");
		if (blast) {
			InvokeRepeating ("AddToScore", 0.0f, 0.05f);
		} else {
			InvokeRepeating ("AddToScore", 0.0f, 0.2f);
		}
	}
	public void DisplayMenu() {
		float padding = (Screen.height - (menuTexture.height + Screen.height * 0.1f) - 20) / 2;
		float buttonHeight = Screen.height * 0.1f;


		GUIStyle styleHighscore = new GUIStyle ();
		styleHighscore.fontSize = 80;
		styleHighscore.normal.textColor = Color.white;
		styleHighscore.font = myFont;
		styleHighscore.alignment = TextAnchor.MiddleCenter;
		Rect labelRect = new Rect (0, padding, Screen.width, Screen.width * 0.2885416667f);
		GUI.DrawTexture(labelRect, menuTexture);                         

		
		Rect buttonRect = new Rect (Screen.width * 0.3f, padding + 20 + menuTexture.height, Screen.width * 0.40f, buttonHeight);
		GUIStyle style = new GUIStyle ();
		style.fontSize = 20;
		style.normal.textColor = Color.white;
		style.font = myFont;
		style.alignment = TextAnchor.MiddleCenter;
		
		Texture2D tex2 = new Texture2D ((int)buttonRect.width, (int)buttonRect.height); 
		Color fillColor = Color.black;
		Color[] fillColorArray = tex2.GetPixels ();
		
		for (int i = 0; i < fillColorArray.Length; ++i) {
			fillColorArray [i] = fillColor;
		}
		tex2.SetPixels (fillColorArray);
		tex2.Apply ();
		style.normal.background = tex2;
		

		if (GUI.Button (buttonRect, "Tap to Start!", style)) {
			Application.LoadLevel (1);
		}
	}
}

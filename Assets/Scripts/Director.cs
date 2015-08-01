using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {
	BronsonController bronson; 
	GhostFaceController ghostFace; 
	GeneratorScript generator; 
	public float initialSpeed = 4.0f; 
	public float lvlSpeedModifier = 0.1f; 
	public float enrageSpeedModifier = 0.02f; 
	public int blastOffCount = 5; 
	int currLvl = 0; 
	public Font myFont;
	public Texture2D tweetTexture, ghostTexture;

	
	// Use this for initialization
	void Start () {
		GameObject bronBron = GameObject.FindGameObjectWithTag("bronBron"); 
		bronson = bronBron.GetComponent<BronsonController>(); 

		GameObject gFace = GameObject.FindGameObjectWithTag("gFace"); 
		ghostFace = gFace.GetComponent<GhostFaceController>(); 
		ghostFace.setFollowObject(bronBron);

		generator = this.GetComponent<GeneratorScript>(); 
	}
	
	// Update is called once per frame
	void Update () {
		if(generator.lvl > currLvl){
			currLvl = generator.lvl; 
			updateSpeed(); 
		}
		if(bronson.tweets == 0){
			generator.generateDTweet = false; 
		}
		else{
			generator.generateDTweet = true;
		}
		ghostFace.hangBack = bronson.blastingOff; 	
		ghostFace.speed = bronson.speed; 
	}
	void OnGUI(){
		DisplayEnrageLvl();
		DisplayCoinsCount ();
	}
	void DisplayEnrageLvl(){
//		Rect enrageIconRect = new Rect(8, Screen.height - 42, 32, 32);
//		GUIStyle style = new GUIStyle();
//		style.fontSize = 30;
//		style.normal.textColor = new Color (0.7803921569f, 0.2549019608f, 0.1882352941f);
//		style.font = myFont;

//		Rect labelRect = new Rect(10, enrageIconRect.y, 100, 32);
//		GUI.Label(labelRect, "Enrage: " + ghostFace.enrageLvl, style);

		Rect ghostRect = new Rect(8, Screen.height - 42, 32, 32);
		GUI.DrawTexture(ghostRect, ghostTexture);                         
		
		GUIStyle style = new GUIStyle();
		style.fontSize = 25;
		style.font = myFont;
		style.normal.textColor = new Color (0.7803921569f, 0.2549019608f, 0.1882352941f);
		
		Rect labelRect = new Rect(ghostRect.xMax, ghostRect.y + 4, 60, 32);
		GUI.Label(labelRect, "" + ghostFace.enrageLvl, style);
	}


	void DisplayCoinsCount()
	{
		Rect tweetRect = new Rect(Screen.width - 70, Screen.height - 42, 32, 32);
		GUI.DrawTexture(tweetRect, tweetTexture);                         
		
		GUIStyle style = new GUIStyle();
		style.fontSize = 25;
		style.font = myFont;
		style.normal.textColor = new Color(0f, 0.6745098039f, 0.9294117647f);

		Rect labelRect = new Rect(tweetRect.xMax, tweetRect.y + 4, 60, 32);
		GUI.Label(labelRect, "" + bronson.tweets, style);
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
}

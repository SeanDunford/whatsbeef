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
		DisplayTweets();
	}
	void DisplayEnrageLvl(){
		Rect enrageIconRect = new Rect(32, 10, 32, 32);
		GUIStyle style = new GUIStyle();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.red;
		
		Rect labelRect = new Rect(enrageIconRect.xMax, enrageIconRect.y, 100, 32);
		GUI.Label(labelRect, "Enrage Lvl: " + ghostFace.enrageLvl, style);
	}
	
	void DisplayTweets(){
		Rect enrageIconRect = new Rect(800, 10, 32, 32);
		GUIStyle style = new GUIStyle();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = new Color(64.0f,153.0f,255.0f);

		Rect labelRect = new Rect(enrageIconRect.xMax, enrageIconRect.y, 100, 32);
		GUI.Label(labelRect, "Tweet Lvl: " + bronson.tweets, style);
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



}

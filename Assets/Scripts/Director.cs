﻿using UnityEngine;
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
	public Texture2D tweetTexture, ghostTexture;
	public int score = 0, maxScore;
	public string scoreText = "Distance: 0m";
	public bool dead = false;

	// Use this for initialization
	void Start () {
		GameObject bronBron = GameObject.FindGameObjectWithTag("bronBron"); 
		bronson = bronBron.GetComponent<BronsonController>(); 

		GameObject gFace = GameObject.FindGameObjectWithTag("gFace"); 
		ghostFace = gFace.GetComponent<GhostFaceController>(); 
		ghostFace.setFollowObject(bronBron);

		generator = this.GetComponent<GeneratorScript>(); 
		maxScore = PlayerPrefs.GetInt("maxScore");
		InvokeRepeating("AddToScore", 0.0f, 0.2f);
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
		DisplayEnrageLvl ();
		DisplayTweetCount ();
		DisplayScore ();
	}
	void DisplayEnrageLvl(){
		Rect ghostRect = new Rect(8, Screen.height - 42, 32, 32);
		GUI.DrawTexture(ghostRect, ghostTexture);                         
		
		GUIStyle style = new GUIStyle();
		style.fontSize = 25;
		style.font = myFont;
		style.normal.textColor = new Color (0.7803921569f, 0.2549019608f, 0.1882352941f);
		
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
		style.fontSize = 40;
		style.normal.textColor = new Color (0.7803921569f, 0.2549019608f, 0.1882352941f);
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
}

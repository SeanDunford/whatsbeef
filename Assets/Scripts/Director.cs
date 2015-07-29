using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {
	BronsonController bronson; 
	GhostFaceController ghostFace; 
	GeneratorScript generator; 
	public float initialSpeed = 4.0f; 
	public float lvlSpeedModifier = 0.1f; 
	public float enrageSpeedModifier = 0.02f; 
	int currLvl = 0; 

	// Use this for initialization
	void Start () {
		GameObject bronBron = GameObject.FindGameObjectWithTag("bronBron"); 
		bronson = bronBron.GetComponent<BronsonController>(); 

		GameObject[] gFaces = GameObject.FindGameObjectsWithTag("gFace");
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

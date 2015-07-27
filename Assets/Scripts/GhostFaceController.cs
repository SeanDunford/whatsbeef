﻿using UnityEngine;
using System.Collections;

public class GhostFaceController : MonoBehaviour {
	public MouseController followObject; 
	public ParallaxScroll parallax;
	public float baseFollowDistance = 0; 
	private float followDistance = 0; 
	private int enrageLvl = 0; 
	private float lastTime; 
	private int[] angerTimes = {10, 10, 10, 5}; 
	private bool following = false; 
	Animator animator; 

	void Start () {
		lastTime = Time.time; 
		animator = GetComponent<Animator>();	
	}	
	void Update() {
		if (following) {


		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(following){
			float newX = followObject.transform.position.x - (baseFollowDistance + followDistance);  
			Vector2 position = GetComponent<Rigidbody2D> ().position; 
			position.x = Mathf.Round (newX * 100f) / 100f;
			GetComponent<Rigidbody2D> ().position = Vector3.MoveTowards(GetComponent<Rigidbody2D> ().position, position, 4.0f * Time.deltaTime);    
			updateTimers(); 
		}
	}
	void updateTimers(){
		if(Time.time > lastTime + angerTimes[enrageLvl]){
			lastTime = Time.time; 
			updateEnrageLvl(enrageLvl + 1); 
		}
	}
	public void increaseEnrage(){
		updateEnrageLvl(enrageLvl++);
	}
	public void decreaseEnrage(){
		updateEnrageLvl(enrageLvl--);
	}
	void updateEnrageLvl(int _enrageLvl){
		enrageLvl  = _enrageLvl; 
		switch(enrageLvl){
			case 0: 
				break; 
			case 1: 
				break; 
			case 2: 
				break; 
			default:
				following = false;
				Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
				newVelocity.x = 3.2f;
				GetComponent<Rigidbody2D>().velocity = newVelocity;
				break; 
		}
		animator.SetInteger("Enrage", enrageLvl);
	}
	void OnGUI(){
		DisplayEnrageLvl();
	}
	void DisplayEnrageLvl(){
		Rect enrageIconRect = new Rect(32, 10, 32, 32);
//		GUI.DrawTexture(coinIconRect, coinIconTexture);                         
		
		GUIStyle style = new GUIStyle();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.red;
		
		Rect labelRect = new Rect(enrageIconRect.xMax, enrageIconRect.y, 100, 32);
		GUI.Label(labelRect, "Enrage Lvl: " + enrageLvl, style);
	}
	public void setFollowObject(GameObject bronBron){
		MouseController mc = bronBron.GetComponent<MouseController>();
		followObject = mc; 
		if(followObject){
			following = true; 
		}
	}	
}

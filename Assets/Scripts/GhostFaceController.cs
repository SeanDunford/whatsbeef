using UnityEngine;
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
		GameObject ratObject = GameObject.FindGameObjectWithTag("bronBron"); 
		MouseController mc = ratObject.GetComponent<MouseController>();
		followObject = mc; 
		lastTime = Time.time; 
		if(followObject){
			following = true; 
		}
		animator = GetComponent<Animator>();	
	}	
	// Update is called once per frame
	void FixedUpdate () {
		if(following){
			float newX = followObject.xPosition() - (baseFollowDistance + followDistance);  
			Vector2 position = GetComponent<Rigidbody2D>().position; 
			position.x = newX; 
			GetComponent<Rigidbody2D>().position = position; 
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

	
}

using UnityEngine;
using System.Collections;

public class GhostFaceController : MonoBehaviour {
	public GameObject followObject; 
	public ParallaxScroll parallax;
	public float baseFollowDistance = 4.5f; 
	private float followDistance = 0; 
	public float speed = 3.0f; 
	private int enrageLvl = 0; 
	private float lastTime; 
	private int[] angerTimes = {10, 15, 25, 40, 40, 50}; 
	private float[] distances = {5.0f, 4.0f, 3.0f, 2.0f, 1.5f, .5f}; 
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
			GetComponent<Rigidbody2D> ().position = Vector3.MoveTowards(GetComponent<Rigidbody2D> ().position, position, speed * Time.deltaTime);    
			updateTimers(); 
		}
		else{
			Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
			newVelocity.x = speed + (enrageLvl * 0.01f);
			GetComponent<Rigidbody2D>().velocity = newVelocity;
		}
	}
	void updateTimers(){
		if(Time.time > lastTime + angerTimes[enrageLvl]){
			lastTime = Time.time; 
			updateEnrageLvl(enrageLvl + 1); 
		}
	}
	public void increaseEnrage(){
		if(enrageLvl == 5){
			return; 
		}
		updateEnrageLvl(enrageLvl + 1);
		lastTime = Time.time; 
	}
	public void decreaseEnrage(){
		if(enrageLvl == 0){
			return; 
		}
		updateEnrageLvl(enrageLvl - 1);
		lastTime = Time.time; 
	}
	void updateEnrageLvl(int _enrageLvl){
		enrageLvl  = _enrageLvl; 
		switch(enrageLvl){
			case 0: 
			case 1: 
			case 2: 
				following = true;
			    baseFollowDistance = distances[enrageLvl];
				break; 
			default:
				following = false;
				break; 
		}
		animator.SetInteger("Enrage", enrageLvl);
		Debug.Log("enrageLvl set to: " + enrageLvl);
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
	public void setFollowObject(GameObject obj){
		followObject = obj; 
		following = true; 
	}	
	public int getEnragelvl(){
		return enrageLvl; 
	}
}

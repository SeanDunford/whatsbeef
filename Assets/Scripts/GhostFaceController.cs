using UnityEngine;
using System.Collections;

public class GhostFaceController : MonoBehaviour {
	public GameObject followObject; 
	public ParallaxScroll parallax;
	public float baseFollowDistance = 4.5f; 
	private float followDistance = 0; 
	public float speed = 3.0f; 
	public int enrageLvl = 0; 
	private float lastTime; 
	private int[] angerTimes = {5, 10, 10, 10, 15, 20}; //{20, 15, 25, 20, 15, 25}; 
	private float[] distances = {7.0f, 6.0f, 5.0f, 3.0f, 1.5f, .5f}; 
	private bool following = false; 
	public bool hangBack = true; 
	public float smoothTime = 1.0f; 
	Animator animator; 

	void Start () {
		lastTime = Time.time; 
		animator = GetComponent<Animator>();	
		baseFollowDistance = distances[enrageLvl];
	}	
	void Update() {
	}
	// Update is called once per frame
	void FixedUpdate () {
		baseFollowDistance = distances[enrageLvl];
		followDistance = (hangBack) ? 2.0f : 0.0f; 
		if(following){
			followDistance = -3.0f; 
		}
		float distance = baseFollowDistance + followDistance;  
		Vector2 currPosition = transform.position; 
		currPosition.x = Mathf.Lerp( transform.position.x, followObject.transform.position.x - distance, Time.deltaTime * smoothTime);
		transform.position = currPosition; 
		updateTimers(); 
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
	public void updateEnrageLvl(int _enrageLvl){
		enrageLvl  = _enrageLvl; 
		lastTime = Time.time; 
		switch(enrageLvl){
			case 0: 
			case 1: 
			case 2: 
				following = true;
				break; 
			default:
				following = false;
				break; 
		}
		animator.SetInteger("Enrage", enrageLvl);
		Debug.Log("enrageLvl set to: " + enrageLvl);
	}

	public void setFollowObject(GameObject obj){
		followObject = obj; 
		following = true; 
	}	
	public int getEnragelvl(){
		return enrageLvl; 
	}
}

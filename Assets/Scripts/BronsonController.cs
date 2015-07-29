using UnityEngine;
using System.Collections;

public class BronsonController: MonoBehaviour {

	public float jumpForce = 1500.0f;
	public float speed = 3.0f;
	public Transform groundCheckTransform;
	public LayerMask groundCheckLayerMask;
	public Texture2D coinIconTexture;
	public AudioClip coinCollectSound;
	public AudioSource footstepsAudio;
	public Director director; 
	public int level; 

	private bool dead = false;
	public int tweets = 0;
	private bool grounded;
	Animator animator;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();	
		director = GameObject.FindGameObjectWithTag("director").GetComponent<Director>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		if(!dead && grounded && Input.GetButton("Fire1")){
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}
		if (!dead){
			Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
			newVelocity.x = speed;
			GetComponent<Rigidbody2D>().velocity = newVelocity;
		}
		UpdateGroundedStatus();	
	} 

	void UpdateGroundedStatus(){
		grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
		animator.SetBool("grounded", grounded);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.CompareTag("dTweet")){
			Debug.Log("Hit dtweet increase enrage");
			director.increaseEnrage(); 
			collider.gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0); 
			tweets--; 
		}
		else if(collider.gameObject.CompareTag("tweet")){
			Debug.Log("Hit tweet decrease enrage");
			director.decreaseEnrage(); 
			collider.gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0); 
			tweets++; 
		}
		else if(collider.gameObject.CompareTag("ghost")){
			dead = true;
			animator.SetBool("dead", true);
		}
		else if(collider.gameObject.CompareTag("gFace")){
			dead = true;
			animator.SetBool("dead", true);
		}
	}
	void OnGUI(){
		DisplayRestartButton();
	}
	
	void DisplayRestartButton(){
		if (dead && grounded){
			Rect buttonRect = new Rect(Screen.width * 0.35f, Screen.height * 0.45f, Screen.width * 0.30f, Screen.height * 0.1f);
			if (GUI.Button(buttonRect, "Tap to restart!")){
				Application.LoadLevel (Application.loadedLevelName);
			};
		}
	}

	public float xPosition(){ return GetComponent<Rigidbody2D>().position.x;}
}

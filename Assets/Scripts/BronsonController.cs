using UnityEngine;
using System.Collections;

public class BronsonController: MonoBehaviour {

	public float jumpForce = 1500.0f;
	public float blastForce = 10.0f; 
	public bool blastingOff = false; 
	public float speed = 3.0f;
	public Transform groundCheckTransform;
	public LayerMask groundCheckLayerMask;
	public Texture2D coinIconTexture;
	public AudioClip coinCollectSound;
	public AudioSource footstepsAudio;
	public Director director; 
	public int level; 
	private float lastTime; 
	private bool dead = false;
	public int tweets = 0;
	public float blastOffTime = 10; 
	private bool grounded;
	private bool forceFall = false; 
	Animator animator;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();	
		director = GameObject.FindGameObjectWithTag("director").GetComponent<Director>(); 
		lastTime = Time.time; 
	}
	
	// Update is called once per frame
	void Update () {
			
	}
	void Jump(){
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
	}
	void ForceFall(){
		GetComponent<Rigidbody2D>().gravityScale = 1; 
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, (-1.0f * jumpForce * 0.3f)));
	}
	void FixedUpdate () {
		if(!dead){
			if(grounded && Input.GetButton("Fire1")){
				Jump();
			}
			Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
			newVelocity.x = speed;
			if(blastingOff){
				newVelocity.x += blastForce; 
			}
			if(forceFall){
				ForceFall(); 
			}
			GetComponent<Rigidbody2D>().velocity = newVelocity;
		}
		updateTimers(); 
		UpdateGroundedStatus();	
	} 
	void UpdateGroundedStatus(){
		grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
		if(grounded) forceFall = false; 
		animator.SetBool("grounded", grounded);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if(blastingOff){
			return; 
		}
		if (collider.gameObject.CompareTag("dTweet")){
			Debug.Log("Hit dtweet increase enrage");
			collider.gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0); 
			setTweets(tweets - 1);
		}
		else if(collider.gameObject.CompareTag("tweet")){
			Debug.Log("Hit tweet decrease enrage");
			collider.gameObject.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0); 
			setTweets(tweets + 1); 
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
	void updateTimers(){
		if(blastingOff && (Time.time > (lastTime + blastOffTime))){
			lastTime = Time.time; 
			setBlastOff(false); 
		}
		else if(blastingOff && (Time.time > (lastTime + blastOffTime * 0.5))){
			forceFall = true; 
		}
	}
	void setBlastOff(bool blast){
		if(blast){
			GetComponent<Rigidbody2D>().gravityScale = 0; 
			lastTime = Time.time; 
			director.blastingOff(); 
			if(grounded){
				Jump (); 
			}
		}
		blastingOff = blast; 
	}
	void setTweets(int t){
		if(t == 5){
			tweets = 0; 
			setBlastOff(true); 
			return; 
		}
		tweets = t; 	
	}

	public float xPosition(){ return GetComponent<Rigidbody2D>().position.x;}
}

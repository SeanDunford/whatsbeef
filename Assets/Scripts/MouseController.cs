using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	public float jetpackForce = 75.0f;
	public float forwardMovementSpeed = 3.0f;
	public Transform groundCheckTransform;
	public LayerMask groundCheckLayerMask;
	public ParticleSystem jetpack;
	public Texture2D coinIconTexture;
	public AudioClip coinCollectSound;
	public AudioSource jetpackAudio;
	public AudioSource footstepsAudio;
	public ParallaxScroll parallax;
	public Director director; 

	private bool dead = false;
	private int tweets = 0;
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
		bool jetpackActive = Input.GetButton("Fire1");	
		jetpackActive = jetpackActive && !dead;
		
		if (jetpackActive){
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jetpackForce));
		}
		
		if (!dead){
			Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
			newVelocity.x = forwardMovementSpeed;
			GetComponent<Rigidbody2D>().velocity = newVelocity;
		}
		
		UpdateGroundedStatus();	
		AdjustJetpack(jetpackActive);
		AdjustFootstepsAndJetpackSound(jetpackActive);
		parallax.offset = transform.position.x;
	} 

	void UpdateGroundedStatus(){
		grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
		animator.SetBool("grounded", grounded);
	}

	void AdjustJetpack (bool jetpackActive){
		jetpack.enableEmission = !grounded;
		jetpack.emissionRate = jetpackActive ? 300.0f : 75.0f; 
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.CompareTag("dTweet")){
			if(tweets > 0){
				director.increaseEnrage(); 
			}
		}
		else if(collider.gameObject.CompareTag("tweet")){
			director.decreaseEnrage(); 
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

	void AdjustFootstepsAndJetpackSound(bool jetpackActive)    {
		footstepsAudio.enabled = !dead && grounded;
		jetpackAudio.enabled =  !dead && !grounded;
		jetpackAudio.volume = jetpackActive ? 1.0f : 0.5f;        
	}

	public float xPosition(){ return GetComponent<Rigidbody2D>().position.x;}

}

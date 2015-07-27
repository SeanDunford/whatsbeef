using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {
	MouseController mouse; 
	GhostFaceController ghostFace; 

	// Use this for initialization
	void Start () {
		mouse = GameObject.FindGameObjectWithTag("bronBron").GetComponent<MouseController>(); 
		GameObject go = GameObject.FindGameObjectWithTag("gFace"); 
		ghostFace = go.GetComponent<GhostFaceController>(); 
	}
	
	// Update is called once per frame
	void Update () {
		if(ghostFace == null){
			ghostFace = GameObject.FindGameObjectWithTag("gFace").GetComponent<GhostFaceController>(); 
		}
	}
	public void increaseEnrage(){
		ghostFace.increaseEnrage(); 
	}
	public void decreaseEnrage(){
		ghostFace.decreaseEnrage(); 
	}

}

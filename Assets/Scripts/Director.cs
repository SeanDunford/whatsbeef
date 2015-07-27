using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {
	MouseController mouse; 
	GhostFaceController ghostFace; 

	// Use this for initialization
	void Start () {
		GameObject bronBron = GameObject.FindGameObjectWithTag("bronBron"); 
		mouse = bronBron.GetComponent<MouseController>(); 

		GameObject[] gFaces = GameObject.FindGameObjectsWithTag("gFace");
		GameObject gFace = GameObject.FindGameObjectWithTag("gFace"); 
		ghostFace = gFace.GetComponent<GhostFaceController>(); 
		ghostFace.setFollowObject(bronBron);
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

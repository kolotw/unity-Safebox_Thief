using UnityEngine;
using System.Collections;

public class monitorCamMoving : MonoBehaviour {
	public int turnTime=3;
	private float startTime=0;
	private int moveDir=1;
	// Use this for initialization
	void Start () {
		startTime=Time.time;
		moveDir=1;
	}
	
	// Update is called once per frame
	void Update () {
		float tempTime = Time.time-startTime;
		//print("time: " + tempTime);
		if(tempTime>turnTime){
			
			moveDir*=-1;
			startTime=Time.time;
		}
		transform.Translate(3*moveDir*Time.deltaTime,0,0);
	}
}

using UnityEngine;
using System.Collections;

public class monitorCamTurn : MonoBehaviour {
	public int turnTime=3;
	private float startTime=0;
	private int turnLeft=0;

	private Ray myRay0;
	private Ray myRay1;
	private Ray myRay2;
	private RaycastHit myHit0;	
	private RaycastHit myHit1;
	private RaycastHit myHit2;
	public Transform camPoint;
	public Transform camEnd0;
	public Transform camEnd1;
	public Transform camEnd2;
	
	public LineRenderer myLine;


	// Use this for initialization
	void Start () {
		startTime=Time.time;

        myLine.startWidth = 0;
        myLine.endWidth = 2;
    }
	
	// Update is called once per frame
	void Update () {
		myLine.SetPosition(0,camPoint.transform.position);
		
		if(Time.time-startTime>turnTime){
			startTime=Time.time;
			//turnCam();
			turnLeft++;
			if(turnLeft>1)
				turnLeft=0;
		}
		Quaternion target = Quaternion.Euler(0, -180*turnLeft, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime*10);
		
		
		//raycast
		Vector3 direction0;
		Vector3 direction1;
		Vector3 direction2;
		direction0 = camEnd0.transform.position-camPoint.transform.position;
		direction1 = camEnd1.transform.position-camPoint.transform.position;
		direction2 = camEnd2.transform.position-camPoint.transform.position;
		myRay0 = new Ray (camPoint.transform.position,direction0);	
		myRay1 = new Ray (camPoint.transform.position,direction1);	
		myRay2 = new Ray (camPoint.transform.position,direction2);	
		
		Debug.DrawLine(myRay0.origin, camEnd0.transform.position);
		Debug.DrawLine(myRay0.origin, camEnd1.transform.position);
		Debug.DrawLine(myRay0.origin, camEnd2.transform.position);
		
		float dist0= Vector3.Distance(camEnd0.position, camPoint.position);
		float dist1= Vector3.Distance(camEnd1.position, camPoint.position);
		float dist2= Vector3.Distance(camEnd2.position, camPoint.position);
		
		if (Physics.Raycast(myRay0, out myHit0,dist0)){
			myLine.SetPosition(1, myHit0.point);
			if((myHit0.transform.tag=="Player" || myHit0.transform.tag=="myTV") && gameMaster.isAlert==false){
				gameMaster.isAlert=true;
			}
		}else{
			myLine.SetPosition(1, camEnd0.transform.position);
		}
		if (Physics.Raycast(myRay1, out myHit1,dist1)){
			if((myHit1.transform.tag=="Player" || myHit1.transform.tag=="myTV") && gameMaster.isAlert==false){
				gameMaster.isAlert=true;
			}
		}
		if (Physics.Raycast(myRay2, out myHit2,dist2)){
			if((myHit2.transform.tag=="Player" || myHit2.transform.tag=="myTV") && gameMaster.isAlert==false){
				gameMaster.isAlert=true;
			}
		}
		if(gameMaster.isCounting){
            myLine.startColor = Color.red;
            myLine.endColor = Color.black;
        }
        else{
            myLine.startColor = Color.white;
            myLine.endColor = Color.black;
		}
	}
	
}

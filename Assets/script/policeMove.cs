using UnityEngine;
using System.Collections;

public class policeMove : MonoBehaviour {
	private int moveDir=1;
	private float moveSpeed;
	
	private Ray myRay;
	private RaycastHit myHit;
	public Transform patrollTarget;
	public Transform policePoint;
	RaycastHit[] hits;


	private Transform go;
	private bool goingUp=false;
	private string tempName;
	private int upCount=6;
	private int downCount=4;

	GameObject myPlayer;
	
	// Use this for initialization
	void Start () {
		myPlayer = GameObject.Find("PlayerSet");
		moveDir=1;
		if(this.transform.name=="PatrolSet"){
			moveSpeed=2.0f;
		}
		if(this.transform.name=="PoliceSet(Clone)"){
			moveSpeed=12.0f;
			Destroy(gameObject,10);
		}
	}
	
	// Update is called once per frame
	void Update () {
		checkPlayer();

		if(gameMaster.isCounting){
			moveSpeed=12.0f;
			Transform pp;
			pp = transform.Find("policeManSet/PoliceCapsule");
			pp.GetComponent<Renderer>().material.color=Color.red;
		}else{
			if(this.transform.name=="PatrolSet"){
				moveSpeed=2.0f;
			}
			if(this.transform.name=="PoliceSet(Clone)"){
				moveSpeed=12.0f;
			}
			Transform pp;
			pp = transform.Find("policeManSet/PoliceCapsule");
			pp.GetComponent<Renderer>().material.color=Color.white;
		}
		transform.Translate(moveSpeed*Time.deltaTime,0,0);
		gravityMovement();

	}
	private void checkPlayer() {
		if (patrollTarget != null)
		{
			Vector3 direction;
			direction = patrollTarget.transform.position - policePoint.transform.position;
			myRay = new Ray(policePoint.transform.position, direction);
			Debug.DrawLine(policePoint.transform.position, patrollTarget.transform.position);
			float dist0 = Vector3.Distance(policePoint.transform.position, patrollTarget.position); //default Untagged
			float dist1 = 0; //PoliceTurn
			float dist2 = 0; //Player
			float dist3 = 0; //door

			hits = Physics.RaycastAll(myRay, dist0);
			//理論上應該是要比較兩者，或三者，離PLAYER最近的才可以發出警報
			foreach (RaycastHit hit in hits)
			{
				if (hit.collider.tag.Equals("Untagged"))
				{
					//dist0 = Vector3.Distance(policePoint.transform.position, hit.transform.position);
				}
				if (hit.collider.tag.Equals("policeTurn"))
				{
					dist1 = Vector3.Distance(policePoint.transform.position, hit.transform.position);
				}
				if (hit.collider.tag.Equals("door"))
				{
					dist3 = Vector3.Distance(policePoint.transform.position, hit.transform.position);
				}
				if (hit.collider.tag.Equals("Player"))
				{
					dist2 = Vector3.Distance(policePoint.transform.position, hit.transform.position);					
				}
			}
			if (gameMaster.isAlert == false && dist2>0) {
				if (dist2 < dist1 && dist2 < dist0 && dist2<dist3) {
					gameMaster.isAlert = true;
					print("D0:" + dist0.ToString() + " D1:" + dist1.ToString() + " D2:" + dist2.ToString() + " D3:" + dist3.ToString());
				}
			}
		}
	}
	public void gravityMovement(){
		int turnAngle=0;
		if(moveDir>0){
			turnAngle=0; //right
		}else{
			turnAngle=180; //left
		}
		Quaternion target = Quaternion.Euler(0, turnAngle, 0); //left
		transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);
	
	}	
	void OnTriggerStay(Collider other){
		if(other.tag=="upStair" && this.transform.name=="PoliceSet(Clone)"){
			tempName=other.name;
			goingUp=true;
		}
	}
	void OnTriggerExit(Collider other){
		if(other.tag=="upStair" && this.transform.name=="PoliceSet(Clone)"){
			
			goingUp=false;
		}
	}
	void OnTriggerEnter(Collider other){
		if(other.tag=="policeTurn" || other.tag == "door")
		{
			moveDir=moveDir*-1;
			return;
		}

		if(goingUp){
			//print("other: "+ other.name);
			if(upCount>0){
				if(other.name=="up"){
					go = transform.Find("/"+tempName+"/down");
					StartCoroutine(waitUp(0.5f));
					if(go!=null){
						this.transform.position = new Vector3(go.position.x,go.position.y,0);
					}
				}
			}
			if(upCount==0 && downCount>0){
				if(other.name=="down"){
					if(downCount==0){
						
					}else{
						go = transform.Find("/"+tempName+"/up");
						print("should going up");
						StartCoroutine(waitDown(0.5f));
						if(go!=null){
							this.transform.position = new Vector3(go.position.x,go.position.y,0);
						}
					}
				}
			}
			
			
		}
	}
	IEnumerator waitUp(float waitTime){
		yield return new WaitForSeconds(waitTime);
		upCount--;
		if(upCount<0){
			upCount=0;
		}
		
	}	
	IEnumerator waitDown(float waitTime){
		yield return new WaitForSeconds(waitTime);
		downCount--;
		//print("down count: " + downCount);
		
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour {
	private Vector3 moveDirection = Vector3.zero;
	public float speed = 10.0F;
	float horiDir = 0;


	private Transform TV;
	private bool isTV;
	//private bool isHidingColumn=false;
	//private bool isHidingShadow=false;
	private int isHiding=0;
	
	private Transform go;
	private int moveDir=1;
	
	public Transform playerBody;
	public Transform playerHead;
	Image tip;

	//sound
	public AudioSource SoundFX;
	public AudioClip gotYou;
	public AudioClip hidingSound;
	
	// Use this for initialization
	void Start () {
		
		tip=GameObject.Find("/Canvas/Image_TIP").GetComponent<Image>();
		if (tip != null)
			tip.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();
		//moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		if (Input.GetAxis("Axis 7") != 0)
		{
			horiDir = Input.GetAxis("Axis 7");
		}
		else {
			horiDir = Input.GetAxis("Horizontal");
		}
		
		moveDirection = new Vector3(horiDir, 0, 0);
        moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		controller.Move(moveDirection * Time.deltaTime);

		//if (Input.GetButton("Jump")) {
		//	print("JUMP ▲型");
		//}
		//if (Input.GetButton("Fire1"))
		//{
		//	print("Fire1 方型");
		//}
		//if (Input.GetButton("Fire2"))
		//{
		//	print("Fire2 X型");
		//}
		//if (Input.GetButton("Fire3"))
		//{
		//	print("Fire3 O型");
		//}

		//print(Input.GetAxisRaw("Horizontal"));

		if (Input.GetAxis("Horizontal") > 0.1f  || Input.GetAxis("Axis 7") > 0f)
		{
			moveDir = 1;
		}
		else if (Input.GetAxis("Horizontal") < -0.1f || Input.GetAxis("Axis 7") < 0f)
		{
			moveDir = -1;
		}

		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			moveDir=-1;
		}
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
			moveDir=1;
		}
		turnOver();
		
		if(Input.GetKey(KeyCode.Q)){
			Time.timeScale=0.1f;
		}
		if(Input.GetKey(KeyCode.E)){
			Time.timeScale=1;
		}	
		
		//hiding
		if(isHiding!=0){
			switch (isHiding)
			{
				case 1: //column
				case 2: //shadow
					this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1.5f);
					break;
				
					//this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 1.0f);
					//break;
				default:
					this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 1.5f);
					break;
			}
		}
		else{
			this.transform.position=new Vector3(this.transform.position.x,this.transform.position.y,0);
		}
		
		
		
	}
	void turnOver(){
		int turnAngle=0;
		if(moveDir>0){
			turnAngle=0; //right
		}else{
			turnAngle=115; //left
		}
		Quaternion target = Quaternion.Euler(0, turnAngle, 0); //left
		playerBody.transform.rotation = Quaternion.Slerp(playerBody.transform.rotation, target, 1);		
	}
	void OnTriggerEnter(Collider other){
		
		
		if(other.tag=="column" ||other.tag=="shadow"){
			SoundFX.PlayOneShot(hidingSound,0.2f);
		}
		
		if(other.tag=="enemy" && gameMaster.isCounting){
			SoundFX.PlayOneShot(gotYou,1f);
			
			print("catch");
			isHiding=0;
			//isTV=false;
			gameMaster.isAlife=false;
			Destroy(other.gameObject);
			playerBody.GetComponent<Renderer>().enabled=false;
			playerHead.GetComponent<Renderer>().enabled=false;
			StartCoroutine(waitSound(1.0f));
			
		}
	}
	IEnumerator waitSound(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Destroy(this.gameObject);
	}	
	void OnTriggerStay(Collider other){
		//loader and elevator
		if (Input.GetAxisRaw("Vertical") > 0.1f || Input.GetAxis("Axis 8") > 0f)
		{
			print("UP");
			go = transform.Find("/" + other.name + "/down");
			if (go != null)
				this.transform.position = new Vector3(go.position.x, go.position.y, 0);
		}
		else if (Input.GetAxisRaw("Vertical") < -0.1f || Input.GetAxis("Axis 8") < 0f) 
		{
			print("Down");
			go = transform.Find("/" + other.name + "/up");
			if (go != null)
				this.transform.position = new Vector3(go.position.x, go.position.y, 0);
		}
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetButtonUp("Jump"))
		{
			go = transform.Find("/"+other.name+"/down");
			if(go!=null)
				this.transform.position = new Vector3(go.position.x,go.position.y,0);
		}
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetButtonUp("Fire2"))
		{
			go = transform.Find("/"+other.name+"/up");
			if(go!=null)
				this.transform.position = new Vector3(go.position.x,go.position.y,0);
		}
		if(other.tag=="column"){
			isHiding=1;
			
		}
		if(other.tag=="shadow"){
			//print("in shadow");
			isHiding=2;
			
		}
		
		if(other.name=="exit"){
			GameObject go = GameObject.Find("00gameMaster");
			go.GetComponent<gameMaster>().checkLV();
				
		}
		
		//show tip
		if(other.tag=="goal"){			
			if(tip != null){
				tip.enabled = true;
				if(gameMaster.tipOff){
					tip.enabled = false;
				}
			}
		}
		
	}
	void OnTriggerExit(Collider other){
		if(other.tag=="column" || other.tag=="shadow"){
			isHiding=0;			
		}
		
			
	}
}

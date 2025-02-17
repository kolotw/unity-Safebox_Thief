using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameMaster : MonoBehaviour {
	public static bool isAlert=false;
	
	public Text TxtStatus;
	public Text TxtCount;
	public Text TxtInfo;
	public Text TxtLevel;
	public Image infoBoard;

	private int countDown=10;
	private float startTime=0;
	public static bool isCounting=false;
	
	public Transform policeMan;
	public Transform policeSpawn;
	private int policeCount=0;
	
	public int currentLV;
	private int TVnumGoal;
	public static int TVnum;
	
	
	public static bool isAlife=true;
	public Transform player;
	
	public static bool tipOff=false;
	
	
	public Sprite info_StartLV1;	
	public Sprite info_StartLV2;
	public Sprite info_StartLV3;
	public Sprite info_StartLV4;
	public Sprite info_StartLV5;
	public Sprite info_StartLV6;
	public Sprite info_Win;
	public Sprite empty;	
	
	//sound
	public AudioSource BGSound;
	public AudioSource SoundFX;
	public AudioClip alertSound;

    [System.Obsolete]
    void Start () {
		BGSound.Play();
		SoundFX.Stop(); 
        TxtCount.text="";
		TxtInfo.text = "";
		TxtLevel.text = "Level " + currentLV;
		isAlert =false;
		isCounting=false;
		isAlife=true;
		TVnum=0;
	 	seeLV();
		StartCoroutine(clearScreen(3.0f));
	}

	void seeLV(){
		switch(currentLV){
			case 1:
				infoBoard.sprite = info_StartLV1;
				TVnumGoal =1;
				
				break;
			case 2:
				infoBoard.sprite = info_StartLV2;
				TVnumGoal =1;
				break;
			case 3:
				infoBoard.sprite = info_StartLV3;
				TVnumGoal =1;
				break;
			case 4:
				infoBoard.sprite = info_StartLV4;
				TVnumGoal =2;
				break;
			case 5:
				infoBoard.sprite = info_StartLV5;
				TVnumGoal =3;//3
				break;
			case 6:
				infoBoard.sprite = info_StartLV6;
				TVnumGoal =3;//3
				break;
			case 7:
				infoBoard.sprite = info_Win;
				break;
			default:
				//infoBoard.guiTexture.texture=info_StartLV1;
				TVnumGoal=1;
				break;
		}
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			SceneManager.LoadScene("menu");
		}
		if(currentLV==6){
			isAlert=true;
		}
		if(isAlert){
			
			TxtStatus.text="Status: Warning";
			if(!isCounting){
				startTime=Time.time+10;
				isCounting=true;
			}
			//gen police
			if(policeCount==0){
				Instantiate (policeMan,policeSpawn.transform.position, Quaternion.identity);
				policeCount++;
				SoundFX.PlayOneShot(alertSound,1f);
			}
			isAlert=false;
		}
		if(isCounting){
			counting();
		}else{
			countDown=10;
		}

		
		//checkLV();
		if(!isAlife){
			isAlife=true;
			TxtInfo.text="Catch";
			StartCoroutine(spawnPlayer(1.0f));
		}
	}
	IEnumerator spawnPlayer(float waitTime){
		yield return new WaitForSeconds(waitTime);
		SceneManager.LoadScene("LV" + currentLV);
		//Instantiate (player,policeSpawn.transform.position, Quaternion.identity);
	}
	IEnumerator clearScreen(float waitTime){
		yield return new WaitForSeconds(waitTime);
		infoBoard.sprite = empty;
		if(currentLV==6){			
			SceneManager.LoadScene("menu");
		}
	}	
	public void checkLV(){
		if(TVnum==TVnumGoal){
			TxtInfo.text="COMPLETE";
			//level UP
			StartCoroutine(nextLV(1.0f));
		}
	}
	IEnumerator nextLV(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		currentLV++;
		if (currentLV < 6)
		{
			SceneManager.LoadScene("LV" + currentLV);
		}
		else
		{
			// play complete music
			BGSound.Stop();
			//GetComponent<AudioSource>().Stop();
			SceneManager.LoadScene("win");
			//StartCoroutine(clearScreen(3.0f));

		}
	}
	void counting(){
		countDown= Mathf.RoundToInt((Time.time-startTime)*-1);
		if(currentLV==6){
			TxtCount.fontSize=50;
			TxtCount.font.material.color=Color.red;
			TxtCount.text= "Next police comes up in " + countDown.ToString() + " seconds";
		}else{
			TxtCount.font.material.color=Color.white;
			TxtCount.text= countDown.ToString();
		}
		if(countDown==0){
			isCounting=false;
			TxtCount.text="";
			TxtStatus.text="Status: Peaceful";
			policeCount=0;
		}
	}
}

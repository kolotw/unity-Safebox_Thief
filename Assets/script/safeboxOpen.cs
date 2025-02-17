using UnityEngine;
using System.Collections;

public class safeboxOpen : MonoBehaviour {
	public Texture saferClose;
	public Texture safer1;
	public Texture safer2;
	public Texture safer3;
	public Texture saferOpen;
	int clickTime=0;
	int openProgress=0;
	bool isOpen=false;
	//sound
	public AudioSource SoundFX;
	public AudioClip unlockedBox;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!isOpen){
			switch (openProgress)
			{
				case 0:
					GetComponent<Renderer>().material.mainTexture = saferClose;
					break;
				case 1:
					GetComponent<Renderer>().material.mainTexture = safer1;
					break;
				case 2:
					GetComponent<Renderer>().material.mainTexture = safer2;
					break;
				case 3:
					GetComponent<Renderer>().material.mainTexture = safer3;
					break;
				case 4:
					GetComponent<Renderer>().material.mainTexture = saferOpen;
					gameMaster.TVnum++;
					gameMaster.tipOff = true;
					isOpen = true;
					SoundFX.PlayOneShot(unlockedBox, 1f);
					break;
				default:
					GetComponent<Renderer>().material.mainTexture = saferOpen;
					break;
			}
		}
	}
	void OnTriggerStay(Collider other){
		if(other.tag=="Player"){
			//if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
				//print("ClickTime: "+clickTime);
				clickTime++;
				if(clickTime>10){
					clickTime=0;
					openProgress++;
				}
			//}
		}
	}
}

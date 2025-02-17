using UnityEngine;
using System.Collections;

public class doorSwitich : MonoBehaviour {
	public Transform wall;
	private bool wallSwitich=false;
	
	public Material[] materials;
	public AudioClip gateSound;
	public AudioSource SoundFX;
	// Use this for initialization
	void Start () {
		//wall = GameObject.Find("Door");
		//renderer.materials=
		GetComponent<AudioSource>().volume=0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (materials.Length == 0)
            return;

        
	}
	void OnTriggerEnter(Collider other){
		if(other.tag=="Player"){
			print("player in trigger");
			if(wall!=null){
				
				if(!wallSwitich){
					wall.transform.localScale=new Vector3(0.5f,4.5f,5);
					wall.transform.position=new Vector3(wall.transform.position.x,wall.transform.position.y-2,0);
					wallSwitich=true;
					GetComponent<Renderer>().sharedMaterial = materials[1];//off
					//GetComponent<AudioSource>().PlayOneShot(gateSound);
					SoundFX.PlayOneShot(gateSound, 1f);
				}
				else{
					wall.transform.localScale=new Vector3(0.5f,0.5f,5);
					wall.transform.position=new Vector3(wall.transform.position.x,wall.transform.position.y+2,0);					
					wallSwitich=false;
					GetComponent<Renderer>().sharedMaterial = materials[0];//off
					//GetComponent<AudioSource>().PlayOneShot(gateSound);
					SoundFX.PlayOneShot(gateSound, 1f);
				}
			}
		}
	}
}

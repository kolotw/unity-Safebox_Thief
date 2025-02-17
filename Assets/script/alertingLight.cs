using UnityEngine;
using System.Collections;

public class alertingLight : MonoBehaviour {
	public float duration = 1.0F;
    public Color color0 = Color.red;
    public Color color1 = Color.white;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameMaster.isCounting){
		 	float t = Mathf.PingPong(Time.time, duration) / duration;
    	    GetComponent<Light>().color = Color.Lerp(color0, color1, t);
		}else{
			GetComponent<Light>().color=Color.white;
		}
	}
}

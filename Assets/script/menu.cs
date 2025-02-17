using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {
	void OnGUI() {
		GUI.Label(new Rect(Screen.width / 2 - 50, 190, 250, 100), "");
		GUI.Label(new Rect(Screen.width / 2 - 100, 20, 450, 100), "GAM 606 Instructor: Daniel Miller");
		GUI.Label(new Rect(Screen.width - 80, Screen.height - 60, 450, 100), "by Lung Kuo");
		GUI.Label(new Rect(Screen.width - 120, Screen.height - 40, 450, 100), "kolo.tw@gmail.com");
		GUI.Label(new Rect(Screen.width - 80, Screen.height - 20, 450, 100), "http://kolo.tw");
	}
	public void LV1(){
		SceneManager.LoadScene("LV1");
	}
	public void LV2()
	{
		SceneManager.LoadScene("LV2");
	}
	public void LV3()
	{
		SceneManager.LoadScene("LV3");
	}
	public void LV4()
	{
		SceneManager.LoadScene("LV4");
	}
	public void LV5()
	{
		SceneManager.LoadScene("LV5");
	}
}
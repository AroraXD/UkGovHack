using UnityEngine;
using System.Collections;

public class weatherControlScript : MonoBehaviour {

	public bool day = true;
	public bool night = false;

	public GameObject rain;


	// Use this for initialization
	void Start () {
	
		rain.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (day) {
			SetDay ();
		} else if (night) {
			SetNight ();
		}
		*/
	}


	public void SetDay()
	{
		GameObject.Find ("sun").GetComponent<Light> ().intensity = 1.5f;
		GameObject.Find ("headlightR").GetComponent<Light> ().intensity = 0;
		GameObject.Find ("headlightL").GetComponent<Light> ().intensity = 0;
		//GameObject.Find ("Main Camera").GetComponent<Skybox> ().enabled = false;
	}

	public void SetNight()
	{
		GameObject.Find ("sun").GetComponent<Light> ().intensity = 0;
		GameObject.Find ("headlightR").GetComponent<Light> ().intensity = 8;
		GameObject.Find ("headlightL").GetComponent<Light> ().intensity = 8;
		//GameObject.Find ("Main Camera").GetComponent<Skybox> ().enabled = true;
	}

	public void SetClouds()
	{
		GameObject.Find ("Main Camera").GetComponent<Skybox> ().enabled = true;
	}

	public void SetFog()
	{
		//GameObject.Find ("Main Camera").GetComponent<SetFog> ();
	}

	public void SetRain()
	{
		rain.SetActive (true);
	}
}

using UnityEngine;
using System.Collections;

public class weatherControlScript : MonoBehaviour {

	public bool day = true;
	public bool night = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (day) {
			SetDay ();
		} else if (night) {
			SetNight ();
		}
	}


	public void SetDay()
	{
		GameObject.Find ("sun").GetComponent<Light> ().intensity = 1;
		GameObject.Find ("headlightR").GetComponent<Light> ().intensity = 0;
		GameObject.Find ("headlightL").GetComponent<Light> ().intensity = 0;
		GameObject.Find ("Main Camera").GetComponent<Skybox> ().enabled = false;

	}

	public void SetNight()
	{
		GameObject.Find ("sun").GetComponent<Light> ().intensity = 0;
		GameObject.Find ("headlightR").GetComponent<Light> ().intensity = 8;
		GameObject.Find ("headlightL").GetComponent<Light> ().intensity = 8;
		GameObject.Find ("Main Camera").GetComponent<Skybox> ().enabled = true;
	}
}

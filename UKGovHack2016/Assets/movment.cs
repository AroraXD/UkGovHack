using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class movment : MonoBehaviour {

	public float fuel = 100f;
	public float speed = 10;
	// Use this for initialization

	void Start () {
		fuel = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (fuel > 0f) {

			if (Input.GetAxis ("Vertical") > 0f) {
				speed += 0.1f;
			} else if (Input.GetAxis ("Vertical") < 0f) {
				speed -= 0.05f;
			}

			fuel -= (0.0015f * speed);
			speed += 0.01f;
			if (speed < 0) {
				speed = 0;
			}

			transform.Translate ((Vector3.forward * (Input.GetAxis ("Horizontal")) * speed)/100); //move left/right 

			while (transform.position.z < -10) {
				transform.Translate (Vector3.forward);
			}

			while (transform.position.z > 10) {
				transform.Translate ((Vector3.forward * -1));
			}
		} else {
			fuel = 0;
			if (speed > 0) {
				speed -= speed*0.1f;
			}
		}

		transform.Translate (Vector3.left * speed * Time.deltaTime);
		GameObject.Find ("Fuel").GetComponent<Text> ().text = "Fuel: " + Mathf.FloorToInt (fuel);
		GameObject.Find ("Speed").GetComponent<Text> ().text = "Speed: " + Mathf.FloorToInt (speed);
	}

	public void resetFuel()
	{
		fuel = 100;
		speed = 5;
		//GetComponent("StartButton").enabled = false;
	}
}


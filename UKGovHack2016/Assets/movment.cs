using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class movment : MonoBehaviour {

	public float fuel = 100f;
	public float speed = 0;

	public GameObject fireworks;

	private bool start = false;
	// Use this for initialization

	void Start () {
		fuel = 0;
		fireworks.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (fuel > 0f) {

			if (Input.GetAxis ("Vertical") > 0f) {
				speed += 0.1f;
			} else if (Input.GetAxis ("Vertical") < 0f) {
				speed -= 0.05f;
			}

			fuel -= (0.0015f * speed * 0.5f);
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

			if (start == true) {
				//GameObject.Find ("Fireworks").SetActive(true); //fireworks set off when game ends
				fireworks.SetActive (true);

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
		start = true;
	}

	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		if (collision.relativeVelocity.magnitude > 2)
			GetComponent<AudioSource>().Play();
		print ("oh no");
	}
}


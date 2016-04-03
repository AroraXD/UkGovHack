using UnityEngine;
using System.Collections;

public class STARTGAME : MonoBehaviour {

	public GameObject button; // Assign in inspector

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void gameStart()
	{
		button.SetActive(false);
	}
}

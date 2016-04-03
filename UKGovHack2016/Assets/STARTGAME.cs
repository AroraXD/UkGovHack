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

/*
public BubbleController bubbleController;
private bool isShowing;

// Use this for initialization
void Start () {
	isShowing = false;
}

// Update is called once per frame
void Update () {
	if (Input.GetKeyDown("escape")) {
		isShowing = !isShowing;
	}

	menu.SetActive(isShowing);
	*/
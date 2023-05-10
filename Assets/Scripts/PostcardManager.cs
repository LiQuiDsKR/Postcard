using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostcardManager : MonoBehaviour {

	Sprite Basic;
	Sprite Luxury;
	Sprite Dice;
	Sprite Surprise;

	public string postType;
	public int honor;
	public int time;
	// Use this for initialization
	void Start () {
		switch (postType) {
		case "Basic":
			honor = 1;
			time = 30;
			break;
		case "Luxury":

			honor = 2;
			time = 30;
			break;
		case "Dice":
			
			break;
		case "Surprise":
			honor = 42;
			time = 5;
			break;
		}
	}

	void UIUpdate() {
		switch (postType) {
		case "Basic":
			transform.Find ("Img").GetComponent<Image> ().sprite = Basic;
			break;
		case "Luxury":
			transform.Find ("Img").GetComponent<Image> ().sprite = Luxury;
			break;
		case "Dice":
			transform.Find ("Img").GetComponent<Image> ().sprite = Dice;
			break;
		case "Surprise":
			transform.Find ("Img").GetComponent<Image> ().sprite = Surprise;
			break;
		}
		transform.Find ("Honor").GetComponent<Text> ().text = "+" + honor;
		transform.Find ("Time").GetComponent<Text> ().text = time + "일";
	}
	// Update is called once per frame
	void Update () {
		
	}
}

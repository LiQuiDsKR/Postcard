using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostcardManager : MonoBehaviour {

	public Sprite Basic;
	public Sprite Luxury;
	public Sprite Dice;
	public Sprite Surprise;

	int[] diceHonor = {822, 833, 833, 833, 833, 833, 833, 833, 144, 111, 111, 122, 100, 111, 111, 100, 111, 100, 100, 100, 100, 100, 89, 100, 89, 89, 89, 78, 89, 78, 78, 78, 78, 78, 67, 67, 67, 67, 67, 56, 56, 56, 56, 44, 44, 44, 33, 33, 22, 22, 11};

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
			DiceHonorRand ();
			break;
		case "Surprise":
			honor = 42;
			time = 5;
			break;
		}

		UIUpdate ();
	}

	void DiceHonorRand() {
		int rand = Random.Range (1, 10000 + 1);
		for (int i = 0; i < 51; i++) {
			if (rand <= diceHonor [i]) {
				honor = i + 1;
				break;
			}
			else {
				rand -= diceHonor [i];
			}
		}
		rand = Random.Range (1, 40 + 1);
		time = rand;
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
		transform.Find ("Time").GetComponent<Text> ().text = (time-1) + "일";
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			DiceHonorRand ();
		}
	}
}

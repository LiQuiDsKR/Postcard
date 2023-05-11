using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	int myHonor = 0;
	int mySlot = 42;
	int myPost = 0;
	int rewardRubble;
	int rewardLuna;
	float guildRankBonus;
	float BuildingBonus;
	float famousBonus;

	public GameObject Postcard;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			RewardCheck ();
			UIRefresh ();
		}
	}

	void RewardCheck() {
		myHonor = 0;
		for (int i = 0; i < transform.childCount; i++) {
			myHonor += transform.GetChild (i).GetComponent<PostcardManager> ().honor;
		}
		print (myHonor);
		if (GameObject.Find ("GuildRankDropdown").GetComponent<Dropdown> ().value == 0) {
			guildRankBonus = 1.01f;
		} else if (GameObject.Find ("GuildRankDropdown").GetComponent<Dropdown> ().value == 1) {
			guildRankBonus = 1.02f;
		} else if (GameObject.Find ("GuildRankDropdown").GetComponent<Dropdown> ().value == 2) {
			guildRankBonus = 1.03f;
		} else if (GameObject.Find ("GuildRankDropdown").GetComponent<Dropdown> ().value == 3) {
			guildRankBonus = 1.04f;
		} else if (GameObject.Find ("GuildRankDropdown").GetComponent<Dropdown> ().value == 4) {
			guildRankBonus = 1.05f;
		} else if (GameObject.Find ("GuildRankDropdown").GetComponent<Dropdown> ().value == 5) {
			guildRankBonus = 1.06f;
		}

		if (GameObject.Find ("BuildingRankDropdown").GetComponent<Dropdown> ().value == 0) {
			BuildingBonus = 1.025f;
		} else if (GameObject.Find ("BuildingRankDropdown").GetComponent<Dropdown> ().value == 1) {
			BuildingBonus = 1.05f;
		} else if (GameObject.Find ("BuildingRankDropdown").GetComponent<Dropdown> ().value == 2) {
			BuildingBonus = 1.075f;
		} else if (GameObject.Find ("BuildingRankDropdown").GetComponent<Dropdown> ().value == 3) {
			BuildingBonus = 1.1f;
		} else if (GameObject.Find ("BuildingRankDropdown").GetComponent<Dropdown> ().value == 4) {
			BuildingBonus = 1.125f;
		} else if (GameObject.Find ("BuildingRankDropdown").GetComponent<Dropdown> ().value == 5) {
			BuildingBonus = 1.15f;
		}

		if (GameObject.Find ("FamousToggle").GetComponent<Toggle> ().isOn == true) {
			famousBonus = 1.03f;
		} else {
			famousBonus = 1.0f;
		}

		rewardRubble = (int)(myHonor * 100 * guildRankBonus * BuildingBonus * famousBonus);
		rewardLuna = (int)Mathf.Floor(rewardRubble/42);
	}

	void UIRefresh() {


		GameObject.Find ("PostSlotText").GetComponent<Text> ().text = myPost + "/" + mySlot;
		GameObject.Find ("HonorText").GetComponent<Text> ().text = myHonor + "";
		GameObject.Find ("RewardLunaText").GetComponent<Text> ().text = rewardLuna + "";
		GameObject.Find ("RewardRubbleText").GetComponent<Text> ().text = rewardRubble + "";
	}

	public void OnBasic() {
		if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 0) {
			PurchasePost("Basic");
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 1) {
			for (int i = 0; i < 10; i++) {
				PurchasePost("Basic");
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 2) {
			for (int i = 0; i < 100; i++) {
				PurchasePost("Basic");
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 3) {
			for (int i = 0; i < (mySlot - myPost); i++) {
				PurchasePost("Basic");
			}
		}
	}

	public void OnLuxury() {
		if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 0) {
			PurchasePost("Luxury");
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 1) {
			for (int i = 0; i < 10; i++) {
				PurchasePost("Luxury");
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 2) {
			for (int i = 0; i < 100; i++) {
				PurchasePost("Luxury");
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 3) {
			for (int i = 0; i < (mySlot - myPost); i++) {
				PurchasePost("Luxury");
			}
		}
	}

	public void OnDice() {
		if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 0) {
			PurchasePost("Dice");
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 1) {
			for (int i = 0; i < 10; i++) {
				PurchasePost("Dice");
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 2) {
			for (int i = 0; i < 100; i++) {
				PurchasePost("Dice");
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 3) {
			for (int i = 0; i < (mySlot - myPost); i++) {
				PurchasePost("Dice");
			}
		}
	}

	public void OnSurprise() {
		if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 0) {
			PurchasePost("Surprise");
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 1) {
			for (int i = 0; i < 10; i++) {
				PurchasePost("Surprise");
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 2) {
			for (int i = 0; i < 100; i++) {
				PurchasePost("Surprise");
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 3) {
			for (int i = 0; i < (mySlot - myPost); i++) {
				PurchasePost("Surprise");
			}
		}
	}

	void PurchasePost(string type) {
		GameObject temp = Instantiate (Postcard, GameObject.Find ("PostSlotContent").transform);
		temp.GetComponent<PostcardManager> ().postType = type;
	}

}

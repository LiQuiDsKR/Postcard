using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int myHonor = 0;
	public int mySlot = 42;
	public int myPost = 0;
	public int myRubble = 0;
	public int myLuna = 0;
	public int rewardRubble;
	public int rewardLuna;
	float guildRankBonus;
	float BuildingBonus;
	float famousBonus;
	int expandCost = 10000;
	int dday = 0;

	public GameObject Postcard;
	private GameObject Content;
	// Use this for initialization
	void Start () {
		Content = GameObject.Find ("PostSlotContent");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void RewardCheck() {
		myHonor = 0;
		for (int i = 0; i < Content.transform.childCount; i++) {
			myHonor += Content.transform.GetChild (i).GetComponent<PostcardManager> ().honor;
		}
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
		} else if (GameObject.Find ("GuildRankDropdown").GetComponent<Dropdown> ().value == 6) {
			guildRankBonus = 1.0f;
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
		} else if (GameObject.Find ("BuildingRankDropdown").GetComponent<Dropdown> ().value == 6) {
			BuildingBonus = 1.0f;
		}

		if (GameObject.Find ("FamousToggle").GetComponent<Toggle> ().isOn == true) {
			famousBonus = 1.03f;
		} else {
			famousBonus = 1.0f;
		}

		rewardRubble = (int)(myHonor * 100 * guildRankBonus * BuildingBonus * famousBonus);
		rewardLuna = (int)Mathf.Floor(rewardRubble/4200);
	}

	public void OnGetLuna() {
		myLuna += 1000;
		UIRefresh ();
	}

	public void OnGetRubble() {
		myRubble += 1000000;
		UIRefresh ();
	}

	public void OnNextDay() {
		myRubble += rewardRubble;
		myLuna += rewardLuna;

		List<int> removeList = new List<int>{};
		for (int i = 0; i < Content.transform.childCount; i++) {
			Content.transform.GetChild (i).GetComponent<PostcardManager> ().time -=1;
			Content.transform.GetChild (i).GetComponent<PostcardManager> ().UIRefresh ();
			if (Content.transform.GetChild (i).GetComponent<PostcardManager> ().time <= 0) {
				removeList.Add (i);
			}
		}
		for (int i = 0; i < removeList.Count; i++) {
			GameObject.DestroyImmediate (Content.transform.GetChild (removeList [i]).gameObject);
			for (int j = i; j < removeList.Count; j++) {
				removeList [j] -= 1;
			}
		}

		dday += 1;

		UIRefresh ();
	}

	public void UIRefresh() {
		myPost = Content.transform.childCount;

		RewardCheck ();

		GameObject.Find ("LunaText").GetComponent<InputField> ().text = myLuna + "";
		GameObject.Find ("RubbleText").GetComponent<InputField> ().text = myRubble + "";
		GameObject.Find ("PostSlotText").GetComponent<Text> ().text = myPost + "/" + mySlot;
		GameObject.Find ("HonorText").GetComponent<Text> ().text = myHonor + "";
		GameObject.Find ("RewardLunaText").GetComponent<Text> ().text = rewardLuna + "";
		GameObject.Find ("RewardRubbleText").GetComponent<Text> ().text = rewardRubble + "";
		GameObject.Find ("Dday").GetComponent<Text> ().text = "Day " + dday;
	}

	public void OnEndEditLuna() {
		myLuna = int.Parse (GameObject.Find ("LunaText").GetComponent<InputField> ().text);
	}

	public void OnEndEditRubble() {
		myRubble = int.Parse (GameObject.Find ("RubbleText").GetComponent<InputField> ().text);
	}

	public void OnExpand() {
		if (myRubble >= expandCost) {
			myRubble -= expandCost;
			expandCost += 10000;
			mySlot += 10;

			UIRefresh ();
		}
	}

	public void OnBasic() {
		if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 0) {
			if ((mySlot - myPost) >= 1) {
				if (myRubble >= 2500) {
					PurchasePost("Basic");
					myRubble -= 2500;
				}
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 1) {
			if ((mySlot - myPost) >= 10) {
				if (myRubble >= 25000) {
					for (int i = 0; i < 10; i++) {
						PurchasePost("Basic");
						myRubble -= 2500;
					}
				}
			}

		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 2) {
			if ((mySlot - myPost) >= 100) {
				if (myRubble >= 250000) {
					for (int i = 0; i < 100; i++) {
						PurchasePost("Basic");
						myRubble -= 2500;
					}
				}

			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 3) {
			for (int i = 0; i < (mySlot - myPost); i++) {
				if (myRubble >= 2500) {
					PurchasePost("Basic");
					myRubble -= 2500;
				}
			}
		}
		UIRefresh ();
	}

	public void OnLuxury() {
		if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 0) {
			if ((mySlot - myPost) >= 1) {
				if (myLuna >= 10) {
					PurchasePost("Luxury");
					myLuna -= 10;
				}
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 1) {
			if ((mySlot - myPost) >= 10) {
				if (myLuna >= 100) {
					for (int i = 0; i < 10; i++) {
						PurchasePost("Luxury");
						myLuna -= 10;
					}
				}
			}

		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 2) {
			if ((mySlot - myPost) >= 100) {
				for (int i = 0; i < 100; i++) {
					PurchasePost("Luxury");
					myLuna -= 10;
				}
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 3) {
			for (int i = 0; i < (mySlot - myPost); i++) {
				if (myLuna >= 10) {
					PurchasePost("Luxury");
					myLuna -= 10;
				}
			}
		}
		UIRefresh ();
	}

	public void OnDice() {
		if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 0) {
			if ((mySlot - myPost) >= 1) {
				if (myLuna >= 50) {
					PurchasePost("Dice");
					myLuna -= 50;
				}
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 1) {
			if ((mySlot - myPost) >= 10) {
				if (myLuna >= 500) {
					for (int i = 0; i < 10; i++) {
						PurchasePost("Dice");
						myLuna -= 50;
					}
				}
			}

		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 2) {
			if ((mySlot - myPost) >= 100) {
				if (myLuna >= 5000) {
					for (int i = 0; i < 100; i++) {
						PurchasePost("Dice");
						myLuna -= 50;
					}
				}
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 3) {
			for (int i = 0; i < (mySlot - myPost); i++) {
				if (myLuna >= 50) {
					PurchasePost("Dice");
					myLuna -= 50;
				}
			}
		}
		UIRefresh ();
	}

	public void OnSurprise() {
		if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 0) {
			if ((mySlot - myPost) >= 1) {
				if (myRubble >= 42000) {
					PurchasePost("Surprise");
					myRubble -= 42000;
				}
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 1) {
			if ((mySlot - myPost) >= 10) {
				if (myRubble >= 420000) {
					for (int i = 0; i < 10; i++) {
						PurchasePost("Surprise");
						myRubble -= 42000;
					}
				}
			}

		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 2) {
			if ((mySlot - myPost) >= 100) {
				if (myRubble >= 4200000) {
					for (int i = 0; i < 100; i++) {
						PurchasePost("Surprise");
						myRubble =- 42000;
					}
				}
			}
		} else if (GameObject.Find ("PostcardPurchaseDropdown").GetComponent<Dropdown> ().value == 3) {
			for (int i = 0; i < (mySlot - myPost); i++) {
				if (myRubble >= 42000) {
					PurchasePost("Surprise");
					myRubble -= 42000;
				}
			}
		}
		UIRefresh ();
	}

	void PurchasePost(string type) {
		GameObject temp = Instantiate (Postcard, GameObject.Find ("PostSlotContent").transform);
		temp.GetComponent<PostcardManager> ().postType = type;
	}

}

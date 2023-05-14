using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadManager : MonoBehaviour {
	public string str = "";
	public string strFin;
	public int intFin;
	public string parentName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ResultRefresh() {
		transform.Find ("Result").Find ("KeypadText").gameObject.GetComponent<Text> ().text = str;
	}

	public void Key1() {
		str = str + "1";
		ResultRefresh ();
	}

	public void Key2() {
		str = str + "2";
		ResultRefresh ();
	}

	public void Key3() {
		str = str + "3";
		ResultRefresh ();
	}

	public void Key4() {
		str = str + "4";
		ResultRefresh ();
	}

	public void Key5() {
		str = str + "5";
		ResultRefresh ();
	}

	public void Key6() {
		str = str + "6";
		ResultRefresh ();
	}

	public void Key7() {
		str = str + "7";
		ResultRefresh ();
	}

	public void Key8() {
		str = str + "8";
		ResultRefresh ();
	}

	public void Key9() {
		str = str + "9";
		ResultRefresh ();
	}

	public void Key0() {
		str = str + "0";
		ResultRefresh ();
	}

	public void KeyBack() {
		if (str.Length == 1) {
			str = "";
		}
		if (str.Length >= 2) {
			str = str.Substring (0, str.Length - 1);
		}
		ResultRefresh ();
	}

	public void OnClose() {
		str = "";
		strFin = "";
		intFin = 0;
		this.gameObject.SetActive (false);
	}

	public void KeyEnter() {
		strFin = str;
		intFin = int.Parse (strFin);
		if (parentName == "RubbleText") {
			GameObject.Find ("System").GetComponent<GameManager> ().myRubble = intFin;
		}
		if (parentName == "LunaText") {
			GameObject.Find ("System").GetComponent<GameManager> ().myLuna = intFin;
		}
		if (parentName == "PostSlotText") {
			if (intFin >= 40) {
				strFin = str.Substring (0, str.Length - 1);
				strFin = strFin + "2";
				intFin = int.Parse (strFin);
				GameObject.Find ("System").GetComponent<GameManager> ().mySlot = intFin;
			}
		}
		GameObject.Find ("System").GetComponent<GameManager> ().UIRefresh ();

		Destroy (this.gameObject);
	}
}

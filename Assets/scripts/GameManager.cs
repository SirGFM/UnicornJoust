using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Player[] players;

	public UnityEngine.UI.Text txtWinner;
	public UnityEngine.UI.Text countdown;

	public string level = "game";


	public bool OnePlayerAlive(){
		int maxPlayers = players.Length;
		int deadPlayers = 0;
		foreach(Player player in players){
			if(!player.isAlive())
				deadPlayers++;			
		}
		if(deadPlayers >= maxPlayers-1)
			return true;
		return false;
	}

	private IEnumerator StartMatch(){
		float time = 0.0f;
		countdown.gameObject.SetActive(true);
		//Part 0: Creating standard values
		RectTransform r = countdown.gameObject.GetComponent<RectTransform>();
		Vector3 ctSta = new Vector3(.75f,.75f,.75f);
		Vector3 ctEnd = new Vector3(1.75f,1.75f,1.75f);
		//Part 1: Ready
		while (time < 1f) {
			r.localScale = Vector3.Lerp(ctSta, ctEnd, TimeToPerc(0f,1f,time));
			time += Time.deltaTime;
			yield return null;
		}
		//Part 2: 3
		countdown.text = "3";
		while (time < 2f) {
			r.localScale = Vector3.Lerp(ctSta, ctEnd, TimeToPerc(1f,2f,time));
			time += Time.deltaTime;
			yield return null;
		}
		//Part 3: 2
		countdown.text = "2";
		while (time < 3f) {
			r.localScale = Vector3.Lerp(ctSta, ctEnd, TimeToPerc(2f,3f,time));
			time += Time.deltaTime;
			yield return null;
		}
		//Part 4: 1
		countdown.text = "1";
		while (time < 4f) {
			r.localScale = Vector3.Lerp(ctSta, ctEnd, TimeToPerc(3f,4f,time));
			time += Time.deltaTime;
			yield return null;
		}
		//Part 5: GO
		foreach(Player player in players)
			player.canControl = true;
		countdown.text = "GO!";
		while (time < 5f) {
			r.localScale = Vector3.Lerp(ctSta, ctEnd, TimeToPerc(4f,5f,time));
			time += Time.deltaTime;
			yield return null;
		}
		//Remove countdown
		countdown.gameObject.SetActive(false);
	}
 
	public void Restart(){
		Application.LoadLevel(level);
	}


	public float TimeToPerc(float start, float end, float now){
		return (now-start)/(end-start);
	}



	/** === UNITY EVENTS ======================================================== */

	void Awake () {
		players = GameObject.FindObjectsOfType<Player>();
	}

	void Start(){
		StartCoroutine("StartMatch");
	}
	
	void Update () {
		if(OnePlayerAlive()){
			txtWinner.gameObject.SetActive(true);
			foreach(Player player in players)
				if(player.isAlive())
					txtWinner.text = player.name + " WON!\n (Press R to restart)";			
		}

		if(Input.GetKey(KeyCode.R)){
			Restart();
		}
	}

}

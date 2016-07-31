using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Player[] players;

	public UnityEngine.UI.Text txtWinner;

	private bool matchOver;




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

	public void Restart(){
		Application.LoadLevel("tico-scene");
	}

	/** === UNITY EVENTS ======================================================== */

	void Awake () {
		players = GameObject.FindObjectsOfType<Player>();
		matchOver = false;
	}
	
	void Update () {
		if(OnePlayerAlive()){
			matchOver = true;
			txtWinner.gameObject.SetActive(true);
			foreach(Player player in players)
				if(!player.isAlive())
					txtWinner.text = player.name + " WON!\n (Press R to restart)";			
		}

		if(Input.GetKey(KeyCode.R)){
			Restart();
		}
	}

}

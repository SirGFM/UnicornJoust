using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Sprite ready;
	public Sprite number_1;
	public Sprite number_2;
	public Sprite number_3;
	public Sprite go;
	public Sprite p1_win;
	public Sprite p2_win;
	public Sprite p3_win;
	public Sprite p4_win;

	/* Maximum scale factor during initialization */
	public float maxScale = 10.0f;
	/* Scale factor of the "you win" text */
	public float winScale = 5.0f;

	private Player[] _players;
	private SpriteRenderer _spr;
	private Sprite[] _initSprites;

	public string level = "game";

	public bool OnePlayerAlive(){
		bool isOneAlive = false;
		foreach (Player player in this._players) {
			if (!isOneAlive && player.isAlive()) {
				isOneAlive = true;
			}
			else if (isOneAlive && player.isAlive()){
				return false;
			}
		}
		return true;
	}

	private IEnumerator StartMatch(){
		int i = 0;
		while (i < this._initSprites.Length) {
			float time = 0.0f;
			this._spr.sprite = this._initSprites[i];
			while (time < 1.0f) {
				this.transform.localScale = Vector3.one * this.maxScale * time;
				time += Time.fixedDeltaTime;
				yield return null;
			}
			
			if (i == this._initSprites.Length - 1) {
				foreach(Player player in this._players) {
					player.canControl = true;
				}
			}
			i++;
		}
		this._spr.enabled = false;
	}
 
	public void Restart(){
		Application.LoadLevel(level);
	}

	/** === UNITY EVENTS ======================================================== */

	void Awake () {
		this._players = GameObject.FindObjectsOfType<Player>();
		this._spr = this.gameObject.GetComponent<SpriteRenderer>();

		this._initSprites = new Sprite[5];
		this._initSprites[0] = this.ready;
		this._initSprites[1] = this.number_3;
		this._initSprites[2] = this.number_2;
		this._initSprites[3] = this.number_1;
		this._initSprites[4] = this.go;
	}

	void Start(){
		StartCoroutine("StartMatch");
	}

	void Update () {
		if(OnePlayerAlive()){
			if (this._spr.enabled == false) {
				this._spr.enabled = true;
				this.transform.localScale = Vector3.one * this.winScale;
				foreach (Player player in this._players) {
					if (!player.isAlive()) {
						continue;
					}
					switch (player.controller) {
						case Player.enController.CONTROLLER_1: this._spr.sprite = this.p1_win; break;
						case Player.enController.CONTROLLER_2: this._spr.sprite = this.p2_win; break;
						case Player.enController.CONTROLLER_3: this._spr.sprite = this.p3_win; break;
						case Player.enController.CONTROLLER_4: this._spr.sprite = this.p4_win; break;
					}
					break;
				}
			}
		}

		if(Input.GetKey(KeyCode.R)){
			Restart();
		}
	}
}

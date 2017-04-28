using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*********************************************************/
/* Programme entièrement implémenté par Vincent Leclerc */
/********************************************************/

public class Tile : MonoBehaviour {

	public int tileLine;
	public int tileColumn;
	public GameObject playerObject;
	private PlayerController playerController;

	public bool isObstacle;

	// Use this for initialization
	void Start () {
		playerObject = GameObject.Find ("PlayerPrefab(Clone)");
		playerController = playerObject.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){

		if (IsNeighbour(playerController.playerLine, playerController.playerColumn) && !isObstacle){
			playerController.Move (tileLine, tileColumn);
		}
		
	}

	public bool IsNeighbour(int i, int j){
		bool result = false;

		if ((i == tileLine - 1 || i == tileLine || i == tileLine + 1) &&
		    (j == tileColumn - 1 || j == tileColumn || j == tileColumn + 1)) {
			result = true;
		}

		return result;
	}
}

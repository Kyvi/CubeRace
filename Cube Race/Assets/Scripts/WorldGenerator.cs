using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*********************************************************/
 /* Programme entièrement implémenté par Vincent Leclerc */
 /********************************************************/


public class WorldGenerator : MonoBehaviour {


	public int nbTurn;

	public bool isGame;

	public int nbLine;
	public int nbColumn;
	public int tileSize ;
	public int distanceSize;

	public GameObject fixedObstaclePrefab;
	public GameObject randomObstaclePrefab;
	public GameObject emptyPrefab;
	public GameObject playerPrefab;
	public GameObject iaPrefab;
	public GameObject endPrefab;

	private GameObject playerGObj;
	private GameObject iaGOjb;

	private PlayerController playerController;
	private AI ai;

	public int fixedObstacleChance;
	public int randomObstacleChance;

	public int[,] mapState;
	public GameObject [,] gameObjectState;

	public int startLine;
	public int startColumn;

	public int aiStartLine;
	public int aiStartColumn;

	public int endLine;
	public int endColumn;


	public float timer = 5.0f;
	public float nextTimer = 1.0f;

	// Use this for initialization
	void Start()
	{
		Time.timeScale = 1;
		timer = Time.time + 5f;
		mapState = new int[nbLine,nbColumn];

		gameObjectState = new GameObject[nbLine,nbColumn];
		CreateMap ();
			
	}

	public void CreateMap(){

		GameObject playerGO = Instantiate (playerPrefab, new Vector3(startColumn,startLine,0), transform.rotation,transform.parent);

			GameObject iaGO = Instantiate (iaPrefab, new Vector3 (aiStartColumn, aiStartLine, 0), transform.rotation, transform.parent);
		if (isGame) {
			iaGO.transform.localScale = new Vector3 (tileSize, tileSize, 1);
		}

			GameObject endGO = Instantiate (endPrefab, new Vector3(endColumn,endLine,0), transform.rotation,transform.parent);

		playerGO.transform.localScale = new Vector3(tileSize, tileSize, 1);
		endGO.transform.localScale = new Vector3(tileSize, tileSize, 1);

		for (int i = 0; i < nbLine; i++)
		{
			for (int j = 0; j < nbColumn; j++)
			{
				//if (IsAcceptable(i,j)) {

					Vector3 pos = new Vector3 (); 
					pos.x =  j * distanceSize; 
					pos.y = i * distanceSize ; 

					float value = Random.value * 100;
					GameObject toInstantiate = emptyPrefab;

				if (value <= fixedObstacleChance && IsAcceptable(i,j)) {
						toInstantiate = fixedObstaclePrefab;
						mapState [i,j] = 1;	
					} else{
						mapState [i,j] = 0;	
					}

					GameObject g = Instantiate (toInstantiate, pos, transform.rotation,transform.parent);
					g.GetComponent<Tile> ().tileLine = i;
					g.GetComponent<Tile> ().tileColumn = j;
						
					gameObjectState [i,j] = g;
					gameObjectState[i, j].transform.localScale = new Vector3(tileSize, tileSize, 1);

				//}
			}
		}


	}

	public void Refresh(int i, int j){
		if (mapState [i, j] == 2) {
			mapState [i, j] = 0;
			Destroy (gameObjectState [i, j]);
			Vector3 pos = new Vector3 (); 
			pos.x =  j * distanceSize; 
			pos.y = i * distanceSize ; 
			GameObject g = Instantiate (emptyPrefab, pos, transform.rotation,transform.parent);
			g.GetComponent<Tile> ().tileLine = i;
			g.GetComponent<Tile> ().tileColumn = j;

			gameObjectState [i,j] = g;
			gameObjectState[i, j].transform.localScale = new Vector3(tileSize, tileSize, 1);

		}
	}

	public void CreateRandomObstacles(){

		for (int i = 0; i < nbLine; i++) {
			for (int j = 0; j < nbColumn; j++) {
				Refresh (i,j);
				if (mapState [i, j] != 1 && IsAcceptable(i,j)) {


					float value = Random.value * 100;
					GameObject toInstantiate = randomObstaclePrefab;

					if (value <= randomObstacleChance) {
						Vector3 pos = new Vector3 (); 
						pos.x = j * distanceSize; 
						pos.y = i * distanceSize; 
						mapState [i, j] = 2;	
						Destroy (gameObjectState [i, j]);
						GameObject g = Instantiate (toInstantiate, pos, transform.rotation, transform.parent);
						g.GetComponent<Tile> ().tileLine = i;
						g.GetComponent<Tile> ().tileColumn = j;

						gameObjectState [i, j] = g;
						gameObjectState [i, j].transform.localScale = new Vector3 (tileSize, tileSize, 1);

					} 

				}
			}
		}
	}

	public bool IsAcceptable(int i, int j){
		bool result = true;

		playerGObj = GameObject.Find ("PlayerPrefab(Clone)");
		playerController = playerGObj.GetComponent<PlayerController> ();

		if (isGame) {
			iaGOjb = GameObject.Find ("AIPrefab(Clone)");
			ai = iaGOjb.GetComponent<AI> ();
			if ((i == playerController.playerLine && j == playerController.playerColumn) || (i == ai.aiLine && j == ai.aiColumn) || (i == endLine && j == endColumn)) {
				result = false;		
			}
		} else {

			if ((i == playerController.playerLine && j == playerController.playerColumn) || (i == endLine && j == endColumn)) {
				result = false;		
			}
		}
		return result;
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}

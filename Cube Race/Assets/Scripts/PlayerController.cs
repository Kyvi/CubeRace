using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



/*********************************************************/
/* Programme entièrement implémenté par Vincent Leclerc */
/********************************************************/

public class PlayerController : MonoBehaviour {

	public GameObject Generator;
	private WorldGenerator worldGenerator;

	public GameObject aiGO;
	private AI ai;

	public GameObject path;
	public GameObject bestPath;
	private bool finish = false;

	public int playerLine;
	public int playerColumn;

	public Text bestScoreText;
	public Text winText;
	public Text turnText;
	public int bestScore;

	// Use this for initialization
	void Start () {
		Generator = GameObject.Find("Grid");
		worldGenerator = Generator.GetComponent<WorldGenerator> ();
			aiGO = GameObject.Find ("AIPrefab(Clone)");
			ai = aiGO.GetComponent<AI> ();

		playerLine = worldGenerator.startLine;
		playerColumn = worldGenerator.startColumn;
		bestScoreText = GameObject.Find ("BestScore").GetComponent<Text>();;
		turnText = GameObject.Find ("Turn").GetComponent<Text>();;
		winText = GameObject.Find ("Win").GetComponent<Text>();;
	}

	public void Move(int i, int j){
		if (worldGenerator.isGame) {
			ai.MoveAI ();
		}
		int dist = worldGenerator.distanceSize;
		int size = worldGenerator.tileSize;

		GameObject pathGO = Instantiate (path, new Vector3(playerColumn * dist,playerLine * dist,0), transform.rotation,transform.parent);
		pathGO.transform.localScale = new Vector3(size, size, 1);

		worldGenerator.timer = Time.time + worldGenerator.nextTimer;
		playerLine = i;
		playerColumn = j;
		transform.position = new Vector3 (j * dist, i * dist, 0);
		UpdateCamera ();

		worldGenerator.nbTurn = worldGenerator.nbTurn + 1;
		if (worldGenerator.isGame) {
			worldGenerator.CreateRandomObstacles ();
		}
	}

	// Update is called once per frame
	void Update () {

		turnText.text = "Turn : " + worldGenerator.nbTurn;

		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (0);
		}
		if (Time.time > worldGenerator.timer) {
			Move (playerLine, playerColumn);
		} else {

			if (worldGenerator.isGame) {
				if (playerLine == worldGenerator.endLine && playerColumn == worldGenerator.endColumn && !finish) {
					if (ai.aiLine == worldGenerator.endLine && ai.aiColumn == worldGenerator.endColumn && !finish) {
						finish = true;
						Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, worldGenerator.nbLine / 2, -10);
						Camera.main.orthographicSize = Mathf.Max (10, worldGenerator.nbLine - 10);

						winText.text = "DRAW";
						Time.timeScale = 0;
					}
				}

				if (ai.aiLine == worldGenerator.endLine && ai.aiColumn == worldGenerator.endColumn && !finish) {
					finish = true;
					Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, worldGenerator.nbLine / 2, -10);
					Camera.main.orthographicSize = Mathf.Max (10, worldGenerator.nbLine - 10);

					winText.text = "YOU LOSE";
					Time.timeScale = 0;
				}

				if (playerLine == worldGenerator.endLine && playerColumn == worldGenerator.endColumn && !finish) {
					finish = true;
					Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, worldGenerator.nbLine / 2, -10);
					Camera.main.orthographicSize = Mathf.Max (10, worldGenerator.nbLine - 10);

					winText.text = "YOU WIN";
					Time.timeScale = 0;
				}

			}

			if (playerLine == worldGenerator.endLine && playerColumn == worldGenerator.endColumn && !finish) {

				finish = true;
				Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, worldGenerator.nbLine/2, -10);
				Camera.main.orthographicSize = Mathf.Max(10,worldGenerator.nbLine-10);

				if (!worldGenerator.isGame) {
					Graph g = new Graph (worldGenerator.nbLine, worldGenerator.nbColumn);
					for (int i = 0; i < g.graphNodes.Count; i++) {
						g.graphNodes [i].hScore = ai.Hcost (g.graphNodes [i]);
					}
					Node start = g.graphNodes [ai.aiLine * worldGenerator.nbColumn + ai.aiColumn];
					Node end = g.graphNodes [worldGenerator.endLine * worldGenerator.nbColumn + worldGenerator.endColumn];

					start.gScore = 0;
					start.fScore = start.hScore;

					ai.Astar (g, start, end);

					for (int i = 0; i < ai.optimalPath.Count; i++) {
						//maxScore = maxScore + (1-tabMushrooms [optimalPath [i]]);
						Vector3 pos = new Vector3 ();
						pos.x = ai.optimalPath [i].y * worldGenerator.distanceSize; 
						pos.y = ai.optimalPath [i].x * worldGenerator.distanceSize; 
						GameObject bestPathGO = Instantiate (bestPath, pos, transform.rotation, transform.parent);
						bestPathGO.transform.localScale = new Vector3 (worldGenerator.tileSize, worldGenerator.tileSize, 1);
					}

					bestScore = ai.optimalPath.Count;
					bestScoreText.text = "Best Score : " + bestScore;
					if (worldGenerator.nbTurn <= bestScore) {
						winText.text = "YOU WIN";
					} else {
						winText.text = "YOU LOSE";
					}
				} 
				else {
					winText.text = "YOU WIN";
					Time.timeScale = 0;
				}
					
				/*if (worldGenerator.maxScore > nbMush) {
					winlose.text = "You Lose !";
				} else {
					winlose.text = "You Win !";
				}*/
			}
		}
	}

	public void UpdateCamera(){
		Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, transform.position.y, -10);
	}



}

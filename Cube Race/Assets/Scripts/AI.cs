using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*********************************************************/
/* Programme entièrement implémenté par Vincent Leclerc */
/********************************************************/

public class AI : MonoBehaviour {


	public GameObject Generator;
	private WorldGenerator worldGenerator;

	public GameObject aiPath;

	public List<Vector2> optimalPath = new List<Vector2>();

	public int aiLine;
	public int aiColumn;

	// Use this for initialization
	void Start () {
		
		Generator = GameObject.Find("Grid");
		worldGenerator = Generator.GetComponent<WorldGenerator> ();

		aiLine = worldGenerator.aiStartLine;
		aiColumn = worldGenerator.aiStartColumn;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveAI (){
		Graph g = new Graph (worldGenerator.nbLine, worldGenerator.nbColumn);
		for (int i = 0; i < g.graphNodes.Count; i++) {
			g.graphNodes[i].hScore = Hcost(g.graphNodes[i]);
		}
		Node start = g.graphNodes[aiLine * worldGenerator.nbColumn  + aiColumn];
		Node end = g.graphNodes [worldGenerator.endLine * worldGenerator.nbColumn + worldGenerator.endColumn];

		start.gScore = 0;
		start.fScore = start.hScore;

		Astar (g, start, end);

		int dist = worldGenerator.distanceSize;
		int size = worldGenerator.tileSize;

		GameObject pathGO = Instantiate (aiPath, new Vector3(aiColumn * dist,aiLine * dist,0), transform.rotation,transform.parent);
		pathGO.transform.localScale = new Vector3(size, size, 1);

		aiLine = (int)optimalPath[optimalPath.Count-1].x;
		aiColumn = (int)optimalPath[optimalPath.Count-1].y;

		transform.position = new Vector3 (aiColumn * dist, aiLine * dist, 0);

		optimalPath.Clear ();

		
	}

	// Algorithme A*

	public void Astar(Graph g, Node start, Node end){
		List<Node> open = new List<Node> ();
		List<Node> closed = new List<Node>();
		Node s = null;
		open.Add (start);
		bool hasStopped = false;
		while (true) {
			if (open.Count == 0) {
				hasStopped = true;
				break;
			}
			Node current = MinFCost (open);
			open.Remove (current);
			closed.Add (current);
			if ((current.line == end.line) && (current.column == end.column)) {
				s = current;
				break;
			}

			for (int i = 0; i < current.arcs.Count; i++) {
				Node neighbour = current.arcs [i].finish;
				if (!IsObstacle (neighbour) && !closed.Contains (neighbour)) {
					if (isShorter (current, neighbour) || !open.Contains (neighbour)) {
						neighbour.gScore = current.gScore + 10;
						neighbour.fScore = neighbour.hScore + neighbour.gScore;
						neighbour.father = current;
						if (!open.Contains (neighbour)) {
							open.Add (neighbour);
						}
					}
				}
			}
		}
		if (!hasStopped) {
			while (s != start) {
				optimalPath.Add (new Vector2 (s.line, s.column));
				s = s.father;
			}
		} else {
			Advance ();
		}

	}

	public void Advance(){
		for (int i = -1; i < 2; i++) {
			if (aiLine == worldGenerator.endLine) {
				break;
			}
			if (aiColumn + i < 0 || aiColumn + i > worldGenerator.nbColumn-1) {
				continue;
			}
			if (worldGenerator.mapState [aiLine + 1, aiColumn + i] == 0) {
				optimalPath.Add (new Vector2 (aiLine + 1, aiColumn + i));
				Debug.Log ("changed");
				break;
			} else {
				if (i == 1) {
				optimalPath.Add (new Vector2 (aiLine, aiColumn));
					Debug.Log ("Nope");
					break;
				}
			}
		}
	}

	public Node MinFCost(List<Node> open){
		Node result = open[0];
		int cost = result.fScore;

		for (int i = 1; i < open.Count; i++) {
			if (cost > open [i].fScore) {
				cost = open [i].fScore;
				result = open [i];
			}
		}

		return result;
		
	}

	public bool IsObstacle(Node node){
		bool result = false;
		if (worldGenerator.mapState [node.line, node.column] != 0) {
			result = true;
		}
		return result;
	}

	public int Hcost(Node node){
		return Mathf.Max(Mathf.Abs(worldGenerator.endLine - node.line),
			Mathf.Abs(worldGenerator.endColumn - node.column));
	}


	public bool isShorter(Node current, Node neighbour){
		bool result = false;

		if (neighbour.fScore == -1) {
			result = true;
		} else {
			if (neighbour.fScore > current.fScore + 10) {
				result = true;
			}
		}

		return result;
	}
}

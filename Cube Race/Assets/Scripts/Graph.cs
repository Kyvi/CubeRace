using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*********************************************************/
/* Programme entièrement implémenté par Vincent Leclerc */
/********************************************************/

public class Graph {

	public int nbLine;
	public int nbColumn;

	/// <summary>
	/// The graph nodes.
	/// </summary>
	public List<Node> graphNodes;


	public Graph(int nbL, int nbC){
		graphNodes = new List<Node> ();
		nbLine = nbL;
		nbColumn = nbC;
		int tot = nbL * nbC;
		createGraph ();
	}


	public void createGraph(){
		int tot = nbLine * nbColumn;
		for (int i = 0; i < tot; i++) {
			graphNodes.Add (new Node (i, i / nbColumn, i % nbColumn));
		}

		for (int i = 0; i < tot; i++) {
			Node node = graphNodes [i];

			if (node.line < nbLine - 1) {
				int id = node.id + nbColumn;

				node.addArc (graphNodes [id], 10);
				if (node.column > 0) {
					node.addArc (graphNodes [id-1], 10);
				}
				if (node.column < nbColumn - 1) {
					node.addArc (graphNodes [id+1], 10);
				}


			}

			if (node.line > 0) {
				int id = node.id - nbColumn;
				node.addArc (graphNodes [id], 10);
				if (node.column > 0) {
					node.addArc (graphNodes [id-1], 10);
				}
				if (node.column < nbColumn - 1) {
					node.addArc (graphNodes [id+1], 10);
				}

			}
			if (node.column < nbColumn - 1 ) {
				int id = node.id +1;
				//if (map [id] != int.MaxValue) {
				node.addArc (graphNodes [id], 10);

			}
			if (node.column > 0) {
				int id = node.id - 1;
				//if (map [id] != int.MaxValue) {
				node.addArc (graphNodes [id], 10);

			}



		}

	}


}
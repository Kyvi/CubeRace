using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*********************************************************/
/* Programme entièrement implémenté par Vincent Leclerc */
/********************************************************/

public class Node{

	/// <summary>
	/// The identifier of the node, here it's the position of the room its assigned to.
	/// </summary>
	public int id;

	/// <summary>
	/// The score of the Node
	/// </summary>
	public int fScore;

	public int gScore;
	public int hScore;

	public List<Arc> arcs;


	/// <summary>
	/// the action of the player on choosing this node :
	/// 0 : right
	/// 1 : down
	/// 2 : left
	/// 3 : up
	/// </summary>
	public int type;

	/// <summary>
	/// The line and column of the node according to room positions
	/// </summary>
	public int line;
	public int column;

	public Node father;

	public Node(int id, int line, int column){
		this.id = id;
		this.line = line;
		this.column = column;
		this.father = null;
		fScore = -1;
		arcs = new List<Arc> ();
	}


	public string print(){
		return "  id : " + id + " line : " + line + " column : " + column + " score : " + fScore;
	}

	/// <summary>
	/// Adds different type of children nodes
	/// </summary>

	public void addArc(Node n, int w){
		arcs.Add(new Arc(this,n,w));
	}



}
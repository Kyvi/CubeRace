﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*********************************************************/
/* Programme entièrement implémenté par Vincent Leclerc */
/********************************************************/

public class Arc {

	public Node start;
	public Node finish;

	public int weight;

	public Arc(Node s, Node f, int w){
		start = s;
		finish = f;
		weight = w;
	}


}

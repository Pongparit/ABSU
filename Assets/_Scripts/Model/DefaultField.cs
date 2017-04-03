using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultField : Field {


	public int cost ;

	public Player owner ; 

	public FieldType type = (int) FieldType.defaultField;

	
	public int Cost{
		get{
			return this.cost;
		}
		set{
			this.cost = value; 
		}
	}

	public Player Owner {

		get{
			return this.owner; 
		}

		set{
			this.owner = owner; 
		}

	}

}

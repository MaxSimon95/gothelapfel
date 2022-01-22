using UnityEngine;
using System.Collections;

public class NodeConnection
{
	public Node Parent;
	public Node Node;
	public bool Valid;

	//Debug
	private LineRenderer Debugger;
	GameObject go;
	
	public NodeConnection(Node parent, Node node, bool valid)
	{
		//Debug.Log("new node connection: " + parent + ", " + node + ", " + valid);
		Valid = valid;
		Node = node;	
		Parent = parent;
		
		if (Node != null && Node.BadNode)
			Valid = false;
		if (Parent != null && Parent.BadNode)
			Valid = false;
	}

	//Debug
	public void DrawLine()
	{	
		if (Valid) {
			if (Parent != null && Node != null) {
				go = GameObject.Instantiate (Resources.Load ("Line")) as GameObject;
				Debugger = go.GetComponent<LineRenderer> ();
				
				Debugger.SetPosition (0, Parent.Position);
				Debugger.SetPosition (1, Node.Position);
				Debugger.SetWidth (0.06f, 0.00f);
				if (Valid)
					Debugger.SetColors (Color.green, Color.green);
				else
					Debugger.SetColors (Color.red, Color.red);
			}
		}
	}
}
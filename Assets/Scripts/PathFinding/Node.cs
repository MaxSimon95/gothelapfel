using UnityEngine;
using System.Collections;

public class Node {

	public bool BadNode;    

	//Grid coordinates
	public int X;
	public int Y;

	//world position
	public Vector2 Position;

	//our 8 connection points
	public NodeConnection Top;
	public NodeConnection Left;
	public NodeConnection Bottom;
	public NodeConnection Right;
	public NodeConnection TopLeft;
	public NodeConnection TopRight;
	public NodeConnection BottomLeft;
	public NodeConnection BottomRight;
	
	GameObject Debugger;
	
	public Node(float x, float y, Vector2 position, Grid grid)
	{
		Initialize(x, y, position, grid);
	}
	
	public void Initialize(float x, float y, Vector2 position, Grid grid)
	{
		X = (int)x;
		Y = (int)y;
		
		Position = position;

	

		//Draw Node on screen for debugging purposes
		Debugger = GameObject.Instantiate (Resources.Load ("Node")) as GameObject;
		Debugger.GetComponent<SpriteRenderer>().enabled = false;
		Debugger.transform.position = Position;
		Debugger.GetComponent<Debugger> ().X = X;
		Debugger.GetComponent<Debugger> ().Y = Y;
	}

	public void SetColor(Color color)
	{
		Debugger.transform.GetComponent<SpriteRenderer> ().color = color;
	}

	//Cull nodes if they don't have enough valid connection points (3)
	public void CheckConnectionsPass1(Grid grid)
	{
		if (!BadNode) {

			int clearCount = 0;

			if (Top != null && Top.Valid)
				clearCount++;
			if (Bottom != null && Bottom.Valid)
				clearCount++;
			if (Left != null && Left.Valid)
				clearCount++;
			if (Right != null && Right.Valid)
				clearCount++;
			if (TopLeft != null && TopLeft.Valid)
				clearCount++;
			if (TopRight != null && TopRight.Valid)
				clearCount++;
			if (BottomLeft != null && BottomLeft.Valid)
				clearCount++;
			if (BottomRight != null && BottomRight.Valid)
				clearCount++;

			//If not at least 3 valid connection points - disable node
			if (clearCount < 3) {
				BadNode = true;
				DisableConnections ();
			}
		}		
		
		if (!BadNode)
			SetColor (Color.yellow);
		else
			SetColor (Color.red);
	}

	//Remove connections that connect to bad nodes
	public void CheckConnectionsPass2()
	{
		if (Top != null && Top.Node != null && Top.Node.BadNode)
			Top.Valid = false;
		if (Bottom != null && Bottom.Node != null && Bottom.Node.BadNode)
			Bottom.Valid = false;
		if (Left != null && Left.Node != null && Left.Node.BadNode)
			Left.Valid = false;
		if (Right != null && Right.Node != null && Right.Node.BadNode)
			Right.Valid = false;
		if (TopLeft != null && TopLeft.Node != null && TopLeft.Node.BadNode)
			TopLeft.Valid = false;
		if (TopRight != null && TopRight.Node != null && TopRight.Node.BadNode)
			TopRight.Valid = false;
		if (BottomLeft != null && BottomLeft.Node != null && BottomLeft.Node.BadNode)
			BottomLeft.Valid = false;
		if (BottomRight != null && BottomRight.Node != null && BottomRight.Node.BadNode)
			BottomRight.Valid = false;
	}

	//Disable all connections going from this this
	public void DisableConnections()
	{
		if (Top != null) {
			Top.Valid = false;
		}
		if (Bottom != null) {
			Bottom.Valid = false;
		}
		if (Left != null) {
			Left.Valid = false;
		}
		if (Right != null) {
			Right.Valid = false;
		}
		if (BottomLeft != null) {
			BottomLeft.Valid = false;
		}
		if (BottomRight != null) {
			BottomRight.Valid = false;
		}
		if (TopRight != null) {
			TopRight.Valid = false;
		}
		if (TopLeft != null) {
			TopLeft.Valid = false;
		}
	}

	//debug draw for connection lines
	public void DrawConnections()
	{

		if (Top != null) Top.DrawLine ();
		if (Bottom != null)Bottom.DrawLine ();
		if (Left != null)Left.DrawLine ();
		if (Right != null)Right.DrawLine ();
		if (BottomLeft != null)BottomLeft.DrawLine ();
		if (BottomRight != null)BottomRight.DrawLine ();
		if (TopRight != null)TopRight.DrawLine ();
		if (TopLeft != null)TopLeft.DrawLine ();
	}


	//Raycast in all 8 directions to determine valid routes
	public void InitializeConnections(Grid grid)
	{
		//Debug.Log("InitializeConnections");
		bool tempValid = true;
		RaycastHit2D hit;
		float diagonalDistance = Mathf.Sqrt (Mathf.Pow (grid.UnitSizeX, 2) + Mathf.Pow (grid.UnitSizeY, 2));

		
	//Left
		tempValid = true;
		if (X <= 0)
			tempValid = false;
		hit = Physics2D.Raycast(Position, new Vector2(-1, 0), grid.UnitSizeX, LayerMask.GetMask("ColliderLayer"));
		if (hit.collider != null && hit.collider.tag != "PlayerCharacter" && !hit.collider.isTrigger)
		{
			
			tempValid = false;
		}
		Left = new NodeConnection(this, grid.Nodes[X - 1, Y], tempValid);
		Debugger.GetComponent<Debugger>().Left = Left;
		Debugger.GetComponent<Debugger>().LeftValid = tempValid;

		//Topleft
		tempValid = true;
		if (X <= 0)
			tempValid = false;

		if (Y > (int)(grid.Height / grid.UnitSizeY) + 1)
			tempValid = false;

		hit = Physics2D.Raycast(Position, new Vector2(-grid.UnitSizeX, grid.UnitSizeY), diagonalDistance, LayerMask.GetMask("ColliderLayer"));
		if (hit.collider != null && hit.collider.tag != "PlayerCharacter" && !hit.collider.isTrigger)
		{
			
			tempValid = false;
		}

		TopLeft = new NodeConnection(this, grid.Nodes[X - 1, Y + 1], tempValid);
		Debugger.GetComponent<Debugger>().TopLeft = TopLeft;
		Debugger.GetComponent<Debugger>().TopLeftValid = tempValid;

		//Bottomleft
		tempValid = true;
		if (X <= 0)
			tempValid = false;
		if (Y <= 0)
			tempValid = false;

		hit = Physics2D.Raycast(Position, new Vector2(-grid.UnitSizeX, -grid.UnitSizeY), diagonalDistance, LayerMask.GetMask("ColliderLayer"));
		if (hit.collider != null && hit.collider.tag != "PlayerCharacter" && !hit.collider.isTrigger)
		{
			
			tempValid = false;
		}

		BottomLeft = new NodeConnection(this, grid.Nodes[X - 1, Y - 1], tempValid);
		Debugger.GetComponent<Debugger>().BottomLeft = BottomLeft;
		Debugger.GetComponent<Debugger>().BottomLeftValid = tempValid;

		//Bottom
		tempValid = true;
		if (Y <= 0)
			tempValid = false;

		hit = Physics2D.Raycast(Position, new Vector2(0, -1), grid.UnitSizeY, LayerMask.GetMask("ColliderLayer"));
		if (hit.collider != null && hit.collider.tag != "PlayerCharacter" && !hit.collider.isTrigger)
		{
			
			tempValid = false;
		}

		Bottom = new NodeConnection(this, grid.Nodes[X, Y - 1], tempValid);
		Debugger.GetComponent<Debugger>().Bottom = Bottom;
		Debugger.GetComponent<Debugger>().BottomValid = tempValid;

		//BottomRight
		tempValid = true;
		if (X > (int)(grid.Width / grid.UnitSizeX) + 1)
			tempValid = false;
		if (Y <= 0)
			tempValid = false;

		hit = Physics2D.Raycast(Position, new Vector2(grid.UnitSizeX, -grid.UnitSizeY), diagonalDistance, LayerMask.GetMask("ColliderLayer"));
		if (hit.collider != null && hit.collider.tag != "PlayerCharacter" && !hit.collider.isTrigger)
		{
		
			tempValid = false;
		}

		BottomRight = new NodeConnection(this, grid.Nodes[X + 1, Y - 1], tempValid);
		Debugger.GetComponent<Debugger>().BottomRight = BottomRight;
		Debugger.GetComponent<Debugger>().BottomRightValid = tempValid;

		//Right
		tempValid = true;
		if (X > (int)(grid.Width / grid.UnitSizeX) + 1)
			tempValid = false;

		hit = Physics2D.Raycast(Position, new Vector2(1, 0), grid.UnitSizeX, LayerMask.GetMask("ColliderLayer"));
		if (hit.collider != null && hit.collider.tag != "PlayerCharacter" && !hit.collider.isTrigger)
		{
			
			tempValid = false;
		}

		Right = new NodeConnection(this, grid.Nodes[X + 1, Y], tempValid);
		Debugger.GetComponent<Debugger>().Right = Right;
		Debugger.GetComponent<Debugger>().RightValid = tempValid;

		//TopRight
		tempValid = true;
		if (X > (int)(grid.Width / grid.UnitSizeX) + 1)
			tempValid = false;
		if (Y > (int)(grid.Height / grid.UnitSizeY) + 1)
			tempValid = false;

		hit = Physics2D.Raycast(Position, new Vector2(grid.UnitSizeX, grid.UnitSizeY), diagonalDistance, LayerMask.GetMask("ColliderLayer"));
		if (hit.collider != null && hit.collider.tag != "PlayerCharacter" && !hit.collider.isTrigger)
		{
			
			tempValid = false;
		}

		TopRight = new NodeConnection(this, grid.Nodes[X + 1, Y + 1], tempValid);
		Debugger.GetComponent<Debugger>().TopRight = TopRight;
		Debugger.GetComponent<Debugger>().TopRightValid = tempValid;

		//Top
		tempValid = true;
		if (Y > (int)(grid.Height / grid.UnitSizeY) + 1)
			tempValid = false;

		hit = Physics2D.Raycast(Position, new Vector2(0, 1), grid.UnitSizeY, LayerMask.GetMask("ColliderLayer"));
		if (hit.collider != null && hit.collider.tag != "PlayerCharacter" && !hit.collider.isTrigger)
		{
			
			tempValid = false;
		}

		Top = new NodeConnection(this, grid.Nodes[X, Y + 1], tempValid);
		Debugger.GetComponent<Debugger>().Top = Top;
		Debugger.GetComponent<Debugger>().TopValid = tempValid;

		//DrawConnections();

	}


}


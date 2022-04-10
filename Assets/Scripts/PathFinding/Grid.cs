using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public enum Direction
{
	Right,
	Left,
	Top,
	Bottom,
	BottomLeft,
	BottomRight,
	TopLeft,
	TopRight,  
}

public class Grid : MonoBehaviour {


	public Camera camera;

	//Pathfinding demo

	public Vector2 Offset;
	public float UnitSize;
	public float UnitSizeX;
	public float UnitSizeY;

	public int Width;
	public int Height;
	
	public Node[,] Nodes;
	
	public int Left { get { return 0; } }
	public int Right { get { return (int)(Width / UnitSizeX+1); } }
	public int Bottom { get { return 0; } }
	public int Top { get { return (int)(Height / UnitSizeY+1); } }

	private BreadCrumb currentTargetBC = null;

	private LineRenderer LineRenderer;
	GameObject Player;

	void Awake () 
	{

		Player = GameObject.Find ("PlayerCharacter");
		LineRenderer = transform.GetComponent<LineRenderer>();

		//Get grid dimensions
		Offset = this.transform.position;

		//Width = ((int)this.transform.localScale.x) * 30 + 2;
		//Height = ((int)this.transform.localScale.y) * 30 + 2;

	UnitSizeX = Player.GetComponent<BoxCollider2D>().size.x * Player.transform.localScale.x * 1.3f;
	UnitSizeY = Player.GetComponent<BoxCollider2D>().size.y * Player.transform.localScale.y * 1.3f; 


	Nodes = new Node[(int)(Width/ UnitSizeX)+1, (int)(Height / UnitSizeY)+1];

		//Debug.Log("Initialize the grid nodes - 1 grid unit between each node");
		//We render the grid in a diamond pattern

		for(int x = 0; x < Width/UnitSizeX; x++)
        {
			for(int y = 0; y < Height/UnitSizeY; y++)
            {
				float ptx = x*UnitSizeX;
				float pty = y*UnitSizeY;

				Vector2 pos = new Vector2(ptx + Offset.x, pty + Offset.y);
				Node node = new Node(x, y, pos, this);
				Nodes[x, y] = node;
			}
        }

	



		//Debug.Log(Nodes.Length + " Nodes created");

		//Debug.Log("Create connections between each node");
		for (int x = 1; x < Width / UnitSizeX -1; x++)
		{
			for (int y = 1; y < Height / UnitSizeY -1; y++)
			{
				
				//if (Nodes[x,y] == null) continue;				
				Nodes[x, y].InitializeConnections(this);
			}
		}

		//Debug.Log("Pass 1, we removed the bad nodes, based on valid connections");
		for (int x = 0; x < Width / UnitSizeX; x++)
		{
			for (int y = 0; y < Height / UnitSizeY; y++)
			{
				if (Nodes[x,y] == null) 
					continue;				

				Nodes[x, y].CheckConnectionsPass1 (this);
			}
		}

		//Debug.Log(Nodes.Length + " Nodes remaining");

		//Debug.Log("Pass 2, remove bad connections based on bad nodes");
		for (int x = 0; x < Width / UnitSizeX; x++)
		{
			for (int y = 0; y < Height / UnitSizeY; y++)
			{
				if (Nodes[x,y] == null) 
					continue;				

				Nodes[x, y].CheckConnectionsPass2 ();
				//Nodes[x, y].DrawConnections ();	//debug
			}
		}		
	}		
	

	public Point WorldToGrid(Vector2 worldPosition)
	{

		int x = Mathf.RoundToInt((worldPosition.x - transform.position.x) / UnitSizeX);
		int y = Mathf.RoundToInt((worldPosition.y - transform.position.y) / UnitSizeY);
		//Debug.Log(x);
		//Debug.Log(y);
		return new Point(Nodes[x, y].X, Nodes[x, y].Y);

		//Vector2 gridPosition = new Vector2((worldPosition.x * 2f), (worldPosition.y * 2f) + 1);


		/*
		//adjust to our nearest integer
		float rx = gridPosition.x % 1;
		if (rx < 0.5f)
			gridPosition.x = gridPosition.x - rx;
		else
			gridPosition.x = gridPosition.x + (1 - rx);
		
		float ry = gridPosition.y % 1;
		if (ry < 0.5f)
			gridPosition.y = gridPosition.y - ry;
		else
			gridPosition.y = gridPosition.y + (1 - ry);
				
		int x = (int)gridPosition.x;
		int y = (int)gridPosition.y;

		if (x < 0 || y < 0 || x > Width || y > Height)
			return null;

		Node node = Nodes [x, y];
		//We calculated a spot between nodes'
		//Find nearest neighbor
		if((node == null) ||  (x % 2 == 0 && y % 2 == 0) || (gridPosition.y % 2 == 1 && gridPosition.x % 2 == 1))
		{   
			float mag = 100;


			if (x < Width && !Nodes[x + 1, y].BadNode)
			{
				float mag1 = (Nodes[x + 1, y].Position - worldPosition).magnitude;
				if (mag1 < mag)
				{
					mag = mag1;
					node = Nodes[x + 1, y];
				}
			}
			if (y < Height - 1 && !Nodes[x, y + 1].BadNode)
			{
				float mag1 = (Nodes[x, y+ 1].Position - worldPosition).magnitude;
				if (mag1 < mag)
				{
					mag = mag1;
					node = Nodes[x, y + 1];
				}
			}
			if (x > 0 && !Nodes[x- 1, y].BadNode)
			{
				float mag1 = (Nodes[x - 1, y].Position - worldPosition).magnitude;
				if (mag1 < mag)
				{
					mag = mag1;
					node = Nodes[x-1, y];
				}
			}
			if (y > 0 && !Nodes[x, y - 1].BadNode)
			{
				float mag1 = (Nodes[x, y - 1].Position - worldPosition).magnitude;
				if (mag1 < mag)
				{
					mag = mag1;
					node = Nodes[x, y -1+ 1];
				}
			}
		}


		return new Point(node.X , node.Y);

		*/
	}

	public Vector2 GridToWorld(Point gridPosition)
	{
		//Vector2 world = new Vector2(gridPosition.X / 2f, -(gridPosition.Y / 2f - 0.5f));
		float worldX = (gridPosition.X ) * UnitSizeX + transform.position.x;
		float worldY = (gridPosition.Y ) * UnitSizeY + transform.position.y;


		Vector2 world = new Vector2(worldX, worldY);

		return world;
	}
	
	public bool ConnectionIsValid(Point point1, Point point2)
	{
		//comparing same point, return false
		if (point1.X == point2.X && point1.Y == point2.Y)
			return false;
		
		if (Nodes [point1.X, point1.Y] == null)
			return false;
		
		//determine direction from point1 to point2
		Direction direction = Direction.Bottom;

		if (point1.X == point2.X)
		{
			if (point1.Y > point2.Y)
				direction = Direction.Bottom;
			else if (point1.Y < point2.Y)
				direction = Direction.Top;
		}
		else if (point1.Y == point2.Y)
		{
			if (point1.X < point2.X)
				direction = Direction.Right;
			else if (point1.X > point2.X)
				direction = Direction.Left;
		}
		else if (point1.X < point2.X)
		{
			if (point1.Y < point2.Y)
				direction = Direction.TopRight;
			else if (point1.Y > point2.Y)
				direction = Direction.BottomRight;
		}
		else if (point1.X > point2.X)
		{
			if (point1.Y < point2.Y)
				direction = Direction.TopLeft;
			else if (point1.Y > point2.Y)
				direction = Direction.BottomLeft;
		}

		if (((point1.X == 11) && (point1.Y == 21)) && ((point2.X == 11) && (point2.Y == 22)))
		{
			//Debug.Log("Direction: " + direction);
		}

		//check connection
		switch (direction)
		{
			case Direction.Bottom:
			if (Nodes[point1.X, point1.Y].Bottom != null)
				return Nodes[point1.X, point1.Y].Bottom.Valid;
			else
				return false;

			case Direction.Top:

				if((point1.X == 11) && (point1.Y == 21))
				{
					//Debug.Log("UNSER PUNKT: TOP = " + Nodes[point1.X, point1.Y].Top.Valid);
						}

			if (Nodes[point1.X, point1.Y].Top != null)
				return Nodes[point1.X, point1.Y].Top.Valid;
			else
				return false;
		
			case Direction.Right:
			if (Nodes[point1.X, point1.Y].Right != null)
				return Nodes[point1.X, point1.Y].Right.Valid;
			else
				return false;

			case Direction.Left:
			if (Nodes[point1.X, point1.Y].Left != null)
				return Nodes[point1.X, point1.Y].Left.Valid;
			else
				return false;
		
			case Direction.BottomLeft:
			if (Nodes[point1.X, point1.Y].BottomLeft != null)
				return Nodes[point1.X, point1.Y].BottomLeft.Valid;
			else
				return false;

			case Direction.BottomRight:
			if (Nodes[point1.X, point1.Y].BottomRight != null)
				return Nodes[point1.X, point1.Y].BottomRight.Valid;
			else
				return false;
		
			case Direction.TopLeft:
			if (Nodes[point1.X, point1.Y].TopLeft != null)
				return Nodes[point1.X, point1.Y].TopLeft.Valid;
			else
				return false;
		
			case Direction.TopRight:
			if (Nodes[point1.X, point1.Y].TopRight != null)
				return Nodes[point1.X, point1.Y].TopRight.Valid;
			else
				return false;
		
			default:
				return false;
		}		
	}

	void PrintCorrectPathNodes(int index,  BreadCrumb currentBC)
    {
		//Debug.Log(index + ": " + currentBC.position.X +", " + currentBC.position.Y + "    next: " + currentBC.next + "  prev: " + currentBC.prev);

		//Debug.Log("POAINT");

		if (currentBC.next != null)
        {

			//Debug.Log("sdasdasd");
			if (index > 1) Nodes[currentBC.position.X, currentBC.position.Y].SetColor(Color.black);
			//if (index == 2) Nodes[currentBC.position.X, currentBC.position.Y].SetColor(Color.magenta);
			PrintCorrectPathNodes(index + 1, currentBC.next);

		}
    }

	public bool TryGoToWorldpos(Vector2 worldPos)
    {


		//Debug.Log(worldPos);

		Point gridPos = WorldToGrid(worldPos);
		//Debug.Log(gridPos);

		if (gridPos != null)
		{

			if (gridPos.X > 0 && gridPos.Y > 0 && gridPos.X < (Width / UnitSizeX) && gridPos.Y < (Height / UnitSizeY))
			{

				//Convert player point to grid coordinates
				Point playerPos = WorldToGrid(Player.transform.position);
				Nodes[playerPos.X, playerPos.Y].SetColor(Color.blue);

				Nodes[gridPos.X, gridPos.Y].SetColor(Color.white);


				//Find path from player to clicked position
				BreadCrumb bc = PathFinder.FindPath(this, playerPos, gridPos);

				if (bc != null)
				{
					PrintCorrectPathNodes(1, bc);
					currentTargetBC = bc;
					Player.GetComponent<PlayerCharacter>().StartMoveToPoint(GridToWorld(currentTargetBC.position));

					return true;
				}
				else
				{
					return false;
				}


			}
			else return false;
		}
		else
			return false;
	}

	void Update()
	{
		
		if(currentTargetBC != null)
        {
			Point gridPCPoint = WorldToGrid(new Vector2(Player.transform.position.x, Player.transform.position.y));

			if ((currentTargetBC.position.X == gridPCPoint.X) && (currentTargetBC.position.Y == gridPCPoint.Y))
			{
				//Debug.Log("Breadcrumbs Punkt erreicht");
				
				if(currentTargetBC.next == null)
                {
					//Debug.Log("Pathfinding Ziel erreicht");
				}
				else
                {
					currentTargetBC = currentTargetBC.next;
					Player.GetComponent<PlayerCharacter>().StartMoveToPoint(GridToWorld(currentTargetBC.position));

				}

			}
		}

		if (!MouseInputUIBlocker.BlockedByUI)
		{
			
			if (Input.GetMouseButtonDown(0))
			{
				if (!RenderOrderAdjustment.anyOverlayOpen)
				{
					
					bool blockedByCollider = false;
					List<GameObject> movementTargetBlockers = GameObject.FindGameObjectsWithTag("PlayerCharacter")[0].GetComponent<PlayerCharacter>().currentRoom.movementBlockers;

					Vector3 worldPosClick = camera.ScreenToWorldPoint(Input.mousePosition);

					//Debug.Log(movementTargetBlockers);

					foreach (GameObject tempGO in movementTargetBlockers) {
						//Debug.Log("Loop");
						
						if(tempGO.GetComponent<Collider2D>().OverlapPoint(new Vector2(worldPosClick.x, worldPosClick.y)))
                        {
							blockedByCollider = true;
							//Debug.Log(blockedByCollider);

						}
						else
                        {
							//Debug.Log("kein overlap");
							//Debug.Log("mouse " + new Vector2(worldPosClick.x, worldPosClick.y));
							//Debug.Log("Collider " + new Vector2(tempGO.transform.position.x, tempGO.transform.position.y));  
						}
                    }

					if(!blockedByCollider)
                    {
						//Debug.Log("Mouse Button Down");
						//Convert mouse click point to grid coordinates
						Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

						TryGoToWorldpos(worldPos);
					}
					
				}
			}
		}

		
		
	}

	public Node FindNearestAccessibleNodeFromWorldCoordinates(Vector2 world_coordinates)
    {
		Node temp_return_node = null;
		Point grid_point = WorldToGrid(world_coordinates);
		
		float compare_distance = 9999999;

		for(int i=0; i<Nodes.GetLength(0); i++)
        {
			for (int j = 0; j < Nodes.GetLength(1); j++)
			{
				float temp_distance = Mathf.Sqrt(Mathf.Pow((grid_point.X - Nodes[i, j].X), 2) + Mathf.Pow((grid_point.Y - Nodes[i, j].Y), 2));
				if (!Nodes[i,j].BadNode && temp_distance < compare_distance)
                {
					//Debug.Log("temp_distance: " + temp_distance + ", compare_distance: " + compare_distance);
					if (PathFinder.FindPath(this, WorldToGrid(Player.transform.position), new Point(Nodes[i, j].X, Nodes[i, j].Y)) !=null)
                    {
						compare_distance = temp_distance;
						temp_return_node = Nodes[i, j];

					}
                }
			}
		}

		//Debug.Log("temp_return_node = " + temp_return_node.X + " " + temp_return_node.Y);
		return (temp_return_node);
    }

	
}




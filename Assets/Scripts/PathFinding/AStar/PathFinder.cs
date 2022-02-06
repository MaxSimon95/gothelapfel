using UnityEngine;

public static class PathFinder
{     
	public static BreadCrumb FindPath(Grid world, Point start, Point end)
	{        
	    BreadCrumb bc = FindPathReversed(world, start, end);
        BreadCrumb[] temp = new BreadCrumb[256];

        if (bc != null)
        {
            int index = 0;
            while (bc != null)
            {
                temp[index] = bc;
                bc = bc.next;
                index++;
            }

            index -= 2;

            BreadCrumb current = new BreadCrumb(start);
            BreadCrumb head = current;

            while (index >= 0)
            {                
                current.next = new BreadCrumb(temp[index].position);
                current = current.next;
                index--;              
            }
            return head;
        }
        else
        {
            return null;
        }
	}
	
	private static BreadCrumb FindPathReversed(Grid world, Point start, Point end)
	{
	    MinHeap<BreadCrumb> openList = new MinHeap<BreadCrumb>(256);

        //Debug.Log("world right and top: " + world.Right + " " + world.Top);

	    BreadCrumb[,] brWorld = new BreadCrumb[world.Right, world.Top];
	    BreadCrumb tempBreadCrumb;
	    Point tempPoint;
	    int cost;
	    float diff;
	
	    BreadCrumb current = new BreadCrumb(start);
	    current.cost = 0;
	
	    BreadCrumb finish = new BreadCrumb(end);

        //Debug.Log(current.position.X + " " + current.position.Y);
       // Debug.Log(brWorld);
	    brWorld[current.position.X, current.position.Y] = current;
	    openList.Add(current);
	
	    while (openList.Count > 0)
	    {
	        //Find best item and switch it to the 'closedList'
	        current = openList.ExtractFirst();
	        current.onClosedList = true;
	
	        //Find neighbours
	        for (int i = 0; i < surrounding.Length; i++)
	        {
                tempPoint = new Point(current.position.X + surrounding[i].X, current.position.Y + surrounding[i].Y);

                if(current.position.X == 11 && current.position.Y == 21 && tempPoint.X == 11 && tempPoint.Y == 22)
                {
                    //Debug.Log("UNSER PATHFINDER FIND NEIGHBOURS CHECK --> VALID? " + world.ConnectionIsValid(current.position, tempPoint));
                    
                }

                if (!world.ConnectionIsValid(current.position, tempPoint)) 
                    continue;

                if(world.Nodes[tempPoint.X, tempPoint.Y].BadNode )
                    continue;

                //Debug.Log("valid temppoint in find neighbours" + tempPoint.X + " " + tempPoint.Y);

                //Check if we've already examined a neighbour, if not create a new node for it.
                if (brWorld[tempPoint.X, tempPoint.Y] == null)
                {
                    tempBreadCrumb = new BreadCrumb(tempPoint);
                    brWorld[tempPoint.X, tempPoint.Y] = tempBreadCrumb;
                }
                else
                {
                    tempBreadCrumb = brWorld[tempPoint.X, tempPoint.Y];
                }

                //If the node is not on the 'closedList' check it's new score, keep the best
                if (!tempBreadCrumb.onClosedList)
                {
                    diff = 0;
                    if (current.position.X != tempBreadCrumb.position.X)
                    {
                        diff += world.UnitSizeX * 100;
                    }
                    if (current.position.Y != tempBreadCrumb.position.Y)
                    {
                        diff += world.UnitSizeY * 100;
                    }

					int distance = (int)Mathf.Pow(Mathf.Max(Mathf.Abs (end.X - tempBreadCrumb.position.X), Mathf.Abs(end.Y - tempBreadCrumb.position.Y)), 2);
                    cost = current.cost + (int) diff + distance * 100;

                    if (cost < tempBreadCrumb.cost)
                    {
                        tempBreadCrumb.cost = cost;
                        tempBreadCrumb.next = current;
                    }

                    //If the node wasn't on the openList yet, add it 
                    if (!tempBreadCrumb.onOpenList)
                    {
                        //Check to see if we're done
                        if (tempBreadCrumb.Equals(finish))
                        {
                            //Debug.Log("Finished, Path Found");
                            //Debug.Log(tempBreadCrumb);
                            tempBreadCrumb.next = current;
                            return tempBreadCrumb;
                        }
                        tempBreadCrumb.onOpenList = true;
                        openList.Add(tempBreadCrumb);
                    }
	            }
	        }
	    }

        //Debug.Log("Failure, No Path available");

        return null; //no path found
	}
	
	//Neighbour options
	//Our diamond pattern offsets top/bottom/left/right by 2 instead of 1
	private static Point[] surrounding = new Point[]{                         
	//	new Point(0, 2), new Point(-2, 0), new Point(2, 0), new Point(0,-2),	
    //    new Point(-1, 1), new Point(-1, -1), new Point(1, 1), new Point(1, -1)
    new Point(0,1), new Point(1,1), new Point(1,0), new Point(1,-1), new Point(0,-1), new Point(-1,-1), new Point(-1,0), new Point(-1,1)

    };
}


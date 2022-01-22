using UnityEngine;
using System.Collections;

public class Debugger : MonoBehaviour {

	public int X;
	public int Y;

	public NodeConnection Top;
	public NodeConnection Left;
	public NodeConnection Bottom;
	public NodeConnection Right;
	public NodeConnection TopLeft;
	public NodeConnection TopRight;
	public NodeConnection BottomLeft;
	public NodeConnection BottomRight;

	public bool TopValid;
	public bool LeftValid;
	public bool BottomValid;
	public bool RightValid;
	public bool TopLeftValid;
	public bool TopRightValid;
	public bool BottomLeftValid;
	public bool BottomRightValid;

}

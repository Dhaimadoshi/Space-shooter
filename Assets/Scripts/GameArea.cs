using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter 
{
	/// <summary>
	/// Defines a Rectangular game area.
	/// </summary>
	[AddComponentMenu ("Space Shooter/Game Area")]
	public class GameArea : MonoBehaviour 
	{
		[HideInInspector]
		[SerializeField]
		private Rect _area = new Rect(0, 0, 10, 10);
		public Rect Area
		{
			get { return _area;}
			set { _area = value;}
		}
			
		[SerializeField] private Vector2 size;
		public Vector2 Size
		{
			get { return Area.size; }
			set 
			{
				Area = new Rect(value.x * -0.5f, value.y * -0.5f, value.x, value.y);
			}
		}

		public Color gizmoColor = new Color(0,0,1, 0.2f);
		Color gizmoWireColor;

//		public void SetArea(Vector2 size)
//		{
//			Area = new Rect(size.x * -0.5f, size.y * -0.5f, size.x, size.y);
//		}

		void OnValidate()
		{
//			SetArea(size);
			Size = size;
			gizmoWireColor = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 1);
		}

		void OnDrawGizmos()
		{
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.color = gizmoColor;
			Gizmos.DrawCube (new Vector3 (Area.center.x, Area.center.y, 0), new Vector3 (Area.width, Area.height, 0));
			Gizmos.color = gizmoWireColor;
			Gizmos.DrawWireCube (new Vector3 (Area.center.x, Area.center.y, 0), new Vector3 (Area.width, Area.height, 0));
		}

		public Vector3 GetRandomPosition ()
		{
			Vector3 vector3 = Vector3.zero;
			vector3.x = Random.Range(Area.xMin, Area.xMax);
			vector3.y = Random.Range(Area.yMin, Area.yMax);
			vector3 = transform.TransformPoint(vector3);
			return vector3;
		}
	}
}

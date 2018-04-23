using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter 
{	
	/// <summary>
	/// Fit area to camera.
	/// </summary>
	[AddComponentMenu ("Space Shooter/Fit Area To Camera")]
	[RequireComponent (typeof(GameArea))]
	public class FitAreaToCamera : MonoBehaviour {
		
		private GameArea Area
		{
			get { return GetComponent<GameArea>();}
		}

		private void Awake() 
		{
			FitToMainCamera();
		}

		void FitToCamera (Camera cam)
		{
//			Area.SetArea (new Vector2 (cam.aspect * cam.orthographicSize * 2, cam.orthographicSize * 2));
			Area.Size = new Vector2 (cam.aspect * cam.orthographicSize * 2, cam.orthographicSize * 2);	
			transform.position = (Vector2) cam.transform.position;
			transform.rotation = cam.transform.rotation;
		}

		void FitToMainCamera ()
		{
			FitToCamera(Camera.main);
		}

		void Reset ()
		{
			FitToMainCamera();
		}
	}
}
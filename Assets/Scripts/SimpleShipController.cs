using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter 
{
	[AddComponentMenu ("Space Shooter/Simple Ship Controller")]
	public class SimpleShipController : MonoBehaviour 
	{	
		void Update ()
		{
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");
			transform.Translate(0, y, 0);
			transform.Rotate(0, 0, -x);
		}
	}
}
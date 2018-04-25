//
//  ControlsBase.cs
//
//  Author:
//		 Nicolas Gerard (Daimadoushi) <dhaimadoshi@gmail.com>
//
//  Copyright (c) 2018 Nicolas Gerard.
//
 
using UnityEngine;
 
namespace Shooter
{
    public abstract class ControlsBase : MonoBehaviour
    {
		#region Variables
		protected IControllable controllable;
		public ShipControllerBase shipController;

		protected void Awake()
		{
			controllable = shipController;
		}
		#endregion
    }
}
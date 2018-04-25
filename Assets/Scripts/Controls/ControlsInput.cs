//
//  ControlsInput.cs
//
//  Author:
//		 Nicolas Gerard (Daimadoushi) <dhaimadoshi@gmail.com>
//
//  Copyright (c) 2018 Nicolas Gerard.
//
 
using UnityEngine;
 
namespace Shooter
{
    public class ControlsInput : ControlsBase
    {
        #region Public Fields
        #endregion

		private Vector2 _movement;
     
        #region Unity Methods
        void Update()
        {
			_movement.x = Input.GetAxis("Horizontal");
			_movement.y = Input.GetAxis("Vertical");
			controllable.Move(_movement);

			controllable.Attacking = Input.GetButton("Fire2");
			controllable.Protecting = Input.GetButton("Fire3");
        }
        #endregion
     
        #region Private Methods
        #endregion
    }
}
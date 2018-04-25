//
//  IControllable.cs
//
//  Author:
//		 Nicolas Gerard (Daimadoushi) <dhaimadoshi@gmail.com>
//
//  Copyright (c) 2018 Nicolas Gerard.
//
 
using UnityEngine;
 
namespace Shooter
{
	public interface IControllable
    {
		bool Attacking { get; set; }
		bool Protecting { get; set; }
		void Move (Vector2 movement);
    }
}
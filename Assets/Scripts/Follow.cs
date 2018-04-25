//
//  Follow.cs
//
//  Author:
//		 Nicolas Gerard (Daimadoushi) <dhaimadoshi@gmail.com>
//
//  Copyright (c) 2018 Nicolas Gerard.
//
 
using UnityEngine;
 
namespace Shooter
{
    public class Follow : MonoBehaviour
    {
        #region Public Fields
		public Transform target;
        #endregion
     
        #region Unity Methods
        void Update()
        {
			transform.position = target.position;
        }
        #endregion
     
        #region Private Methods
        #endregion
    }
}
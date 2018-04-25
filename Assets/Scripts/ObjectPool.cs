//
//  ObjectPool.cs
//
//  Author:
//		 Nicolas Gerard (Daimadoushi) <dhaimadoshi@gmail.com>
//
//  Copyright (c) 2018 Nicolas Gerard.
//
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
 
namespace Shooter
{
    public class ObjectPool : MonoBehaviour
    {
        #region Private Fields
		private List<GameObject> _objectList;
		private GameObject _objectToRecyle;
		private int _totalObjectAtStart;
        #endregion

		static private Dictionary<int, ObjectPool> pools = new Dictionary<int, ObjectPool>();
     
        #region Unity Methods
        void Init()
        {
			_objectList = new List<GameObject>(_totalObjectAtStart);

			for (int i = 0; i < _totalObjectAtStart; i++)
			{
				GameObject newObject = Instantiate(_objectToRecyle);
				newObject.transform.parent = transform;
				newObject.SetActive(false);
				_objectList.Add(newObject);
			}

			pools.Add(_objectToRecyle.GetInstanceID(), this);
        }
        #endregion

		#region Public Methods
		static public bool IsPoolReady (GameObject original)
		{
			return pools.ContainsKey(original.GetInstanceID());
		}

		static public void InitPool (GameObject original, int poolSize = 200)
		{
			if (!pools.ContainsKey(original.GetInstanceID()))
			{
				GameObject go = new GameObject("ObjectPool: " + original.name);
				ObjectPool pool = go.AddComponent<ObjectPool>();
				pool._objectToRecyle = original;
				pool._totalObjectAtStart = poolSize;
				pool.Init();
			}
		}

		static public GameObject GetInstance (int instanceID, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), int poolsize = 200)
		{
			return pools[instanceID].PoolObject(position, rotation);
		}

		static public GameObject GetInstance (GameObject original, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), int poolSize = 200)
		{
			int id = original.GetInstanceID();
			InitPool(original, poolSize);
			return pools[id].PoolObject(position, rotation);
		}

		static public void Release (GameObject obj)
		{
			if (obj.GetComponentInParent<ObjectPool>() == null)
			{
				foreach (ObjectPool p in pools.Values)
				{
					if (p._objectList.Contains(obj))
					{
						obj.transform.parent = p.transform;
						break;
					}
				}
			}

			obj.SetActive(false);
		}
        #endregion

		#region Private Methods
		private GameObject PoolObject (Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion))
		{
			var freeObject = (from item in _objectList
				where item.activeSelf == false
				select item).FirstOrDefault();

			if (freeObject == null)
			{
				freeObject = Instantiate(_objectToRecyle, position, rotation);
				freeObject.transform.parent = transform;
				_objectList.Add(freeObject);
			}
			else
			{
				freeObject.transform.position = position;
				freeObject.transform.rotation = rotation;
				freeObject.SetActive(true);
			}

			return freeObject;
		}
		#endregion
    }
}
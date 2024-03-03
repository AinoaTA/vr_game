using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ainoa.Shoot
{
	public class BulletPooling
	{
		public List<Bullet> ListMove { get => _list; private set => _list = value; }
		private List<Bullet> _list = new();

		private readonly Transform _parent;
		private readonly Bullet _prefab;
		private readonly bool _dynamic;

		private List<Transform> Transforms = new(); 

		/// <summary>
		/// Dynamic means the pool will increase quantity if there is not enough items in pool.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="instances"></param>
		/// <param name="parent"></param>
		/// <param name="dynamic"></param>
		public BulletPooling(Bullet type, int instances, Transform parent, bool dynamic)
		{
			_prefab = type;
			_dynamic = dynamic;
			_parent = parent;

			CreatePool(instances);
		}

		private void CreatePool(int instances)
		{
			for (int i = 0; i < instances; i++)
			{
				Bullet a = Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity, _parent);

				a.gameObject.SetActive(false);

				_list.Add(a);
				Transforms.Add(a.transform);
			}
		}

		public Bullet GetObjectPooling()
		{
			if (_dynamic)
			{
				if (_list.Find((x) => !x.gameObject.activeSelf) == null)
					CreatePool(10);
			}

			Bullet m = _list.Find((x) => !x.gameObject.activeSelf);

			//just in case that no exist one deactivated.
			if (m == null) m = _list[0]; 

			return m;
		}
	}
}
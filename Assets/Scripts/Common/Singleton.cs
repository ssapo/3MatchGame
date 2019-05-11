using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	static private T s_instance;

	static private object s_lock = new object();

	static public T Instance
	{
		get
		{
			lock (s_lock)
			{
				s_instance = FindObjectOfType<T>();

				if (s_instance == null)
				{
					var type = typeof(T);

					var singleton = new GameObject(type.Name);
					s_instance = singleton.AddComponent<T>();

					singleton.name = "singleton_" + type.ToString();
				}
			}

			return s_instance;
		}
	}

	protected virtual void Awake()
	{
		if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	protected virtual void OnDestroy()
	{
		s_instance = null;
	}
}

using UnityEngine;

namespace BoaNeoTools.Utility 
{
	/// <summary>
	/// Use `Singleton<Type>.Instance` to safely get a singleton instance of a given MonoBehaviour derived type
	/// This method will return (in order of preference)
	/// 1) An existing scene object if one exist with the required component
	/// 2) A new instance of a prefab with the desired component if one is found in /Resources
	/// 3) A new game object with the desired component added if nothing else is found
	/// </summary>

	public class Singleton<T> where T : MonoBehaviour 
	{
		private static T _instance;
		private static readonly object _lock = new ();

		public static T Instance 
		{
			get 
			{
				lock (_lock) 
				{
					if (_instance == null) 
					{
						var all = Object.FindObjectsOfType<T>();
						_instance = all != null && all.Length > 0 ? all[0] : null;

						if (all != null && all.Length > 1) 
						{
							Log.Warn($"[Singleton] There are {all.Length} instances of {typeof(T)}. This may happen if your singleton is also a prefab, in which case there is nothing to worry about.");
							return _instance;
						}

						if (_instance == null)
						{
							all = Resources.LoadAll<T>("");
							if (all != null && all.Length > 0)
							{
								Log.Debug($"[Singleton] An instance of {typeof(T)} is needed in the scene, found {all.Length} prefabs of the correct type.");
								_instance = Object.Instantiate(all[0]);
							}
							else
							{
								Log.Debug($"[Singleton] An instance of {typeof(T)} is needed in the scene, no prefab found, creating clean object.");
								GameObject singleton = new GameObject();
								_instance = singleton.AddComponent<T>();
							}

							_instance.name = $"(singleton) {typeof(T)}";
						}

						if (Application.isPlaying)
							Object.DontDestroyOnLoad(_instance.gameObject);

						Log.Debug($"[Singleton] {typeof(T)} '{_instance.gameObject}' was created with DontDestroyOnLoad.");
					}
				}
				return _instance;
			}
		}
	}
}
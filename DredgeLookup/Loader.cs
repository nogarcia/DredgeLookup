using UnityEngine;

namespace DredgeLookup
{
	public class Loader
	{
		/// <summary>
		/// Run by Winch to initialize the mod
		/// </summary>
		public static void Initialize()
		{
			var gameObject = new GameObject(nameof(DredgeLookup));
			gameObject.AddComponent<DredgeLookup>();
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}
}
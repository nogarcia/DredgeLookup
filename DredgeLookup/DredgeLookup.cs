using UnityEngine;
using Winch.Core;

namespace DredgeLookup
{
	public class DredgeLookup : MonoBehaviour
	{
		public void Awake()
		{
			WinchCore.Log.Debug($"{nameof(DredgeLookup)} has loaded!");
		}

		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.L)
				&& GameManager.Instance // Make sure the game is loaded!
				&& GameManager.Instance.GridManager // ...make sure the GridManager is loaded too
				&& !GameManager.Instance.GridManager.IsCurrentlyHoldingObject() // The game won't let you open Encyclopedia while holding an object anyway
				&& GameManager.Instance.GridManager.CurrentlyHoveredObject.ItemData is FishItemData)
			{
				FishItemData fishItemData = GameManager.Instance.GridManager.currentlyHoveredObject.ItemData as FishItemData;
				Encyclopedia encyclopedia = GameObject.Find("/GameCanvases/PopupCanvas/EncyclopediaWindow/Container/Encyclopedia").GetComponent<Encyclopedia>();

				if (encyclopedia.currentFishList == null)
				{
					// currentFishList is first set in Encyclopedia's Awake(), which potentially hasn't been called yet if this is the first
					// time the user's opened the encyclopedia in the session.

					// In Awake(), it's immediately set to Encyclopedia.allFish, so we just do this early the first time.
					encyclopedia.currentFishList = encyclopedia.allFish;
				}

				int fishIndex = encyclopedia.currentFishList.FindIndex(fish => fish == fishItemData);

				if (fishIndex == -1)
				{
					// Either this fish doesn't exist, or (more likely) the encyclopedia filters exclude it.
					return;
				}

				encyclopedia.currentIndex = fishIndex;
				encyclopedia.RefreshUI();
			}
		}
	}
}

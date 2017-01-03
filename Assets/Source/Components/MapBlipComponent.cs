using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlipComponent : MonoBehaviour
{
    public MapUIComponent mapComponent;
    public Sprite blipSprite;

	private void Start()
    {
        mapComponent.AddBlip(transform, blipSprite);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

public class InputController : MonoBehaviour
{
    void FixedUpdate()
    {
        Ray ray;
        
        Touch touch = new Touch();
        bool touched = false;

        if (Input.touchCount > 0)
        {
            touched = true;
            touch = Input.GetTouch(0);
            ray = CameraController.Instance.activeCamera.ScreenPointToRay(touch.position);
        }
        else if (Input.GetMouseButton(0))
        {
            ray = CameraController.Instance.activeCamera.ScreenPointToRay(Input.mousePosition);
        }
        else { return; }

        switch (IslandController.Instance.activeIsland)
        {
            case Island.SkyIsland:
                IslandController.Instance.skyIsland.TakeInput(ray);
                break;
            case Island.TempleOfDoom:
                IslandController.Instance.temple.TakeInput(ray);
                break;
            case Island.ChickenFight:
                if (touched)
                    IslandController.Instance.fight.TakeInput(touch);
                break;
        }
    }
}

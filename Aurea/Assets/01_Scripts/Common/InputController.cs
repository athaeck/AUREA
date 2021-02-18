using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

public enum InputType
{
    Tap,
    UpSwipe,
    DownSwipe,
    LeftSwipe,
    RightSwipe
}

public class InputController : MonoBehaviour
{
    [SerializeField]
    private float tapThreshhold = 50f;

    Vector2 startPosition = Vector2.zero;
    Vector2 endPosition = Vector2.zero;

    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        switch (IslandController.Instance.activeIsland)
        {
            case Island.SkyIsland:
                EvaluateInputSkyisland();
                break;
            case Island.TempleOfDoom:
                EvaluateInputTemple();
                break;
            case Island.ChickenFight:
                EvaluateInputChicken();
                break;
        }
    }

    void EvaluateInputChicken()
    {
        Touch touch = new Touch();

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    endPosition = touch.position;

                    Vector2 res = endPosition - startPosition;

                    InputType type = GetInputType(res);

                    switch (type)
                    {
                        case InputType.Tap:
                            Ray ray = CameraController.Instance.activeCamera.ScreenPointToRay(endPosition);
                            IslandController.Instance.fight.TakeInput(ray);
                            break;
                        default:
                            IslandController.Instance.fight.camController.TakeInput(type);
                            break;
                    }
                    break;
            }
        }
    }

    InputType GetInputType(Vector2 _input)
    {
        InputType type = InputType.UpSwipe;

        if (_input.magnitude < tapThreshhold)
        {
            type = InputType.Tap;
            return type;
        }

        Vector2 disTop = _input - Vector2.up;
        Vector2 disDown = _input - Vector2.down;
        Vector2 disRight = _input - Vector2.right;
        Vector2 disLeft = _input - Vector2.left;

        for (int i = 0; i < 4; i++)
        {
            switch (type)
            {
                case InputType.UpSwipe:
                    if (disDown.magnitude < disTop.magnitude)
                        type = InputType.DownSwipe;
                    if (disLeft.magnitude < disTop.magnitude)
                        type = InputType.LeftSwipe;
                    if (disRight.magnitude < disTop.magnitude)
                        type = InputType.RightSwipe;
                    break;
                case InputType.DownSwipe:
                    if (disTop.magnitude < disDown.magnitude)
                        type = InputType.UpSwipe;
                    if (disLeft.magnitude < disDown.magnitude)
                        type = InputType.LeftSwipe;
                    if (disRight.magnitude < disDown.magnitude)
                        type = InputType.RightSwipe;
                    break;
                case InputType.RightSwipe:
                    if (disDown.magnitude < disRight.magnitude)
                        type = InputType.DownSwipe;
                    if (disLeft.magnitude < disRight.magnitude)
                        type = InputType.LeftSwipe;
                    if (disTop.magnitude < disRight.magnitude)
                        type = InputType.UpSwipe;
                    break;
                case InputType.LeftSwipe:
                    if (disDown.magnitude < disLeft.magnitude)
                        type = InputType.DownSwipe;
                    if (disTop.magnitude < disLeft.magnitude)
                        type = InputType.UpSwipe;
                    if (disRight.magnitude < disLeft.magnitude)
                        type = InputType.RightSwipe;
                    break;
            }
        }
        return type;
    }

    void EvaluateInputSkyisland()
    {
        Ray ray;

        Touch touch = new Touch();

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            ray = CameraController.Instance.activeCamera.ScreenPointToRay(touch.position);
        }
        else if (Input.GetMouseButton(0))
        {
            ray = CameraController.Instance.activeCamera.ScreenPointToRay(Input.mousePosition);
        }
        else { return; }

        IslandController.Instance.skyIsland.TakeInput(ray);
    }

    void EvaluateInputTemple()
    {
        Ray ray;

        Touch touch = new Touch();

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            ray = CameraController.Instance.activeCamera.ScreenPointToRay(touch.position);
        }
        else if (Input.GetMouseButton(0))
        {
            ray = CameraController.Instance.activeCamera.ScreenPointToRay(Input.mousePosition);
        }
        else { return; }

        IslandController.Instance.temple.TakeInput(ray);
    }

    // void FixedUpdate()
    // {
    //     Ray ray;

    //     Touch touch = new Touch();
    //     bool touched = false;

    //     if (Input.touchCount > 0)
    //     {
    //         touched = true;
    //         touch = Input.GetTouch(0);
    //         ray = CameraController.Instance.activeCamera.ScreenPointToRay(touch.position);
    //     }
    //     else if(Input.GetMouseButton(0))
    //     {
    //         ray = CameraController.Instance.activeCamera.ScreenPointToRay(Input.mousePosition);
    //     }
    //     else { return; }

    //     switch(IslandController.Instance.activeIsland)
    //     {
    //         case Island.SkyIsland:
    //             IslandController.Instance.skyIsland.TakeInput(ray);
    //             break;
    //         case Island.TempleOfDoom:
    //             IslandController.Instance.temple.TakeInput(ray);
    //             break;
    //         case Island.ChickenFight:
    //             if (touched)
    //                 IslandController.Instance.fight.TakeInput(touch);
    //             else
    //                 IslandController.Instance.fight.TakeInput(ray);
    //             break;
    //     }
    // }
}

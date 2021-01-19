using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraPosition
{
    Top,
    LowerTop,
    Aurea
}
public class FightCameraController : MonoBehaviour
{
    [SerializeField]
    Transform cameraTopPosition = null;

    [SerializeField]
    Transform cameraLowerTopPosition = null;

    [SerializeField]
    List<Transform> aureaPositions = new List<Transform>();

    [SerializeField]
    FollowTarget followTarget = null;

    [SerializeField]
    CameraPosition position = CameraPosition.LowerTop;

    [SerializeField]
    int activeAurea = 2;
    public void TakeInput(InputType _type)
    {
        if (Player.Instance.IsArOn()) return;

        Debug.Log(_type);

        switch (position)
        {
            case CameraPosition.Top:
                if (_type == InputType.DownSwipe)
                    position = CameraPosition.LowerTop;
                break;
            case CameraPosition.LowerTop:
                if (_type == InputType.UpSwipe)
                    position = CameraPosition.Top;
                if (_type == InputType.DownSwipe)
                    position = CameraPosition.Aurea;
                break;
            case CameraPosition.Aurea:
                if (_type == InputType.UpSwipe)
                    position = CameraPosition.LowerTop;
                if (_type == InputType.RightSwipe) {
                    activeAurea++;
                    activeAurea = Mathf.Clamp(activeAurea, 0, aureaPositions.Count - 1);
                }
                if (_type == InputType.LeftSwipe) {
                    activeAurea--;
                    activeAurea = Mathf.Clamp(activeAurea, 0, aureaPositions.Count - 1);
                }
                break;
        }
        ChangePosition();
    }

    void ChangePosition()
    {
        switch (position)
        {
            case CameraPosition.Top:
                followTarget.TakeTarget(cameraTopPosition);
                break;
            case CameraPosition.LowerTop:
                followTarget.TakeTarget(cameraLowerTopPosition);
                break;
            case CameraPosition.Aurea:
                followTarget.TakeTarget(aureaPositions[activeAurea]);
                break;
        }
    }
}

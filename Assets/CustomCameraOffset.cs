using Unity.Cinemachine;
using UnityEngine;

public class CustomCameraOffset : MonoBehaviour
{
    public CinemachineCamera Camera;
    public CinemachinePositionComposer PositionComposer;

    private void Start()
    {
        PositionComposer = Camera.GetComponent<CinemachinePositionComposer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        PositionComposer.TargetOffset.y = -1.8f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Follow Camera component
//Makes a camera object follow a given target
public class FollowCamera : MonoBehaviour
{
    //The target for the camera to follow
    public Transform target;

    //Tilemap to define camera bounds
    public Tilemap tilemap;

    //Offset of the camera's position from the target's position
    public Vector2 offset;
    //Smoothing factor to adjust how smoothly the camera will move
    public float smoothing;

    //Half the dimesions of the camera viewport (calculated on Start())
    private Vector2 _viewportHalfSize;

    //Camera bounds
    private float _leftBoundary;
    private float _rightBoundary;
    private float _bottomBoundary;
    private float _topBoundary;

    //Reference to the Camera component
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();

        tilemap.CompressBounds();
        CalculateBounds();
    }

    //Calculate camera bounds using tilemap
    private void CalculateBounds()
    {
        _viewportHalfSize = new(_camera.aspect * _camera.orthographicSize, _camera.orthographicSize);

        _leftBoundary = tilemap.transform.position.x + tilemap.cellBounds.min.x + _viewportHalfSize.x;
        _rightBoundary = tilemap.transform.position.x + tilemap.cellBounds.max.x - _viewportHalfSize.x;
        _bottomBoundary = tilemap.transform.position.y + tilemap.cellBounds.min.y + _viewportHalfSize.y;
        _topBoundary = tilemap.transform.position.y + tilemap.cellBounds.max.y - _viewportHalfSize.y;
    }

    private void LateUpdate()
    {
        //Calculate movement for the frame
        Vector3 desiredPosition = target.position + new Vector3(offset.x, offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 1 - Mathf.Exp(-smoothing * Time.deltaTime));

        //Clamp movement to bounds
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, _leftBoundary, _rightBoundary);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, _bottomBoundary, _topBoundary);

        transform.position = smoothedPosition;
    }
}

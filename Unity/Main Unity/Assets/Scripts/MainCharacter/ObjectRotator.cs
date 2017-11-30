using UnityEngine;
using System.Collections;

public class ObjectRotator : MonoBehaviour
{
    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;
    private bool _canRotate;

    public float sphereRadius = 2.0f;
    public float maxCastDist = 5.0f;

    void Start()
{
    _sensitivity = 0.4f;
    _rotation = Vector3.zero;
}

void Update()
{
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        RaycastHit hitInfo;

        if (_isRotating)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {                               
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            // apply rotation
            _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;

            // rotate
            transform.Rotate(_rotation);

            // store mouse
            _mouseReference = Input.mousePosition;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _mouseOffset = (Input.mousePosition - _mouseReference);

            _rotation.x = -(_mouseOffset.y + _mouseOffset.y)* _sensitivity;

            // rotate
            transform.Rotate(_rotation);

            // store mouse
            _mouseReference = Input.mousePosition;
        }
    }
        if (Physics.SphereCast(origin, sphereRadius, direction, out hitInfo, maxCastDist))
        {
            _canRotate = true;
            Debug.Log("Rotate!!!!!!!!!!!");
        }
        else
        {
            _canRotate = false;
        }       
}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && _canRotate == true)
        {

            // rotating mirror
            _isRotating = true;

            // store mouse
            _mouseReference = Input.mousePosition;

        }
    }
void OnMouseDown()
{
        if (_canRotate == true)
        {
            // rotating mirror
            _isRotating = true;

            // store mouse
            _mouseReference = Input.mousePosition;
        }

}

void OnMouseUp()
{
    // rotating mirror
    _isRotating = false;
}

}

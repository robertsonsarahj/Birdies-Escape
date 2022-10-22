using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] float _moveSpeed = .1f;
    [SerializeField] float _moveDistance = .05f;
    Rigidbody2D rigidbody;
    private Vector2 _originalPosition;
    private Vector2 _topPosition;
    private Vector2 _bottomPosition;
    private string _currentMove = "up";

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _originalPosition = transform.position;
        _topPosition = _originalPosition + new Vector2(0, _moveDistance);
        _bottomPosition = _originalPosition + new Vector2(0, -_moveDistance);
    }

    void FixedUpdate()
    {
        move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void move()
    {
        Vector3 movementVector = transform.position;
        if (_currentMove.Equals("up") && transform.position.y < _topPosition.y)
        {
            movementVector = (rigidbody.transform.position + new Vector3(0, _moveSpeed * Time.fixedDeltaTime, 0));
        }
        else if (_currentMove.Equals("down") && transform.position.y > _bottomPosition.y)
        {
            movementVector = (rigidbody.transform.position + new Vector3(0, -_moveSpeed * Time.fixedDeltaTime, 0));
        }
        else
        {
            if (_currentMove.Equals("up"))
            {
                _currentMove = "down";
                movementVector = (rigidbody.transform.position + new Vector3(0, -_moveSpeed * Time.fixedDeltaTime, 0));
            }
            else if (_currentMove.Equals("down"))
            {
                _currentMove = "up";
                movementVector = (rigidbody.transform.position + new Vector3(0, _moveSpeed * Time.fixedDeltaTime, 0));
            }
        }
        rigidbody.transform.position = movementVector;
    }
}

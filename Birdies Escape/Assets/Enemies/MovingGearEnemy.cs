using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGearEnemy : MonoBehaviour
{
    [SerializeField] Sprite _sprite1;
    [SerializeField] Sprite _sprite2;
    [SerializeField] float _animationSpeed = .25f;
    [SerializeField] float _moveSpeed = 1f;
    [SerializeField] float _moveDistance = 1;
    [SerializeField] string _currentMove = "right";
    Rigidbody2D rigidbody;
    private int _sprite;
    private Vector2 _originalPosition;
    private Vector2 _rightPosition;
    private Vector2 _leftPosition;
    private float _timePassed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _originalPosition = transform.position;
        _rightPosition = _originalPosition + new Vector2(_moveDistance, 0);
        _leftPosition = _originalPosition + new Vector2(-_moveDistance, 0);
        GetComponent<SpriteRenderer>().sprite = _sprite1;
        _sprite = 1;
        _timePassed = 0;
    }

    void animate()
    {
        if(_sprite == 1)
        {
            GetComponent<SpriteRenderer>().sprite = _sprite2;
            _sprite = 2;
        }
        else if(_sprite == 2)
        {
            GetComponent<SpriteRenderer>().sprite = _sprite1;
            _sprite = 1;
        }
    }

    void FixedUpdate()
    {
        move();
    }

    // Update is called once per frame
    void Update()
    {
        _timePassed += Time.deltaTime;
        if(_timePassed > _animationSpeed)
        {
            animate();
            _timePassed = 0;
        }
    }

    void move()
    {
        Vector3 movementVector = transform.position;
        if (_currentMove.Equals("right") && transform.position.x < _rightPosition.x)
        {
            movementVector = (rigidbody.transform.position + new Vector3(_moveSpeed * Time.fixedDeltaTime, 0, 0));
        }
        else if (_currentMove.Equals("left") && transform.position.x > _leftPosition.x)
        {
            movementVector = (rigidbody.transform.position + new Vector3(-_moveSpeed * Time.fixedDeltaTime, 0, 0));
        }
        else
        {
            if (_currentMove.Equals("right"))
            {
                _currentMove = "left";
                movementVector = (rigidbody.transform.position + new Vector3(-_moveSpeed * Time.fixedDeltaTime, 0, 0));
            }
            else if (_currentMove.Equals("left"))
            {
                _currentMove = "right";
                movementVector = (rigidbody.transform.position + new Vector3(_moveSpeed * Time.fixedDeltaTime, 0, 0));
            }
        }
        rigidbody.transform.position = movementVector;
    }
}

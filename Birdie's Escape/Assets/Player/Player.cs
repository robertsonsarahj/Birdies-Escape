using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] LevelController _levelController;
    [SerializeField] GameObject _deadPlayer;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] float _moveForce = 2f;
    Rigidbody2D rigidbody;
    GameObject dead;
    bool _isDead = false;
    bool _started = false;
    float _horizontalMove = 0f;
    bool _jump = false;
    Vector2 _originalPosition;
    Quaternion _originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _started = false;
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_started)
        {
            transform.position = _originalPosition;
        }
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _moveForce;
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.X)) && !_started)
        {
            _started = true;
            _jump = true;
            rigidbody.velocity = Vector2.zero;
        }
        else if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.X)) && _started)
        {
            _jump = true;
        }
    }

    void FixedUpdate()
    {
        move();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name.ToLower().Contains("enemy"))
        {
            Transform deadPos = rigidbody.transform;

            _isDead = true;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            this.GetComponent<SpriteRenderer>().enabled = false;

            dead = Instantiate(_deadPlayer, (deadPos.position + new Vector3(0, 0, -1)), deadPos.rotation);
            Rigidbody2D deadRigidbody = dead.GetComponent<Rigidbody2D>();
            deadRigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

            Invoke("respawn", 2.5f);
        }
    }

    void respawn()
    {
        rigidbody.transform.position = _levelController._spawn;
        rigidbody.constraints = RigidbodyConstraints2D.None;
        this.GetComponent<SpriteRenderer>().enabled = true;
        _started = false;
        _isDead = false;

        Destroy(dead);
    }

    void move()
    {
        if(_started && !_isDead)
        {
            if(_jump)
            {
                rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _jump = false;
            }
            Vector3 movementVector = (rigidbody.transform.position + new Vector3(_horizontalMove * Time.fixedDeltaTime, 0, 0));
            //rigidbody.MovePosition(movementVector);
            rigidbody.transform.position = movementVector;
        }
        transform.rotation = _originalRotation;
    }
}

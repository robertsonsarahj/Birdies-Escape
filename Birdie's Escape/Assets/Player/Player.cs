using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] LevelController _levelController;
    [SerializeField] Sprite _restSprite;
    [SerializeField] Sprite _spriteJump;
    [SerializeField] Sprite _spriteFall;
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
    Vector2 _oldPosition;
    Quaternion _originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _started = false;
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
        _oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_started)
        {
            GetComponent<SpriteRenderer>().sprite = _restSprite;
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

        if(transform.position.y > _oldPosition.y || _jump == true)
        {
            GetComponent<SpriteRenderer>().sprite = _spriteJump;
        }
        else if(transform.position.y < _oldPosition.y)
        {
            GetComponent<SpriteRenderer>().sprite = _spriteFall;
        }
        _oldPosition = transform.position;
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

            this.GetComponent<AudioSource>().Play();

            dead = Instantiate(_deadPlayer, (deadPos.position + new Vector3(0, 0, -1)), deadPos.rotation);
            Rigidbody2D deadRigidbody = dead.GetComponent<Rigidbody2D>();
            deadRigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Invoke("movePlayerOnDeath", 0.1f);
            Invoke("respawn", 2.5f);
        }
        else if(collision.gameObject.name.ToLower().Contains("key"))
        {
            _levelController.collectKey(collision.gameObject);
        }
        else if(collision.gameObject.name.ToLower().Contains("portal"))
        {
            _levelController.enterPortal();
        }
    }

    void respawn()
    {
        rigidbody.constraints = RigidbodyConstraints2D.None;
        this.GetComponent<SpriteRenderer>().enabled = true;
        _started = false;
        _isDead = false;

        Destroy(dead);
        _levelController.respawn();
    }

    void movePlayerOnDeath()
    {
        rigidbody.transform.position = _levelController._spawn;
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
            rigidbody.transform.position = movementVector;
        }
        transform.rotation = _originalRotation;
    }
}

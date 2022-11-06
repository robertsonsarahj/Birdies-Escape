using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] GameObject _enemies;
    [SerializeField] GameObject _keys;
    [SerializeField] GameObject _portal;
    [SerializeField] Image _black;
    [SerializeField] Animator _animator;
    [SerializeField] public Vector2 _spawn = new Vector2(0, 0);
    private float _timePassed;
    private int _keysCollected;
    private int _totalKeys;
    private bool _keysActive;
    private bool _portalActive;

    // Start is called before the first frame update
    void Start()
    {
        _animator.Play("FadeOut");
        _totalKeys = _keys.transform.childCount;
        Debug.Log(_totalKeys);
        _keysCollected = 0;
        _timePassed = 0;
        _keysActive = false;
        setKeysInactive();
        setPortalInactive();
    }

    // Update is called once per frame
    void Update()
    {
        _timePassed += Time.deltaTime;
        if(_timePassed > 3f && _keysActive == false)
        {
            setKeysActive();
        }
        if(_keysCollected == _totalKeys)
        {
            setPortalActive();
        }
    }

    public void respawn()
    {
        _timePassed = 0;
        _keysCollected = 0;
        setKeysInactive();
        setPortalInactive();
    }

    public void enterPortal()
    {
        //_player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //_player.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(fading());
    }

    public void collectKey(GameObject k)
    {
        foreach(Transform key in _keys.GetComponentInChildren<Transform>())
        {
            if (key.gameObject == k)
            {
                this.GetComponent<AudioSource>().Play();
                key.gameObject.SetActive(false);
                key.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            }
        }
        _keysCollected++;
    }

    IEnumerator fading()
    {
        _animator.Play("FadeIn");
        yield return new WaitUntil(()=>_black.color.a==1);
        if(SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }

    void setKeysActive()
    {
        _keysActive = true;
        foreach (Transform key in _keys.GetComponentInChildren<Transform>())
        {
            key.gameObject.SetActive(true);
            key.gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    void setKeysInactive()
    {
        _keysActive = false;
        foreach (Transform key in _keys.GetComponentInChildren<Transform>())
        {
            key.gameObject.SetActive(false);
            key.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    void setPortalActive()
    {
        _portalActive = true;
        _portal.SetActive(true);
        _portal.GetComponent<Rigidbody2D>().simulated = true;
    }

    void setPortalInactive()
    {
        _portalActive = false;
        _portal.SetActive(false);
        _portal.GetComponent<Rigidbody2D>().simulated = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] Image _black;
    [SerializeField] Animator _animator;
    [SerializeField] TextMeshProUGUI _text1;
    [SerializeField] TextMeshProUGUI _text2;
    [SerializeField] MainMenuController _mainMenuController;

    // Start is called before the first frame update
    void Start()
    {
        _mainMenuController = UnityEngine.Object.FindObjectOfType<MainMenuController>();
        _animator.Play("FadeOut");
        StartCoroutine(showText(1f, _text1));
        StartCoroutine(showText(1f, _text2));
    }

    // Update is called once per frame
    void Update()
    {

        if(_mainMenuController == null)
        {
            _mainMenuController = UnityEngine.Object.FindObjectOfType<MainMenuController>();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enterPortal();
        }
        foreach (Button button in GameObject.FindObjectsOfType<Button>())
        {
            button.GetComponent<SpriteRenderer>().enabled = false;
            button.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (_mainMenuController._text1.color.a != 0f)
        {
            var color = _mainMenuController._text1.color;
            color.a = 0f;
            _mainMenuController._text1.color = color;
        }
    }

    public void enterPortal()
    {
        //_player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //_player.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(fading());
        StartCoroutine(fadeText(1f, _text1));
        StartCoroutine(fadeText(1f, _text2));
    }

    IEnumerator fading()
    {
        _animator.Play("FadeIn");
        yield return new WaitUntil(() => _black.color.a == 1);
        if (SceneManager.GetActiveScene().name.Equals("End Menu"))
        {
            SceneManager.LoadScene(0);
        }
        else if(SceneManager.GetActiveScene().name.Equals("Start Menu"))
        {
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings-1);
            /*if(_mainMenuController != null)
            {
                _mainMenuController.Start();
            }*/
            _mainMenuController._animator.Play("FadeIn");
        }
    }

    IEnumerator fadeText(float t, TextMeshProUGUI i)
    {
        if(i != null)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
            while (i.color.a > 0.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
                yield return null;
            }
        }
    }

    IEnumerator showText(float t, TextMeshProUGUI i)
    {
        if (i != null)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
            while (i.color.a < 1.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
                yield return null;
            }
        }
    }
}

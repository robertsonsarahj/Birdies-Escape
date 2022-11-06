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

    // Start is called before the first frame update
    void Start()
    {
        _animator.Play("FadeOut");
        StartCoroutine(showText(1f, _text1));
        StartCoroutine(showText(1f, _text2));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enterPortal();
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
        if (SceneManager.GetActiveScene().buildIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
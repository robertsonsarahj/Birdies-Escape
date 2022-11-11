using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    Canvas _canvas;
    public Image _black;
    public Animator _animator;
    public TextMeshProUGUI _text1;
    TextMeshProUGUI _text2;
    public Button Level1Button;
    public Button Level2Button;
    public Button Level3Button;
    public Button Level4Button;
    public Button Level5Button;
    public Button Level6Button;
    public Button Level7Button;
    public Button Level8Button;
    public Button Level9Button;

    void Awake()
    {
        var go = new GameObject("Pointer");
        DontDestroyOnLoad(go);

        MainMenuController[] mainMenus = GameObject.FindObjectsOfType<MainMenuController>();
        Debug.Log(mainMenus.Length);
        if(mainMenus.Length > 1)
        {
            foreach(GameObject gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if(gameObject.name.Equals("MainMenuController"))
                {
                    Destroy(gameObject);
                }
            }
        }

        Button[] savedButtons = GameObject.FindObjectsOfType<Button>();
        Debug.Log(savedButtons.Length);
        if(savedButtons.Length > 9)
        {
            if(Level1Button == null)
            {
                foreach (GameObject button in go.scene.GetRootGameObjects())
                {
                    if (button.name.Contains("Button"))
                    {
                        if (button.name.Contains("1"))
                            Level1Button = button.GetComponent<Button>();
                        else if (button.name.Contains("2"))
                            Level2Button = button.GetComponent<Button>();
                        else if (button.name.Contains("3"))
                            Level3Button = button.GetComponent<Button>();
                        else if (button.name.Contains("4"))
                            Level4Button = button.GetComponent<Button>();
                        else if (button.name.Contains("5"))
                            Level5Button = button.GetComponent<Button>();
                        else if (button.name.Contains("6"))
                            Level6Button = button.GetComponent<Button>();
                        else if (button.name.Contains("7"))
                            Level7Button = button.GetComponent<Button>();
                        else if (button.name.Contains("8"))
                            Level8Button = button.GetComponent<Button>();
                        else if (button.name.Contains("9"))
                            Level9Button = button.GetComponent<Button>();
                    }
                }
            }
            foreach (GameObject button in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if(button.name.Contains("Button"))
                {
                    Destroy(button);
                }
            }
        }
        else
        {
            if(Level1Button == null)
            {
                foreach (GameObject button in SceneManager.GetActiveScene().GetRootGameObjects())
                {
                    if (button.name.Contains("Button"))
                    {
                        if (button.name.Contains("1"))
                            Level1Button = button.GetComponent<Button>();
                        else if (button.name.Contains("2"))
                            Level2Button = button.GetComponent<Button>();
                        else if (button.name.Contains("3"))
                            Level3Button = button.GetComponent<Button>();
                        else if (button.name.Contains("4"))
                            Level4Button = button.GetComponent<Button>();
                        else if (button.name.Contains("5"))
                            Level5Button = button.GetComponent<Button>();
                        else if (button.name.Contains("6"))
                            Level6Button = button.GetComponent<Button>();
                        else if (button.name.Contains("7"))
                            Level7Button = button.GetComponent<Button>();
                        else if (button.name.Contains("8"))
                            Level8Button = button.GetComponent<Button>();
                        else if (button.name.Contains("9"))
                            Level9Button = button.GetComponent<Button>();
                    }
                }
            }
        }

        int canvasCount = 0;
        foreach (Canvas canvas in GameObject.FindObjectsOfType<Canvas>())
        {
            if (canvas.name.Equals("MenuCanvas"))
            {
                canvasCount++;
            }
        }

        Debug.Log(canvasCount);
        if (canvasCount > 1)
        {
            if(_canvas == null)
            {
                foreach (GameObject canvas in go.scene.GetRootGameObjects())
                {
                    if (canvas.name.Equals("MenuCanvas"))
                    {
                        _canvas = canvas.GetComponent<Canvas>();
                    }
                }
            }
            foreach (GameObject canvas in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (canvas.name.Equals("MenuCanvas"))
                {
                    Destroy(canvas);
                }
            }
        }
        else
        {
            if(_canvas == null)
            {
                foreach (GameObject canvas in SceneManager.GetActiveScene().GetRootGameObjects())
                {
                    if (canvas.name.Equals("MenuCanvas"))
                    {
                        _canvas = canvas.GetComponent<Canvas>();
                    }
                }
            }
        }

        Destroy(go);

        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this.Level1Button);
        DontDestroyOnLoad(this.Level2Button);
        DontDestroyOnLoad(this.Level3Button);
        DontDestroyOnLoad(this.Level4Button);
        DontDestroyOnLoad(this.Level5Button);
        DontDestroyOnLoad(this.Level6Button);
        DontDestroyOnLoad(this.Level7Button);
        DontDestroyOnLoad(this.Level8Button);
        DontDestroyOnLoad(this.Level9Button);
        DontDestroyOnLoad(this._canvas);

        if(_black == null) {
            for (int i = 0; i < this._canvas.transform.childCount; i++)
            {
                if (this._canvas.transform.GetChild(i).name.Equals("Image"))
                {
                    this._black = this._canvas.transform.GetChild(i).GetComponent<Image>();
                    break;
                }
            }
        }
        if(_animator == null)
        {
            this._animator = this._black.GetComponent<Animator>();
        }
        if(_text1 == null)
        {
            for (int i = 0; i < this._canvas.transform.childCount; i++)
            {
                if (this._canvas.transform.GetChild(i).name.Equals("Text (TMP)"))
                {
                    this._text1 = this._canvas.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                    break;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Level1Button.unlocked = true;
        foreach(TextMeshProUGUI text in GameObject.FindObjectsOfType<TextMeshProUGUI>())
        {
            Debug.Log(text.name);
            if (text.name.Equals("Text (TMP)"))
                this._text1 = text;
        }
        _animator.Play("FadeOut");
        StartCoroutine(showText(1f, _text1));
        StartCoroutine(showText(1f, _text2));
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name + " was loaded!");
        if(scene.name.Equals("Level Menu"))
        {
            Debug.Log("we are here! " + _black.color.a);
            var color = _black.color; color.a = 1f;
            _black.color = color;
            //Debug.Log("color: " + _black.color.a);
            //_animator.Play("FadeIn");
            _animator.Play("FadeOut");
            StartCoroutine(showText(1, _text1));
            StartCoroutine(showText(1, _text2));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.name.Contains("Level") && rayHit.collider.gameObject.name.Contains("Button"))
                {
                    string scene = rayHit.collider.gameObject.name.Replace("Level", "").Replace("Button", "");
                    int sceneNum = 0; int.TryParse(scene, out sceneNum);
                    enterScene(sceneNum);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name.Equals("Level Menu"))
        {
            enterMenu();
        }
        //Debug.Log(_black.color.a);
    }

    public void enterScene(int sceneNumber)
    {
        //_player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //_player.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(fading(sceneNumber));
        StartCoroutine(fadeText(1f, _text1));
        StartCoroutine(fadeText(1f, _text2));
    }

    public void enterMenu()
    {
        //_player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //_player.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(menuFading());
        StartCoroutine(fadeText(1f, _text1));
        StartCoroutine(fadeText(1f, _text2));
    }

    IEnumerator menuFading()
    {
        _animator.Play("FadeIn");
        yield return new WaitUntil(() => _black.color.a == 1);
        SceneManager.LoadScene(0);
        _animator.Play("FadeOut");
    }

    IEnumerator fading(int sceneNumber)
    {
        _animator.Play("FadeIn");
        yield return new WaitUntil(() => _black.color.a == 1);
        SceneManager.LoadScene(sceneNumber);
        _animator.Play("FadeOut");
    }

    IEnumerator loadFading()
    {
        _animator.Play("FadeIn");
        yield return new WaitUntil(() => _black.color.a == 1);
        _animator.Play("FadeOut");
    }

    IEnumerator fadeText(float t, TextMeshProUGUI i)
    {
        Debug.Log("fading text " + i.name);
        if (i != null)
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
        Debug.Log("showing text " + i.name);
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

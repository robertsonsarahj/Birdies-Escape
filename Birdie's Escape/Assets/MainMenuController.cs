using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    Image _black;
    public Animator _animator;
    TextMeshProUGUI _text1;
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
    //List<Button> buttons;

    void Awake()
    {
        MainMenuController[] mainMenus = GameObject.FindObjectsOfType<MainMenuController>();
        Debug.Log(mainMenus.Length);
        if(mainMenus.Length > 1)
        {
            Destroy(this.gameObject);
        }

        var go = new GameObject("Pointer");
        DontDestroyOnLoad(go);

        Button[] savedButtons = GameObject.FindObjectsOfType<Button>();
        Debug.Log(savedButtons.Length);
        if(savedButtons.Length > 9)
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

        int animatorCount = 0;
        foreach (Image animator in GameObject.FindObjectsOfType<Image>())
        {
            if (animator.name.Equals("AnimatorImage"))
            {
                animatorCount++;
            }
        }

        Debug.Log(animatorCount);
        if (animatorCount > 1)
        {
            foreach (GameObject animator in go.scene.GetRootGameObjects())
            {
                if (animator.name.Equals("AnimatorImage"))
                {
                    _animator = animator.GetComponent<Animator>();
                }
            }
            foreach (GameObject animator in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (animator.name.Equals("AnimatorImage"))
                {
                    Destroy(animator);
                }
            }
        }
        else
        {
            foreach(GameObject animator in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if(animator.name.Equals("AnimatorImage"))
                {
                    _animator = animator.GetComponent<Animator>();
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
        DontDestroyOnLoad(this._animator);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        this._black = GameObject.FindObjectOfType<Image>();
        //this._animator = _black.GetComponent<Animator>();
        Level1Button.unlocked = true;
        foreach(TextMeshProUGUI text in GameObject.FindObjectsOfType<TextMeshProUGUI>())
        {
            Debug.Log(text.name);
            if (text.name.Equals("Text (TMP)"))
                this._text1 = text;
        }
        //this.buttons = new List<Button>(){Level1Button, Level2Button, Level3Button, Level4Button, Level5Button, Level6Button, Level7Button, Level8Button, Level9Button};
        _animator.Play("FadeOut");

        StartCoroutine(showText(1f, _text1));
        StartCoroutine(showText(1f, _text2));
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
    }

    /*public void unlockLevel(string levelName)
    {
        foreach(Button button in buttons)
        {
            if(button.name.Contains(levelName))
            {
                button.unlocked = true;
                break;
            }
        }
    }*/

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
    }

    IEnumerator menuFading()
    {
        //_animator.Play("FadeIn");
        yield return new WaitUntil(() => _black.color.a == 0);
        SceneManager.LoadScene(0);
    }

    IEnumerator fading(int sceneNumber)
    {
        //_animator.Play("FadeIn");
        yield return new WaitUntil(() => _black.color.a == 0);
        SceneManager.LoadScene(sceneNumber);
    }

    IEnumerator fadeText(float t, TextMeshProUGUI i)
    {
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

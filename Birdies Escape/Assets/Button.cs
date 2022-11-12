using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] Sprite _unlockedLevel;
    [SerializeField] Sprite _lockedLevel;
    public bool unlocked = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (unlocked == true)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<SpriteRenderer>().sprite = _unlockedLevel;
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if(unlocked == false)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<SpriteRenderer>().sprite = _lockedLevel;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

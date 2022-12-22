using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endEffect : MonoBehaviour
{
    [Header("Components")]

    [Tooltip("The Animator needed for the transition")]
    [SerializeField] private Animator anim;

    [Header("UI")]

    [Tooltip("The text that displayes how many people have been Rescued")]
    [SerializeField] private TMPro.TextMeshProUGUI textDisplay;

    [Tooltip("The text for the win and lose state")]
    [SerializeField] private string[] endText = {"Passed","Lost"};

    [Header("Private data")]
    private bool passed = false;

    public void setState(bool win)
    {
        passed = win;
        anim.SetBool("Show",true);
        textDisplay.text = win ? endText[0] : endText[1];
        Invoke("animationTrigger",2.3f);
    }

    private void animationTrigger()
    {
        if(passed)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

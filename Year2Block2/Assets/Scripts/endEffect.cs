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
    private int loadScene;

    /// <summary>
    /// Called when the game is either lost or won then it shows the transition
    /// </summary>
    public void loadGameTransition()
    {
        loadScene = 1;
        anim.SetBool("Show",true);
        Invoke("animationTrigger",1.5f);
    }
    
    /// <summary>
    /// Called when the game is either lost or won then it shows the transition
    /// </summary>
    public void setState(bool win)
    {
        loadScene = win ? 0 : SceneManager.GetActiveScene().buildIndex;
        anim.SetBool("Show",true);
        textDisplay.text = win ? endText[0] : endText[1];
        Invoke("animationTrigger",2.3f);
    }

    /// <summary>
    /// Loads the set scene after the transition animation
    /// </summary>
    private void animationTrigger()
    {
        SceneManager.LoadScene(loadScene);//loads the main scene again
    }
}

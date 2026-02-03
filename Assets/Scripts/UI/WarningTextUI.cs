using System.Collections;
using TMPro;
using UnityEngine;

public class WarningTextUI : MonoBehaviour
{
    public TextMeshProUGUI warningText;


    private void Start()
    {
        warningText.text = "";
        warningText.enabled = false;
    }


    public void ShowWarningText(string text)
    {
        StartCoroutine(ToggleWarningText(text));
    }


    /// <summary>
    /// Show warning text on the screen. Close it after two seconds.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    IEnumerator ToggleWarningText(string text)
    {
        warningText.text = text;
        warningText.enabled = true;
        yield return new WaitForSeconds(2);
        warningText.enabled = false;
    }
}

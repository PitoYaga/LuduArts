using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public GameObject interactionWindow;
    public Image holdingBar;
    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI itemNameText;

    public Image crosshairDefault;
    public Image crosshairHover;


    private void Start()
    {
        ResetInteractionWindow();
    }


    /// <summary>
    /// Setup interaction window. Get item name and interaction type
    /// </summary>
    /// <param name="interactionName"></param>
    /// <param name="itemName"></param>
    public void UpdateInteractionText(string interactionName, string itemName)
    {
        interactionText.SetText(interactionName);
        itemNameText.SetText(itemName);
        ShowHoverCrosshair(true);
    }


    /// <summary>
    /// Holding inpur progress bar
    /// </summary>
    /// <param name="percent"></param>
    public void UpdateHoldingBar(float percent)
    {
        holdingBar.fillAmount = percent;
    }


    /// <summary>
    /// Change crosshair type
    /// </summary>
    /// <param name="showDefault"></param>
    public void ShowHoverCrosshair(bool showDefault)
    {
        crosshairDefault.enabled = !showDefault;
        crosshairHover.enabled = showDefault;
    }


    /// <summary>
    /// Clear interaction window
    /// </summary>
    public void ResetInteractionWindow()
    {
        holdingBar.fillAmount = 0;
        interactionText.SetText("");
        itemNameText.SetText("");
        ShowHoverCrosshair(false);
    }


    /// <summary>
    /// Show warning text on the screen
    /// </summary>
    /// <param name="warningText"></param>
    public void ShowWarningText(string warningText)
    { 
        GetComponent<WarningTextUI>().ShowWarningText(warningText); 
    }
    
}

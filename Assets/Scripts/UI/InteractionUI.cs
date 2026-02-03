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

    public void UpdateInteractionText(string interactionName, string itemName)
    {
        interactionText.SetText(interactionName);
        itemNameText.SetText(itemName);
        ShowHoverCrosshair(true);
    }

    public void UpdateHoldingBar(float percent)
    {
        holdingBar.fillAmount = percent;
    }


    public void ShowHoverCrosshair(bool showDefault)
    {
        crosshairDefault.enabled = !showDefault;
        crosshairHover.enabled = showDefault;
    }


    public void ResetInteractionWindow()
    {
        holdingBar.fillAmount = 0;
        interactionText.SetText("");
        itemNameText.SetText("");
        ShowHoverCrosshair(false);
    }


    public void ShowWarningText(string warningText)
    { 
        GetComponent<WarningTextUI>().ShowWarningText(warningText); 
    }
    
}

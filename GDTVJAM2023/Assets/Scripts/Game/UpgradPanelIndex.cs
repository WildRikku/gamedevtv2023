using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UpgradPanelIndex : MonoBehaviour
{
    [Header("Panel Settings")]
    public int index;
    public Image panelImage;

    [Header("Panel Values")]
    public UpgradePanelController upgradePanelController;
    public Sprite spPanelSelect;
    public Sprite spPanelDeselcet;
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI descriptionText;
    public Image iconPanel;
    public TextMeshProUGUI mainClass;
    public TextMeshProUGUI subClass;

    public void SetDescription()
    { 
        // Build Panel description
        iconPanel.sprite = upgradePanelController.iconPanel[index];
        headerText.text = upgradePanelController.headerStr[index];
        descriptionText.text = upgradePanelController.descriptionTextStr[index];

        mainClass.text = upgradePanelController.mainClassStr[index];
        mainClass.color = upgradePanelController.mainClassColor[index];
        subClass.text = upgradePanelController.subClassStr[index];
        subClass.color = upgradePanelController.subClassColor[index];
    }

    public void OnMouseEnter_()
    {
        // Farbe des Panels �ndern, wenn die Maus �ber das Panel f�hrt
        upgradePanelController.UpdateValuePanelOnMouseEnter(index);
        panelImage.sprite = spPanelSelect;

        AudioManager.Instance.PlaySFX("MouseHover");
    }

    public void OnMouseExit_()
    {
        // Zur�ck zur Standardfarbe wechseln, wenn die Maus das Panel verl�sst
        panelImage.sprite = spPanelDeselcet;
        upgradePanelController.UpdateValuePanel();
    }

    public void OnMouseDown_()
    {
        upgradePanelController.ChooseAValue(index);
        AudioManager.Instance.PlaySFX("WindowOpen");
    }
}


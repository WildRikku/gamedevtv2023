using UnityEngine;
using UnityEngine.UI;

public class PanelInteraction : MonoBehaviour
{
    private Image panelImage;
    public Color defaultColor;
    public Color hoverColor;
    public int panelIndex;

    public GameObject panelUI;
    public GameObject playerUI;
    private GameManager gameManager;


    private void Start()
    {
        panelImage = GetComponent<Image>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
    }

    public void OnMouseEnter()
    {
        // Farbe des Panels �ndern, wenn die Maus �ber das Panel f�hrt
        panelImage.color = hoverColor;
    }

    public void OnMouseExit()
    {
        // Zur�ck zur Standardfarbe wechseln, wenn die Maus das Panel verl�sst
        panelImage.color = defaultColor;
    }

    public void OnMouseDown()
    {
        // Aktion ausf�hren, wenn das Panel angeklickt wird
        Debug.Log("Panel angeklickt!" + panelIndex);
        gameManager.gameIsPlayed = true;
        panelUI.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1;
        
    }


}

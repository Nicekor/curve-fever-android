using UnityEngine;
using UnityEngine.UI;

public class TypeButtons : MonoBehaviour
{
    public Button publicBtn;
    public Button privateBtn;
    public GameObject PasswordPanel;
    public Text publicBtnText;
    public Text privateBtnText;
    public Image publicBtnIco;
    public Image privateBtnIco;

	// TODO: Improve code, could render different stuff based on a boolean value
	public void OnPublicBtnClick()
	{
        RoomManager.roomType = "PUBLIC";

        publicBtn.GetComponent<Image>().color = Theme.primaryBtnBgColour;
        publicBtnText.color = Theme.primaryBtnTextColour;
        publicBtnIco.enabled = true;

        PasswordPanel.SetActive(false);

        privateBtn.GetComponent<Image>().color = Theme.notSelectedBtnColour;
        privateBtnText.color = Theme.notSelectedTextColour;
        privateBtnIco.enabled = false;
    }

    public void OnPrivateBtnClick()
	{
        RoomManager.roomType = "PRIVATE";

        publicBtn.GetComponent<Image>().color = Theme.notSelectedBtnColour;
        publicBtnText.color = Theme.notSelectedTextColour;
        publicBtnIco.enabled = false;

        PasswordPanel.SetActive(true);

        privateBtn.GetComponent<Image>().color = Theme.primaryBtnBgColour;
        privateBtnText.color = Theme.primaryBtnTextColour;
        privateBtnIco.enabled = true;
	}

}

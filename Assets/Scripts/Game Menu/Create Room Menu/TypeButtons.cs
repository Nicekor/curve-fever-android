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

    private Color notSelectedColour = new Vector4(0f, 0.1176471f, 0.1607843f, 1f);
    private Color selectedColour = new Vector4(0.2039216f, 0.6392157f, 0.8078431f, 1f);
    private Color selectedTextColour = Color.white;
	private Color notSelectedTextColour = new Vector4(0.03137255f, 0.2470588f, 0.3294118f, 1f);

	// TODO: Improve code, could render different stuff based on a boolean value
	public void OnPublicBtnClick()
	{
        RoomManager.roomType = "PUBLIC";

        publicBtn.GetComponent<Image>().color = selectedColour;
        publicBtnText.color = selectedTextColour;
        publicBtnIco.enabled = true;

        PasswordPanel.SetActive(false);

        privateBtn.GetComponent<Image>().color = notSelectedColour;
        privateBtnText.color = notSelectedTextColour;
        privateBtnIco.enabled = false;
    }

    public void OnPrivateBtnClick()
	{
        RoomManager.roomType = "PRIVATE";

        publicBtn.GetComponent<Image>().color = notSelectedColour;
        publicBtnText.color = notSelectedTextColour;
        publicBtnIco.enabled = false;

        PasswordPanel.SetActive(true);

        privateBtn.GetComponent<Image>().color = selectedColour;
        privateBtnText.color = selectedTextColour;
        privateBtnIco.enabled = true;
	}

}

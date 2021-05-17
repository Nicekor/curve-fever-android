using UnityEngine;
using UnityEngine.UI;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] private Text errorLabel;

    public void OnClick_CloseErrorPanel()
    {
        Destroy(gameObject);
    }

    public void Alert(string message)
    {
        errorLabel.text = message;
    }
}

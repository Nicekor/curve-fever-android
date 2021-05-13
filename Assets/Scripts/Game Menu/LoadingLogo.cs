using UnityEngine;
using UnityEngine.UI;

public class LoadingLogo : MonoBehaviour
{
	[SerializeField] private Text _infoText;
	[SerializeField] private GameObject _retryBtn;
	[SerializeField] private Animator _logoAnimator;
	public Text InfoText { get { return _infoText; } }
	public GameObject RetryBtn { get { return _retryBtn; } }
	public Animator LogoAnimator { get { return _logoAnimator; } }
}

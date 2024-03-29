﻿using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
	[SerializeField] private LoadingLogo loadingLogoPrefab;
	[SerializeField] private Transform canvasTransform;

	private string gameVersion = "1";
	private LoadingLogo loadingLogo;

	private void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
		loadingLogo = Instantiate(loadingLogoPrefab, canvasTransform);
		loadingLogo.RetryBtn.GetComponent<Button>().onClick.AddListener(Connect);
	}

	private void Start()
	{
		Connect();
	}

	public void Connect()
	{
		loadingLogo.InfoText.color = Color.white;
		loadingLogo.InfoText.text = "Connecting...";
		loadingLogo.LogoAnimator.enabled = true;
		loadingLogo.RetryBtn.SetActive(false);
		PhotonNetwork.ConnectUsingSettings();
		PhotonNetwork.GameVersion = gameVersion;
	}

	public override void OnConnectedToMaster()
	{
		Destroy(loadingLogo.gameObject);
		Debug.Log("Connecting Using Settings worked fine and I am now connected");
	}

	// Called after disconnecting from the Photon server. It could be a failure or an explicit disconnect call 
	public override void OnDisconnected(DisconnectCause cause)
	{
		loadingLogo.RetryBtn.SetActive(true);
		loadingLogo.LogoAnimator.enabled = false;
		loadingLogo.InfoText.color = Color.red;
		loadingLogo.InfoText.text = "Network connection failed";
		Debug.LogWarningFormat("Cause of disconnection: {0}", cause);
	}
}

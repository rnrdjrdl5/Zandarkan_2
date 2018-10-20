using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class NewLoadingPhoton : Photon.PunBehaviour{

    IEnumerator CoroLoading;

    private AsyncOperation async;

    private float LoadingData;

    public GameObject LoadingObject;
    public GameObject LoadingFinish;


    private Image LoadingCat;
    private Image LoadingMouse;
    private Image FadeImage;

    private bool isOnceLoadingFinishRPC;

    private void Awake()
    {
        LoadingCat = GameObject.Find("LoadingCat").GetComponent<Image>();
        LoadingMouse = GameObject.Find("LoadingMouse").GetComponent<Image>();
        FadeImage = GameObject.Find("FadeImage").GetComponent<Image>();

        LoadingData = 0.0f;
        InitPhotonView();

        CoroLoading = Loading();

        isOnceLoadingFinishRPC = false;





    }

    // Use this for initialization
    void Start () {

        // 플레이어 위치 씬 변경
        ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable { { "Scene", "Loading" } };
        PhotonNetwork.player.SetCustomProperties(ht);

        StartCoroutine(CoroLoading);

        // fade 효과주기
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {

        float count = 1.0f;
        while (true)
        {
            if (count <= 0) yield break;

            FadeImage.color = new Color(
                FadeImage.color.r,
                FadeImage.color.g,
                FadeImage.color.b,
                count);

            count -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3.0f);

        float count = 0.0f;
        while (true)
        {

            if (count >= 1)
            {
                async.allowSceneActivation = true;
                yield break;
            }

            FadeImage.color = new Color(
                FadeImage.color.r,
                FadeImage.color.g,
                FadeImage.color.b,
                count);

            count += Time.deltaTime;
            yield return null;
        }
    }
	
	// Update is called once per frame
	void Update () {

        CheckFinishAllLoading();

    }

    IEnumerator Loading()
    {
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;

        while (true)
        {

            LoadingData = async.progress;

            if (LoadingData >= 0.9f)
            {
                // 로딩완료
                ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable { { "Scene", "LoadingFinish" } };
                PhotonNetwork.player.SetCustomProperties(ht);
                Debug.Log("로딩완료.");
                StopCoroutine(CoroLoading);

            }

            else
            {
                Debug.Log("로딩중");
            }

            yield return null;
        }
       
    }

    private void CheckFinishAllLoading()
    {
        if (isOnceLoadingFinishRPC) return;

        if (photonView == null) return;

        if (!PhotonNetwork.isMasterClient) return;

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            string SceneState = (string)PhotonNetwork.playerList[i].CustomProperties["Scene"];
            if (SceneState != "LoadingFinish") return;

        }
        isOnceLoadingFinishRPC = true;

        photonView.RPC("RPCNextScene", PhotonTargets.All);

        


    }

    void InitPhotonView()
    {

        if (photonView == null)
            Debug.Log("photon 없음");
    }

    // RPC

    [PunRPC]
    void RPCNextScene()
    {
        StartCoroutine("FadeOut");
       // async.allowSceneActivation = true;
    }


}

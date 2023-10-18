using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GetRequest : MonoBehaviour
{
    [SerializeField] InputField tokenField;

    // API Gateway���쐬���āAResource Server�ɂ��w�肵��URL
    static string url = "https://ibkfffoclk.execute-api.ap-northeast-1.amazonaws.com/tettete";

    public void OnClick()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        Debug.Log(tokenField.text);
        string token = tokenField.text; // UserPools����擾����IdToken
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Authorization", token); // Authorization Header�̕t�^
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}
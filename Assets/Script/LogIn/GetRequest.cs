using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GetRequest : MonoBehaviour
{
    [SerializeField] InputField tokenField;

    // API Gateway‚ªì¬‚µ‚ÄAResource Server‚É‚àw’è‚µ‚½URL
    static string url = "https://ibkfffoclk.execute-api.ap-northeast-1.amazonaws.com/tettete";

    public void OnClick()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        Debug.Log(tokenField.text);
        string token = tokenField.text; // UserPools‚©‚çæ“¾‚µ‚½IdToken
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Authorization", token); // Authorization Header‚Ì•t—^
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
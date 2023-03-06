using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URL : MonoBehaviour
{
    public void onClick()
    {
        Application.OpenURL("https://sonarrrr.auth.ap-northeast-1.amazoncognito.com/login?client_id=75n394ll9d0o8u28pbv00vn5pd&response_type=code&scope=aws.cognito.signin.user.admin+email+openid+phone+profile&redirect_uri=https://www.example.com/cb");//""の中には開きたいWebページのURLを入力します
    }
}

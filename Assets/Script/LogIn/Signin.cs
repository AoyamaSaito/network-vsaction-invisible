using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Amazon;
using Amazon.Runtime;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;

public class Signin : MonoBehaviour
{

    public InputField emailField;
    public InputField passwordField;
    static string clientId = AWSCognitoIDs.AppClientId;
    static string userPoolId = AWSCognitoIDs.UserPoolId;

    public void OnClick()
    {
        Debug.Log("Start Signin");
        try
        {
            AuthenticateWithSrpAsync();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public async void AuthenticateWithSrpAsync()
    {
        var provider = new AmazonCognitoIdentityProviderClient(null, RegionEndpoint.USWest2);
        CognitoUserPool userPool = new CognitoUserPool(
            userPoolId,
            clientId,
            provider
        );
        CognitoUser user = new CognitoUser(
            emailField.text,
            clientId,
            userPool,
            provider
        );

        AuthFlowResponse context = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest()
        {
            Password = passwordField.text
        }).ConfigureAwait(false);

        Debug.Log(user.SessionTokens.IdToken);
    }
}
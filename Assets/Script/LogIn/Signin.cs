
using System;
using UnityEngine;
using UnityEngine.UI;
using Amazon.CognitoIdentityProvider; // for AmazonCognitoIdentityProviderClient
using Amazon.Extensions.CognitoAuthentication; // for CognitoUserPool
using Amazon; // for RegionEndpoint

public class Signin : MonoBehaviour
{
    public InputField emailField;
    public InputField passwordField;
    public Text resultText;
    static string appClientId = AWSCognitoIDs.AppClientId;
    static string userPoolId = AWSCognitoIDs.UserPoolId;

    public void OnClick()
    {
        try
        {
            AuthenticateWithSrpAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    public async void AuthenticateWithSrpAsync()
    {
        var provider = new AmazonCognitoIdentityProviderClient(null, RegionEndpoint.APNortheast1);
        CognitoUserPool userPool = new CognitoUserPool(
            userPoolId,
            appClientId,
            provider
        );
        CognitoUser user = new CognitoUser(
            emailField.text,
            appClientId,
            userPool,
            provider
        );

        AuthFlowResponse context = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest()
        {
            Password = passwordField.text
        }).ConfigureAwait(true);

        // for debug
        resultText.text = user.SessionTokens.IdToken;
    }
}

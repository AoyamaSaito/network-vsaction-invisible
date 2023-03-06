using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Amazon;
using Amazon.Runtime;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;

public class Signin : MonoBehaviour
{
    [SerializeField] InputField emailField;
    [SerializeField] InputField passwordField;
    [SerializeField] InputField tokenField;
    static string clientId = AWSCognitoIDs.AppClientId;
    static string userPoolId = AWSCognitoIDs.UserPoolId;

    public void OnClick()
    {
        Debug.Log("Start Signin");

        try
        {
            CallAuthenticateTask();
            Debug.Log("ƒTƒCƒ“ƒCƒ“¬Œ÷");
            Debug.Log("GET REQUEST‚ÉJWT‚ðŽ©“®“ü—Í");
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    private async void CallAuthenticateTask()
    {
        var context = SynchronizationContext.Current;
        AuthFlowResponse response = await AuthenticateWithSrpAsync();
        context.Post((state) => {
            tokenField.text = response.AuthenticationResult.IdToken;
        }, null);
    }

    private async Task<AuthFlowResponse> AuthenticateWithSrpAsync()
    {
        var provider = new AmazonCognitoIdentityProviderClient(null, RegionEndpoint.APNortheast1);
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

        return await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest()
        {
            Password = passwordField.text
        }).ConfigureAwait(false);
    }
}
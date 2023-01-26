
using System;
using UnityEngine;
using UnityEngine.UI;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

public class Confirmation : MonoBehaviour
{
    public InputField emailField;
    public InputField confirmationCodeField;
    static string appClientId = AWSCognitoIDs.AppClientId;

    public void OnClick()
    {
        var client = new AmazonCognitoIdentityProviderClient(null, Amazon.RegionEndpoint.APNortheast1);
        ConfirmSignUpRequest confirmSignUpRequest = new ConfirmSignUpRequest();

        confirmSignUpRequest.Username = emailField.text;
        confirmSignUpRequest.ConfirmationCode = confirmationCodeField.text;
        confirmSignUpRequest.ClientId = appClientId;

        try
        {
            ConfirmSignUpResponse confirmSignUpResult = client.ConfirmSignUp(confirmSignUpRequest);
            Debug.Log(confirmSignUpResult.ToString());
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
}

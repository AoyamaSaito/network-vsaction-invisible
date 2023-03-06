using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

public class Confirmation : MonoBehaviour
{

    [SerializeField] InputField emailField;
    [SerializeField] InputField confirmationCodeField;
    static string clientId = AWSCognitoIDs.AppClientId;

    public void OnClick()
    {
        var client = new AmazonCognitoIdentityProviderClient(null, RegionEndpoint.APNortheast1);
        ConfirmSignUpRequest confirmSignUpRequest = new ConfirmSignUpRequest();

        confirmSignUpRequest.Username = emailField.text;
        confirmSignUpRequest.ConfirmationCode = confirmationCodeField.text;
        confirmSignUpRequest.ClientId = clientId;

        try
        {
            ConfirmSignUpResponse confirmSignUpResult = client.ConfirmSignUp(confirmSignUpRequest);
            Debug.Log(confirmSignUpResult.ToString());
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
}
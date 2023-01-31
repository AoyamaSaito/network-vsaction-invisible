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

    public InputField emailField;
    public InputField confirmationCodeField;
    static string clientId = AWSCognitoIDs.AppClientId;

    void Start()
    {
    }

    void Update()
    {
    }

    public void OnClick()
    {
        var client = new AmazonCognitoIdentityProviderClient(null, RegionEndpoint.USWest2);
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
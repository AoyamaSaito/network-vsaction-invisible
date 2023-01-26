
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Amazon.CognitoIdentityProvider; // for AmazonCognitoIdentityProviderClient
using Amazon.CognitoIdentityProvider.Model; // for SignUpRequest

public class Signup : MonoBehaviour
{
    public InputField emailField;
    public InputField passwordField;
    static string appClientId = AWSCognitoIDs.AppClientId;

    public void OnClick()
    {
        var client = new AmazonCognitoIdentityProviderClient(null, Amazon.RegionEndpoint.APNortheast1);
        var sr = new SignUpRequest();
        string email = emailField.text;
        string password = passwordField.text;

        sr.ClientId = appClientId;
        sr.Username = email;
        sr.Password = password;
        sr.UserAttributes = new List<AttributeType> {
            new AttributeType {
                Name = "email",
                Value = email
            }
        };

        try
        {
            SignUpResponse result = client.SignUp(sr);
            Debug.Log(result);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Amazon.CognitoIdentityProvider; // for AmazonCognitoIdentityProviderClient
using Amazon.CognitoIdentityProvider.Model; // for SignUpRequest

public class Signup : MonoBehaviour
{
    [SerializeField] InputField emailField;
    [SerializeField] InputField passwordField;

    public void OnClick()
    {
        var client = new AmazonCognitoIdentityProviderClient(null, Amazon.RegionEndpoint.APNortheast1);
        var sr = new SignUpRequest
        {
            ClientId = AWSCognitoIDs.AppClientId,
            Username = emailField.text,
            Password = passwordField.text,
            UserAttributes = new List<AttributeType>
            {
                new AttributeType
                {
                    Name = "email",
                    Value = emailField.text
                }
            }
        };

        try
        {
            var result = client.SignUp(sr);
            Debug.Log(result);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
}
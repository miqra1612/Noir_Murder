using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to controll user sign in process to the game
/// </summary>

public class SignInManager : MonoBehaviour
{

    public InputField signInPassword;
    public InputField teamName;
    public GameObject signInPanel;
    public GameObject failText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function read the password input from the user and allow user to begin the game when the password is correct
    public void SignIn()
    {
        if(signInPassword.text != "")
        {
            GameData.instance.teamName = teamName.text;
            signInPanel.SetActive(false);
        }
        else
        {
            failText.SetActive(true);
            StartCoroutine(LogInFail());
        }
       
    }

    // This coroutine is used to give message for the player when there is problem during log in
    IEnumerator LogInFail()
    {
        yield return new WaitForSeconds(10);
        failText.SetActive(false);
    }
}

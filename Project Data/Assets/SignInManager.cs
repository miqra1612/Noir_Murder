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
    public void DebugSignIn()
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

    public void SignIn()
    {
        if (signInPassword.text != "")
        {
            StartCoroutine(RealLogIn());
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

    IEnumerator RealLogIn()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", signInPassword.text);
        form.AddField("password", signInPassword.text);  
        
        WWW host = new WWW("http://localhost/sqlconnect/login.php", form);
        yield return host;

        Debug.Log(host.text);

        if (host.text.Split()[0] == "0" && host.text.Split()[2] == "0")
        {
            
            GameData.instance.teamName = teamName.text;
            GameData.instance.username = host.text.Split()[1];
            signInPanel.SetActive(false);
            StartCoroutine(UsedPassword());
        }
        else
        {
            failText.SetActive(true);
            StartCoroutine(LogInFail());
        }

    }


    IEnumerator UsedPassword()
    {
        yield return new WaitForEndOfFrame();

        int a = 1;

        WWWForm form = new WWWForm();
        form.AddField("username", GameData.instance.username);
        form.AddField("count", a);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;

        Debug.Log("www:  " + www.text);

        if (www.text.Split()[2] != "0")
        {
            Debug.Log("password used, you cannot use it anymore!");
        }
        else
        {
            Debug.Log("save data error: password used count fail");
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class LogOut : MonoBehaviour
{
    FirebaseAuth auth;
    
    private void Awake(){
        auth = FirebaseAuth.DefaultInstance;
    }
    public void LogOutUser(){
        auth.SignOut();
        SceneManager.LoadScene("LoginSignup");
    }
}

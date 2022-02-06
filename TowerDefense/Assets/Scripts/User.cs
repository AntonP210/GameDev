using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User
{//POJO CLASS 
 //class that will hold the user progress as the user logs in.
    public string userName;
    public int userProgress;
    public string localId;


    public User(string _localId, string _username, int _stage)
    {
        userName = _username;

        userProgress = _stage;

        localId = _localId;
    }
    public string GetUserName()
    {
        return userName;
    }
    public User()
    {

    }
}

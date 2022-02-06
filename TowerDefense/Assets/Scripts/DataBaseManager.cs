using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using FullSerializer;
using System;
using UnityEngine.UI;
using TMPro;


public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;
    private int PlayerProgress;

    //sets the initial value
    public TMP_InputField PlayerEmail, PlayerUserName, PlayerPassword;

    public TextMeshProUGUI loggedUser;
    public GameObject dialogUI;
    User user = new User();
    NewUserRegister newUserRegister = new NewUserRegister();

    public static fsSerializer serializer = new fsSerializer();
    private string idToken;
    private string localId;
    private string getLocalid;

    private string DbUrl = "https://towerdefence-c9b43.firebaseio.com/users";
    private string AuthKey = "AIzaSyBZergr9kItzLK5JuEAkO8DBjPKpAFA3sU";

    //crud functions (FIREBASE REST API)
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SignUpUserButton()//register  button
    {
        SignUpUser(PlayerEmail.text, PlayerUserName.text, PlayerPassword.text);
    }
    public void SignInUserButton()//login button
    {
        SignInUser(PlayerEmail.text, PlayerPassword.text);
    }


    public void GetFromDataBase()
    {
        RetrieveFromDataBase();
    }

    private void UpdateUi(User user)
    {
        loggedUser.text = user.GetUserName();
    }

    private void PostToDataBase(bool newGame = false)
    {
        //creates new User in DB
        user = new User(localId, PlayerUserName.text, PlayerProgress);

        if (newGame)
        {
            user.userProgress = 1;
        }

        RestClient.Put(DbUrl + "/" + localId + ".json", user);

    }
    public void PostProgressToDataBase(int level)
    {
        user.userProgress = level;
        RestClient.Put(DbUrl + "/" + localId + ".json", user);

    }
    private void RetrieveFromDataBase()//gets all the user data from firebase
    {
        GetLocalid();
        //Gets data from the DB by PlayerName
        RestClient.Get<User>(DbUrl + "/" + localId + ".json").Then(response =>
               {
                   user = response;

                   UpdateUi(user);

               });
    }
    private void RetrieveProgress()//gets progress only from the database by the name
    {
        GetLocalid();
        //Gets data from the DB by PlayerName
        RestClient.Get<User>(DbUrl + "/" + localId + ".json").Then(response =>
               {
                   user = response;

                   PlayerProgress = user.userProgress;
               });
    }
    public int GetPlayerProgress()
    {
        return PlayerProgress;
    }
    private void SignUpUser(string _email, string _username, string _password)
    {

        newUserRegister = new NewUserRegister();
        newUserRegister.email = _email;
        newUserRegister.password = _password;
        newUserRegister.returnSecureToken = true;
        string newUserData = JsonUtility.ToJson(newUserRegister);


        RequestHelper currentRequest = new RequestHelper
        {
            Uri = "https://identitytoolkit.googleapis.com/v1/accounts:signUp",
            Params = new Dictionary<string, string> {
                    { "key", AuthKey }
                },
            Body = new NewUserRegister
            {
                email = _email,
                password = _password,
                returnSecureToken = true
            },
            EnableDebug = true
        };

        RestClient.Post<SignResponse>(currentRequest).Then(
            response =>
            {
                Debug.Log("Get the Response");
                idToken = response.idToken;
                localId = response.localId;
                PostToDataBase(true);
                SignInUser(_email, _password);
                dialogUI.SetActive(false);


            }).Catch(error =>
            {
                Debug.Log(error);
            });


    }

    private void SignInUser(string _email, string _password)
    {
        newUserRegister = new NewUserRegister();
        newUserRegister.email = _email;
        newUserRegister.password = _password;
        newUserRegister.returnSecureToken = true;
        string newUserData = JsonUtility.ToJson(newUserRegister);


        RequestHelper currentRequest = new RequestHelper
        {
            Uri = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword",
            Params = new Dictionary<string, string> {
                    { "key", AuthKey }
                },
            Body = new NewUserRegister
            {
                email = _email,
                password = _password,
                returnSecureToken = true
            },
            EnableDebug = true
        };

        RestClient.Post<SignResponse>(currentRequest).Then(
            response =>
            {
                Debug.Log("Get the Response");
                idToken = response.idToken;
                localId = response.localId;
                RetrieveFromDataBase();
                RetrieveProgress();
                dialogUI.SetActive(false);

            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }


    private void GetLocalid()
    {
        RestClient.Get(DbUrl + localId + ".json" + idToken).Then(response =>
        {
            fsData userData = fsJsonParser.Parse(response.Text);

            string username = PlayerUserName.text;

            Dictionary<string, User> users = null;

            serializer.TryDeserialize(userData, ref users);

            foreach (User user in users.Values)
            {
                if (user.userName == username)
                {
                    getLocalid = user.localId;
                    Debug.Log(getLocalid);
                    break;
                }
            }

        });
    }

}


public class NewUserRegister
{
    public string email;
    public string password;
    public bool returnSecureToken;
}


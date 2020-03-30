using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerSelectionManager : MonoBehaviour
{

    public Transform playerSwitcherTransform;

    public GameObject[] spinnerTopModels;



    public int playerSelectionNumber;


    [Header("UI")]
    //public TextMeshProUGUI playerModelType_Text;
    public Button next_Button;
    public Button previous_Button;

    public TextMeshProUGUI playerModelType_Text;
    public GameObject uI_Selection;  //ui 이름 나타나게 하기
    public GameObject uI_AfterSelection;



    #region UNITY Methods

    // Start is called before the first frame update
    void Start()
    {

        uI_Selection.SetActive(true); // BACK 버튼 다망
        uI_AfterSelection.SetActive(true);  // 게임 플레이로 넘어가는 버튼


        playerSelectionNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region UI Callback Methods
    public void NextPlayer()
    {
        playerSelectionNumber += 1;

        if (playerSelectionNumber >= spinnerTopModels.Length)  // 모델 수 이상 넘어가면 0으로 초기화
        {
            playerSelectionNumber = 0;
        }
        Debug.Log(playerSelectionNumber);


        next_Button.enabled = false;
        previous_Button.enabled = false;

        StartCoroutine(Rotate(Vector3.up, playerSwitcherTransform, 180, 1.0f));

        if (playerSelectionNumber == 0 )
        {
            //This means the player model type is ATTACK.
            playerModelType_Text.text = "BaBarian";

        }
        else
        {
            //This means the player model type is DEFEND.
           playerModelType_Text.text = "Robot";

        }




    }

    public void PreviousPlayer()
    {

        playerSelectionNumber -= 1;
        if (playerSelectionNumber < 0)
        {
            playerSelectionNumber = spinnerTopModels.Length - 1;
        }
        Debug.Log(playerSelectionNumber);



        next_Button.enabled = false;
        previous_Button.enabled = false;


        StartCoroutine(Rotate(Vector3.up, playerSwitcherTransform, -180, 1.0f));

        if (playerSelectionNumber == 0)
        {
            //This means the player model type is ATTACK.
            playerModelType_Text.text = "BaBarian";

        }
        else
        {
            //This means the player model type is DEFEND.
            playerModelType_Text.text = "Robot";

        }


    }



    public void OnSelectButtonClicked()
    {

        uI_Selection.SetActive(false);
        uI_AfterSelection.SetActive(true);

        
        ExitGames.Client.Photon.Hashtable playerSelectionProp = new ExitGames.Client.Photon.Hashtable { {Capson.PLAYER_SELECTION_NUMBER, playerSelectionNumber } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerSelectionProp);
    }

    public void OnReSelectButtonClicked()
    {
        uI_Selection.SetActive(true);
        uI_AfterSelection.SetActive(false);
    }
    
    
    
    #endregion
    

    #region Private Methods
    IEnumerator Rotate(Vector3 axis, Transform transformToRotate, float angle, float duration = 1.0f)
    {

        Quaternion originalRotation = transformToRotate.rotation;
        Quaternion finalRotation = transformToRotate.rotation * Quaternion.Euler(axis * angle);

        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            transformToRotate.rotation = Quaternion.Slerp(originalRotation, finalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transformToRotate.rotation = finalRotation;

        next_Button.enabled = true;
        previous_Button.enabled = true;


    }


    #endregion


}

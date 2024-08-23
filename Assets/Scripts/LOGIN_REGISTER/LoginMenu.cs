using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    private AudioSource myAudioSource => GetComponent<AudioSource>();
    [SerializeField] private GameObject loginForm;
    [SerializeField] private GameObject RegisterForm;
    [SerializeField] private Button RegisterButton;
    [SerializeField] private Button backButton;


    [SerializeField]  private TMP_InputField emailText;
    [SerializeField] private TMP_InputField passlText;
    [SerializeField] private TextMeshProUGUI warningText;


    [SerializeField] private TMP_InputField emailRegisterText;
    [SerializeField] private TMP_InputField passRegisterText;
    [SerializeField] private TMP_InputField verifyPassRegisterText;
    [SerializeField] private TextMeshProUGUI warningRegisterText;
    [SerializeField] private TextMeshProUGUI successRegisterText;

    void Start()
    {
        RegisterButton.onClick.AddListener(() =>
        {
            loginForm.SetActive(false);
            RegisterForm.SetActive(true);
            Debug.Log(emailRegisterText.text);
            emailRegisterText.text = "";
            passRegisterText.text = "";
            verifyPassRegisterText.text = "";
            warningRegisterText.SetText("");
            successRegisterText.SetText("");

        });
        backButton.onClick.AddListener(() =>
        {
            RegisterForm.SetActive(false);
            loginForm.SetActive(true);
            emailText.text = "";
            passlText.text = "";
            warningText.SetText("");
        });
    }
}

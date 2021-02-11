using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TicTacToe
{
    public class TTT_Manager : MonoBehaviour
    {
        private static TTT_Manager instance = null;
        

        [SerializeField] private string _sceneMenuName = "TTT_Menu";
        [SerializeField] private string _sceneGameName = "TTT_Game";


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        private void Start()
        {
            SetupButtons();
            var board = FindObjectOfType<TTT_Board>();
            if (board != null)
                board.RestartGame();
        }

        private void SetupButtons()
        {
            foreach (var button in FindObjectsOfType<ButtonReference>())
            {
                switch (button.Type)
                {
                    case ButtonReference.EventType.MENU:
                        button.OnClick += LoadMenu;
                        break;
                    case ButtonReference.EventType.SOLO:
                        button.OnClick += () => LoadGame(false);
                        break;
                    case ButtonReference.EventType.MULTI:
                        button.OnClick += () => LoadGame(true);
                        break;
                }
            }
        }

        private void InitMenu()
        {
            SetupButtons();
        }


        private void InitGame(bool isMultiplayer)
        {
            SetupButtons();
            var board = FindObjectOfType<TTT_Board>();
            board.IsMultiplayer = isMultiplayer;
            board.RestartGame();
        }

        public void LoadMenu()
        {
            SceneManager.sceneLoaded += HandleLoadedMenu;
            SceneManager.LoadScene(_sceneMenuName);
        }

        public void LoadGame(bool isMultiplayer)
        {
            if (isMultiplayer)
                SceneManager.sceneLoaded += HandleLoadedGameMultiplayer;
            else
                SceneManager.sceneLoaded += HandleLoadedGameSolo;
            SceneManager.LoadScene(_sceneGameName);
        }

        public void HandleLoadedGameMultiplayer(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.sceneLoaded -= HandleLoadedGameMultiplayer;
            InitGame(true);
        }

        public void HandleLoadedGameSolo(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.sceneLoaded -= HandleLoadedGameSolo;
            InitGame(false);
        }

        public void HandleLoadedMenu(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.sceneLoaded -= HandleLoadedMenu;
            InitMenu();
        }

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private SettingsPopup settingsPopup; //поле для выбора окна настроек
    private bool IsPaused = false; //переменная для проверки, находится ли игра на паузе

    
    void Start()
    {
        settingsPopup.Close();//закрыть окно с настройками при загрузке
    }

    
    void Update()
    {
        //Обработка нажатия esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //если игра не на паузе, открываем окно с настройками, ставим на паузу
            //иначе, закрываем коно с настройками, убираем паузу
            if (!IsPaused)
            {
                settingsPopup.Open();
                Time.timeScale = 0;
            }
            else
            {
                settingsPopup.Close();
                Time.timeScale = 1f;
            }

            IsPaused = !IsPaused;
        }
    }
}

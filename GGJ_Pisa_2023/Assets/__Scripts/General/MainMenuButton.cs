using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jam.General
{
    public class MainMenuButton : MonoBehaviour
    {

        public void exit()
        {
            Application.Quit();
        }
        public void startGame()
        {
            var temp = Random.Range(0, 4);
            if (temp==0)
            {
             AudioManaegr.Instance.PlayFx("UiClick0");   
            } if (temp==1)
            {
                AudioManaegr.Instance.PlayFx("UiClick1");   

            } if (temp==2)
            {
                AudioManaegr.Instance.PlayFx("UiClick2");   

            } if (temp==3)
            {
                AudioManaegr.Instance.PlayFx("UiClick3");   
   
            }
            SceneManager.LoadScene("_Intern");
        }
    }
}
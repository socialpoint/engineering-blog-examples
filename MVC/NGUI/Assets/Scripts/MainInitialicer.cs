using UnityEngine;

namespace SocialPoint.Examples.MVC
{
    public class MainInitialicer : MonoBehaviour 
    {
        PlayerController Controller;

        void Start()
        {
            Controller = new PlayerController();

            Controller.ShowView();
        }
    }
}

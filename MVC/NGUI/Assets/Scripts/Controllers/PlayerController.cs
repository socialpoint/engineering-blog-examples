using System;
using UnityEngine;

namespace SocialPoint.Examples.MVC
{
    public class PlayerController
    {
        PlayerModel Player {get;set;}

        MainGUIViewPresenter MainGUI {get;set;}

       
        public PlayerController()
        {
            MainGUI = CreateView("MainGUI").GetComponent<MainGUIViewPresenter>();

            MainGUI.AddXpButton.Clicked += (s, e) => Player.AddXp(50);
            MainGUI.AddXpButton.Clicked += (s, e) => Debug.Log("Adding XP points");

            MainGUI.TakeDamageButton.Clicked += (s, e) => Player.TakeDamage(75);
            MainGUI.TakeDamageButton.Clicked += (s, e) => Debug.Log("Player took damage!");

            Player = new PlayerModel();
            Player.XPGained += OnPlayerGainedXP;
            Player.LevelUp += OnPlayerLevelUp;
            Player.LevelUp += (s,e) => Debug.Log("Player leveled up!");
            Player.DamageTaken += OnDamageTaken;
            Player.DamageTaken += (sender, e) => Debug.Log("Damage taken");

            Player.Died += (s, e) => UpdatePlayerDeadUI();
            Player.Died += (s, e) => Debug.Log("Player is dead");

            UpdateLevelAndXPUI();
            UpdateHitPointsUI();

        }


        void UpdateHitPointsUI()
        {
            MainGUI.HitPointsLabel.Text = "Hit Points " + Player.HitPoints;
            if (Player.HasLowHitPoints)
            {
                MainGUI.HitPointsLabel.TextColor = UnityEngine.Color.yellow;
            }
            else
            {
                MainGUI.HitPointsLabel.TextColor = UnityEngine.Color.white;
            }
        }

        void UpdateLevelAndXPUI()
        {
            MainGUI.XpLabel.Text = "Experience: " + Player.XP;
            MainGUI.LevelLabel.Text = "Level " + Player.Level;
        }

        void UpdatePlayerDeadUI()
        {
            MainGUI.HitPointsLabel.Text = "Dead";
            MainGUI.HitPointsLabel.TextColor = UnityEngine.Color.red;
        }

        public void ShowView()
        {
            MainGUI.Show();
        }

        public void HideView()
        {
            MainGUI.Hide();
        }

        public void OnPlayerGainedXP(object sender, EventArgs args)
        {
            UpdateLevelAndXPUI();
        }

        public void OnDamageTaken(object sender, EventArgs args)
        {
            UpdateHitPointsUI();
        }


        public void OnPlayerLevelUp(object sender, EventArgs args)
        {
            UpdateLevelAndXPUI();
            UpdateHitPointsUI();
        } 
        
        GameObject CreateView(string viewName)
        { 
            // Loads the prefab with the view and instantiates it inside the View hierarchy
            return GameObject.Find("MainGUI");
        }
    }

}
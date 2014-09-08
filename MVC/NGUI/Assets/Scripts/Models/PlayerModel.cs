using System;


namespace SocialPoint.Examples.MVC
{
    public class PlayerModel
    {   
        public PlayerModel()
        {
            XP = 0;
            Level = 1;
            HitPoints = MaxHitPoints;
        }

        public event EventHandler XPGained;
        public event EventHandler LevelUp;
        public event EventHandler DamageTaken;
        public event EventHandler Died;

        int _hitPoints;
        public int HitPoints 
        {
            get 
            {
                return _hitPoints;
            }
            set
            {
                var oldValue = _hitPoints;
                _hitPoints = value;

                if (_hitPoints < 0)
                {
                    _hitPoints = 0;
                }

                if (oldValue != _hitPoints)
                {
                    if (DamageTaken != null) DamageTaken(this, EventArgs.Empty);
                    if (IsDead) 
                    {
                        if (Died != null) Died(this, EventArgs.Empty);
                    }
                }
            }
        }

        public int Level { get; private set;}

        public int XP {get; private set;}

        public int MaxHitPoints 
        {
            get { return Level * 150; }
        }
        
        public bool HasLowHitPoints
        {
            get { return HitPoints <= MaxHitPoints * 0.25f; }
        }

        public bool IsDead
        {
            get { return HitPoints <= 0;}
        }

        public void TakeDamage(int hpDamage)
        {
            if (IsDead) return;

            HitPoints -= hpDamage;
        }

        public void AddXp(int amount)
        {
            if (IsDead) return;

            XP += amount; 

            OnPlayerXPGained();

            if (XP >= (100 * Level))
            {
                OnPlayerLevelUp();
            }
        }

        protected virtual void OnPlayerLevelUp()
        {
            Level++;
            HitPoints = MaxHitPoints;

            if (LevelUp != null) LevelUp(this, EventArgs.Empty);
        }

        protected virtual void OnPlayerXPGained()
        {
            if (XPGained != null) XPGained(this, EventArgs.Empty);
        }
        
    }
}

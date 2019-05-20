using System;
using UnityEngine;
using System.Collections.Generic;


namespace StrategyPacMan.weapons
{
    public class foodWeapon : Food,  IWeapon
    {

        private int damage;
        private string verb, name;


        
        
        public foodWeapon()
        {
            this.damage = 1;
            this.verb = "chomp";
            this.name = "pacman food weapon";
                
        }

        public int Damage
        {
            get
            {
                return this.damage;
            }
            set
            {
                this.damage = value;
            }
        }

        public string Verb
        {
            get
            {
                return this.verb;
            }
            set
            {
                this.verb = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Use()
        {
            throw new NotImplementedException();
        }
    }
}

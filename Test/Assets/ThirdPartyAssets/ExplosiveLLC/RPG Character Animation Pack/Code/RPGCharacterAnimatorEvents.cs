using System.Collections;
using UnityEngine;

namespace RPGCharacterAnims
{
    //Placeholder functions for Animation events.
    public class RPGCharacterAnimatorEvents:MonoBehaviour
    {
		[HideInInspector] public RPGCharacterController rpgCharacterController;
        [HideInInspector] public Moviment characterMovimentBehaviour;

        public void Hit()
        {
            if (characterMovimentBehaviour != null)
            {
                characterMovimentBehaviour.Hit();
            }
        }

        public void Shoot()
        {
        }

        public void FootR()
        {
        }

        public void FootL()
        {
        }

        public void Land()
        {
        }

		public void WeaponSwitch()
		{
			if(rpgCharacterController.rpgCharacterWeaponController != null)
			{
				rpgCharacterController.rpgCharacterWeaponController.WeaponSwitch();
			}
		}
	}
}
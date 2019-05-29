﻿using System.Collections.Generic;
using UnityEngine;

namespace MyPhotonProject.Scripts
{
    public class OfflineIntentReceiver : AIntentReceiver
    {
        [SerializeField]
        public int avatarIndex;

        private enum PlayerAction
        {
            Left,
            Down,
            Right, 
            Up,
            Bump,
            Shoot,
            //Jump,
        }

        private static readonly Dictionary<int, Dictionary<PlayerAction, KeyCode>> keys =
            new Dictionary<int, Dictionary<PlayerAction, KeyCode>>
            {
                {
                    0, new Dictionary<PlayerAction, KeyCode>
                    {
                        {PlayerAction.Left, KeyCode.Q},
                        {PlayerAction.Down, KeyCode.S},
                        {PlayerAction.Right, KeyCode.D},
                        {PlayerAction.Up, KeyCode.Z},
                        {PlayerAction.Bump, KeyCode.M},
                        {PlayerAction.Shoot, KeyCode.K}
                        //{PlayerAction.Jump, KeyCode.Space}
                    }
                },
                {
                    1, new Dictionary<PlayerAction, KeyCode>
                    {
                        {PlayerAction.Left, KeyCode.LeftArrow},
                        {PlayerAction.Down, KeyCode.DownArrow},
                        {PlayerAction.Right, KeyCode.RightArrow},
                        {PlayerAction.Up, KeyCode.UpArrow},
                        {PlayerAction.Bump, KeyCode.RightControl},
                        {PlayerAction.Shoot, KeyCode.P}
                        //{PlayerAction.Jump, KeyCode.O}
                    }
                },
                {
                    2, new Dictionary<PlayerAction, KeyCode>
                    {
                        {PlayerAction.Left, KeyCode.J},
                        {PlayerAction.Down, KeyCode.K},
                        {PlayerAction.Right, KeyCode.L},
                        {PlayerAction.Up, KeyCode.I},
                        {PlayerAction.Bump, KeyCode.H},
                        {PlayerAction.Shoot, KeyCode.N}
                        //{PlayerAction.Jump, KeyCode.U}    
                    }
                },
                {
                    3, new Dictionary<PlayerAction, KeyCode>
                    {
                        {PlayerAction.Left, KeyCode.Keypad4},
                        {PlayerAction.Down, KeyCode.Keypad5},
                        {PlayerAction.Right, KeyCode.Keypad6},
                        {PlayerAction.Up, KeyCode.Keypad8},
                        {PlayerAction.Bump, KeyCode.KeypadEnter},
                        {PlayerAction.Shoot, KeyCode.B}
                        //{PlayerAction.Jump, KeyCode.V}
                    }
                }
            };


        public void Update()
        {
            if (Input.GetKeyDown(keys[avatarIndex][PlayerAction.Left]))
            {
                WantToMoveLeft = true;
            }

            if (Input.GetKeyDown(keys[avatarIndex][PlayerAction.Down]))
            {
                WantToMoveBack = true;
            }

            if (Input.GetKeyDown(keys[avatarIndex][PlayerAction.Right]))
            {
                WantToMoveRight = true;
            }

            if (Input.GetKeyDown(keys[avatarIndex][PlayerAction.Up]))
            {
                WantToMoveForward = true;
            }

            if (Input.GetKeyUp(keys[avatarIndex][PlayerAction.Left]))
            {
                WantToMoveLeft = false;
            }

            if (Input.GetKeyUp(keys[avatarIndex][PlayerAction.Down]))
            {
                WantToMoveBack = false;
            }

            if (Input.GetKeyUp(keys[avatarIndex][PlayerAction.Right]))
            {
                WantToMoveRight = false;
            }

            if (Input.GetKeyUp(keys[avatarIndex][PlayerAction.Up]))
            {
                WantToMoveForward = false;
            }

            if (Input.GetKeyDown(keys[avatarIndex][PlayerAction.Bump]))
            {
                WantToBumpIntent = true;
            }

            if (Input.GetKeyDown(keys[avatarIndex][PlayerAction.Shoot]))
            {
                WantToShoot = true;
            }

            if (Input.GetKeyUp(keys[avatarIndex][PlayerAction.Shoot]))
            {
                WantToShoot = false;
            }



            /*
            if (Input.GetKeyDown(keys[avatarIndex][PlayerAction.Jump]))
            {
                WantToJump = true;
            }

            if (Input.GetKeyUp(keys[avatarIndex][PlayerAction.Jump]))
            {
                WantToJump = false;
            }*/

        }
    }
}
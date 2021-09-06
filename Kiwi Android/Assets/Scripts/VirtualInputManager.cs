using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player_controller
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
    }
}

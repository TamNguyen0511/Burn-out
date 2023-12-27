using System;

namespace _Game.Scripts.Story_and_Map
{
    public class SameWorldPortal : Portal
    {
        private void Start()
        {
            _portalType = PortalType.MoveToSameWorld;
        }
    }
}
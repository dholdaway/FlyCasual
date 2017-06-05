﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CriticalHitCard
{

    public class LooseStabilizer : GenericCriticalHit
    {
        public LooseStabilizer()
        {
            Name = "Loose Stabilizer";
            Type = CriticalCardType.Ship;
            ImageUrl = "http://vignette4.wikia.nocookie.net/xwing-miniatures/images/d/db/Loose-stabilizer.png";
        }

        public override void ApplyEffect(Ship.GenericShip host)
        {
            Game.UI.ShowInfo("After you execute a white maneuver, receive 1 stress token");
            Game.UI.AddTestLogEntry("After you execute a white maneuver, receive 1 stress token");
            host.AssignToken(new Tokens.LooseStabilizerCritToken());

            host.OnMovementFinish += StressAfterWhiteManeuvers;
            host.AfterAvailableActionListIsBuilt += AddCancelCritAction;
        }

        public override void DiscardEffect(Ship.GenericShip host)
        {
            Game.UI.ShowInfo("No stress after white maneuvers");
            Game.UI.AddTestLogEntry("No stress after white maneuvers");
            host.RemoveToken(typeof(Tokens.LooseStabilizerCritToken));

            host.OnMovementFinish -= StressAfterWhiteManeuvers;
            host.AfterAvailableActionListIsBuilt -= AddCancelCritAction;
        }

        private void StressAfterWhiteManeuvers(Ship.GenericShip ship)
        {
            if (ship.GetLastManeuverColor() == Ship.ManeuverColor.White)
            {
                Game.UI.ShowError("Loose Stabilizer: Stress token is assigned");
                Game.UI.AddTestLogEntry("Loose Stabilizer: Stress token is assigned");
                ship.AssignToken(new Tokens.StressToken());
            }
        }

    }

}
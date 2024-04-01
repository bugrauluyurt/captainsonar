using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptainSonar.Common.Domain.Game
{
    public enum TeamName { Team1, Team2, System }

    public static class TeamNameExtensions
    {
        public static TeamName GetEnemyTeamName(this TeamName self)
        {
            if (self == TeamName.Team1)
            {
                return TeamName.Team2;
            }
            else if (self == TeamName.Team2)
            {
                return TeamName.Team1;
            }
            else
            {
                throw new InvalidOperationException("System team does not have an enemy team.");
            }
        }

    }
}

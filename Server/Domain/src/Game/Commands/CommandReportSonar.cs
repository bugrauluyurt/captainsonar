using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Assets;
using CaptainSonar.Map;

namespace CaptainSonar.Game.Commands
{
    internal class CommandReportSonarData
    {
        public string TruthyData { get; } = "";
        public string FalsyData { get; } = "";
    }

    internal class CommandReportSonar(CommandReportSonarData data) : Command<CommandReportSonarData>(data, CommandName.ReportSonar)
    {
    }
}

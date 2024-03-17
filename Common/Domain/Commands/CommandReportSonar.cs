using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Assets;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandReportSonarData
    {
        public string TruthyData { get; } = "";
        public string FalsyData { get; } = "";
    }

    public class CommandReportSonar(CommandReportSonarData data) : Command<CommandReportSonarData>(data, CommandName.ReportSonar)
    {
    }
}

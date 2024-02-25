using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Assets;
using CaptainSonar.Map;

namespace CaptainSonar.Game.Commands
{
    internal class CommandReportSectionData
    {
        public GridSection GridSection { get; }
    }

    internal class CommandReportSection(CommandReportSectionData data) : Command<CommandReportSectionData>(data, CommandName.ReportSection)
    {
    }
}

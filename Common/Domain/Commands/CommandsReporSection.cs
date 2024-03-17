using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Assets;
using CaptainSonar.Common.Domain.Map;

namespace CaptainSonar.Common.Domain.Commands
{
    public class CommandReportSectionData
    {
        public GridSection GridSection { get; }
    }

    public class CommandReportSection(CommandReportSectionData data) : Command<CommandReportSectionData>(data, CommandName.ReportSection)
    {
    }
}

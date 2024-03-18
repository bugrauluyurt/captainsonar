using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptainSonar.Common.Domain.Commands;

namespace Common.Domain.Commands
{
    public interface ICommandBase
    {
        public CommandName Name { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExecute.Engine.Repositories.Abstractions
{
    public interface IRepositoryBuilderStrategy
    {
        IRepositoryAsync Build(string connectionString);
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulasWebApi.Infra.Db
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}

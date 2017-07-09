using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net.Appender;
using System.Text.RegularExpressions;

namespace Manulife.TopFiveWebsites.Entity
{
    public class EntityFrameworkAppender : AdoNetAppender
    {
        protected override IDbConnection CreateConnection(Type connectionType, string connectionString)
        {
            var factory = (IDbConnectionFactory)Activator.CreateInstance(connectionType);

            //parse EF connection content
            var m = Regex.Match(connectionString, "connection string=\"(?<sqlConnString>.*?)\"");
            if (!m.Success)
                throw new InvalidOperationException($"Cannot extract sql connection string from EF connection content [{connectionString}]");

            //create connection object from EF connection string
            var sqlConnString = m.Groups["sqlConnString"].Value;
            IDbConnection instance = factory.CreateConnection(sqlConnString);
            instance.ConnectionString = sqlConnString;

            return instance;
        }
    }
}

using Npgsql;
using System.Collections.Generic;

namespace web.Logger
{
    public class RepositorioLogger
    {
        private string ConnectionString { get; set; }

        public RepositorioLogger(string connection)
        {
            ConnectionString = connection;
        }

        private static bool ExecuteNonQuery(string commandStr, List<NpgsqlParameter> paramList)
        {
            return false;
        }

#pragma warning disable CC0091 // Use static method
        public bool InsertLog(LogEvento log)
#pragma warning restore CC0091 // Use static method
        {
            var command = $@"INSERT INTO event_log (id_evento, log_level, mensagem, data_criacao) VALUES (@id_evento, @log_level, @mensagem, @data_criacao)";
            var paramList = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("id_evento", log.EventId),
                new NpgsqlParameter("log_level", log.LogLevel),
                new NpgsqlParameter("mensagem", log.Message),
                new NpgsqlParameter("data_criacao", log.CreatedTime)
            };

            return ExecuteNonQuery(command, paramList);
        }
    }
}

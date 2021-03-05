using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace core.config
{
    public class Response
    {
        public object Result { get; internal set; }

        private readonly IList<string> messages = new List<string>();

        public bool IsOk { get; internal set; }

        public string Mensagem { get; internal set; }

        public Exception Exception { get; internal set; }

        public IEnumerable<string> Validates { get; }

        public Response()
        {
            IsOk = true;
            Exception = null;
            Mensagem = "Operação concluída com sucesso";
            Validates = new ReadOnlyCollection<string>(messages);
        }

        public Response(object entidade) : this()
        {
            Result = entidade;
        }

        public Response(object entidade, string mensagem) : this(mensagem)
        {
            Result = entidade;
        }

        public Response(string mensagem)
            : this()
        {
            Mensagem = mensagem;
        }

        public Response(bool isOk, string mensagem)
            : this(mensagem)
        {
            IsOk = isOk;
        }

        public Response(Exception erro)
        {
            IsOk = false;
            Exception = erro;
        }

        public Response AddError(string message)
        {
            IsOk = false;
            Mensagem = "Parametro inválido";
            messages.Add(message);
            return this;
        }
    }
}

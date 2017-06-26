using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Exceptions
{
    public class PrimaryKeyViolationException : Exception
    {
        public PrimaryKeyViolationException(string message) : base(message)
        {

        }
    }
    public class ForignKeyViolationException : Exception
    {
        public ForignKeyViolationException(string message) : base(message)
        {

        }
    }
    public class InvaliedUserInputsException : Exception
    {
        public InvaliedUserInputsException(string message) : base(message)
        {

        }
    }
    public class InvaliedTokenException : Exception
    {
        public InvaliedTokenException(string message = "invalied token") : base(message)
        {

        }
    }

    public class ObjectNotFoundException : Exception {
        public ObjectNotFoundException(string message = "element not found") : base(message)
        {

        }
    }

    public class EmailSendFailException : Exception {
        public EmailSendFailException(string message = "Email send fail.please try again") : base(message)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TouristClub.Logic
{
    public class IncorrectDataException : Exception
    {
        public IncorrectDataException()
		{
		}

		public IncorrectDataException(string message) : base(message)
		{
		}

		public IncorrectDataException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected IncorrectDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
    }
}
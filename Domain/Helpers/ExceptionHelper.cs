using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public static class ExceptionHelper
    {
        public static string GetAllInnerException(Exception ex)
        {
            string message = string.Empty;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(ex.Message);
            // message += ex.Message + "\n";
            if (ex.InnerException != null)
                sb.Append(GetAllInnerException(ex.InnerException));

            return sb.ToString();
        }
    }
}

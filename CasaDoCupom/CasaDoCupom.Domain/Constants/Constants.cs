using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CasaDoCupom.Domain.Constants
{
    public class Constants
    {
        public const string VERSAO_API = "/api/v1";

        public const string DOMAIN = "casadocupom";

        public const string DEFAULT_PORT = "5000";

        public const string URL_PROTOCOL = "https://";

        public const string URL_BASE = URL_PROTOCOL + DOMAIN + DEFAULT_PORT + VERSAO_API;

    }
}

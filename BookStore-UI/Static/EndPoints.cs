using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_UI.Static
{
    public static class EndPoints
    {
#if DEBUG
        //public static string BaseUrl = "https://localhost:44395/";
        public static string BaseUrl = "http://localhost:90/";
#else
        public static string BaseUrl = "http://localhost:90/";

#endif

        public static string AuthorsEndPoint = $"{BaseUrl}api/authors/";
        public static string BooksEndPoint = $"{BaseUrl}api/Books/";
        public static string RegistersEndPoint = $"{BaseUrl}api/Users/register/";
        public static string LoginEndPoint = $"{BaseUrl}api/Users/login";
    }
}

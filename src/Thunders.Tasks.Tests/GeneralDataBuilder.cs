using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunders.Tasks.Tests
{
    public static class GeneralDataBuilder
    {
        public static int ID_VALID = 1;
        public static int ID_EMPTY = 0;
        public static int ID_INVALID = -1;
        public static string TEXT_VALID = "Text Valid";
        public static string TEXT_EMPTY = "";
        public static DateTime DATETIME_VALID = DateTime.Now.Date;
        public static DateTime DATETIME_INVALID = DateTime.MinValue;
        public static DateTime? DATETIME_EMPTY = null;
    }
}

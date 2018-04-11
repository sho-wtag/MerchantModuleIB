
namespace bits.SqlClient
{
    //public class StaticSettings
    //{
    //    public class Database
    //    {
    //        #region Server Connection
    //        ////public static string WUDbConnectionString = "User ID=sa; Password=sa; Initial Catalog=remittance; Data Source=10.100.13.115;";

    //        ////public static string DbConnectionString = "User ID=sa; Password=sa; Initial Catalog=ELDORADOdd; Data Source=10.100.13.115;";
    //        ////public static string DbConnectionString = @"User ID=sa; Password=123456; Initial Catalog=ELDORADO; Data Source=BSD-002\SQLEXPRESS;";
    //        ////public static string DbConnectionString = "User ID=sa; Password=bbladmin; Initial Catalog=ELDORADO; Data Source=10.20.0.42;";
    //        ////public static string DbConnectionString = "User ID=sa; Password=sa; Initial Catalog=eldorado; Data Source=10.100.15.51;";
    //        ////public static string DbConnectionString = "User ID=connection; Password=connection; Initial Catalog=eldorado; Data Source=10.100.15.55;";			
    //        ////public static string DbConnectionString = "User ID = sa; Password = sa; Initial Catalog = ELD_CRD; Data Source = 10.100.13.115;";
    //        ////public static string DbConnectionString = "User ID=sa; Password=sa; Initial Catalog=ELDORADO; Data Source=10.100.13.116;";
    //        //public static string DbConnectionString = "User ID=sa; Password=bsdsa2005; Initial Catalog=eldorado; Data Source=10.20.6.58; Connect Timeout=600";
    //        //#endregion

    //        //#region Live Server Connection
    //        ////************************************************************************************************
    //        ////public static string DbConnectionString = "User ID=sa; Password=bbladmin; Initial Catalog=ELDORADO; Data Source=10.20.0.42; Connect Timeout=600";
    //        ////public static string WUDbConnectionString = "User ID=sa; Password=sa; Initial Catalog=remittance; Data Source=10.20.4.91;";
    //        ////************************************************************************************************
    //        #endregion

    //        #region Test Server Connection
    //        //************************************************************************************************
    //        //*****public static string DbConnectionString = "User ID=sa; Password=bsdsa2005; Initial Catalog=eldorado; Data Source=10.20.6.58;";
    //        //public static string DbConnectionString = System.Configuration.ConfigurationManager.AppSettings["DbConnectionString"];
    //        //************************************************************************************************
    //        #endregion	

    //        public static string DbConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["eldorado2005ConnectionString"].ConnectionString;

    //        public static string XMLDownloadConnectionString = System.Configuration.ConfigurationManager.AppSettings["XMLDownloadConnectionString"];
    //        public static string XMLUploadConnectionString = System.Configuration.ConfigurationManager.AppSettings["XMLUploadConnectionString"];
            
    //        #region Live Server Connection
    //        //************************************************************************************************
    //        //*******public static string DbConnectionString = "User ID=sa; Password=bbladmin; Initial Catalog=ELDORADO; Data Source=10.20.0.42; Connect Timeout=600";
    //        //public static string DbConnectionString = System.Configuration.ConfigurationManager.AppSettings["DbConnectionString"];
    //        //************************************************************************************************
    //        #endregion
			
    //        #region Common Connection
    //        //************************************************************************************************
    //        public static string WUDbConnectionString = "User ID=sa; Password=sa; Initial Catalog=remittance; Data Source=10.20.4.91;";
    //        //************************************************************************************************
    //        #endregion

    //        public static void ChangeConnection(bool IsLive)
    //        {
    //            //ConfigDecrypter cd = new ConfigDecrypter(); //Added By Anjan
    //            //cd.DecryptAppSettings();

    //            if (IsLive)
    //                DbConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["eldorado2005ConnectionString"].ConnectionString;
    //            else
    //                DbConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["eldorado2005ConnectionString_Stage"].ConnectionString;

    //            //ConfigEncrypter ce = new ConfigEncrypter(); //Added By Anjan
    //            //ce.EncryptAppSettings();

    //        }

    //    }
       
    //    public class DateSelector
    //    {
    //        public static ArrayList GetMonths()
    //        {
    //            ArrayList arlMonths = new ArrayList();
    //            arlMonths.Add("January");
    //            arlMonths.Add("February");
    //            arlMonths.Add("March");
    //            arlMonths.Add("April");
    //            arlMonths.Add("May");
    //            arlMonths.Add("June");
    //            arlMonths.Add("July");
    //            arlMonths.Add("August");
    //            arlMonths.Add("September");
    //            arlMonths.Add("October");
    //            arlMonths.Add("November");
    //            arlMonths.Add("December");

    //            return arlMonths;
    //        }
    //        //author:shanchoy---Dec05,2006
    //        public static ArrayList GetDayofWeek()
    //        {
    //            ArrayList arlDayofWeek = new ArrayList();
    //            arlDayofWeek.Add("Saturday");
    //            arlDayofWeek.Add("Sunday");
    //            arlDayofWeek.Add("Monday");
    //            arlDayofWeek.Add("Tuesday");
    //            arlDayofWeek.Add("Wednesday");
    //            arlDayofWeek.Add("Thursday");
    //            arlDayofWeek.Add("Friday");
    //            return arlDayofWeek;
    //        }

    //        public static ArrayList GetBackYears(int BackYear)
    //        {
    //            int i;
    //            int CurrentYear = DateTime.Now.Year;
    //            ArrayList arlYears = new ArrayList();

    //            for (i = CurrentYear; i > CurrentYear - BackYear; i--)
    //            {
    //                arlYears.Add(i);
    //            }

    //            return arlYears;
    //        }
    //        public static ArrayList GetDays()
    //        {
    //            int i;
    //            ArrayList arlYears = new ArrayList();

    //            for (i = 1; i <= 9; i++)
    //            {
    //                arlYears.Add("0" + i.ToString());
    //            }
    //            for (i = 10; i <= 31; i++)
    //            {
    //                arlYears.Add(i);
    //            }

    //            return arlYears;
    //        }
    //        //By Mahatab....
    //        /// <summary>
    //        /// Returns minutes array list.
    //        /// </summary>
    //        /// <returns></returns>
    //        public static ArrayList GetMinutes()
    //        {
    //            int i;
    //            ArrayList arlMinutes = new ArrayList();

    //            for (i = 0; i <= 9; i++)
    //            {
    //                arlMinutes.Add("0" + i.ToString());
    //            }
    //            for (i = 10; i <= 59; i++)
    //            {
    //                arlMinutes.Add(i);
    //            }

    //            return arlMinutes;
    //        }
    //        //By Mahatab.....
    //        /// <summary>
    //        /// Returns short month name( 3 characters). 
    //        /// </summary>
    //        /// <returns></returns>
    //        public static ArrayList GetMonthsShort()
    //        {
    //            ArrayList arlMonths = new ArrayList();
    //            arlMonths.Add("Jan");
    //            arlMonths.Add("Feb");
    //            arlMonths.Add("Mar");
    //            arlMonths.Add("Apr");
    //            arlMonths.Add("May");
    //            arlMonths.Add("Jun");
    //            arlMonths.Add("Jul");
    //            arlMonths.Add("Aug");
    //            arlMonths.Add("Sep");
    //            arlMonths.Add("Oct");
    //            arlMonths.Add("Nov");
    //            arlMonths.Add("Dec");

    //            return arlMonths;
    //        }
    //        //By Mahatab.
    //        /// <summary>
    //        /// Returns hour (1-12) array list.
    //        /// </summary>
    //        /// <returns></returns>
    //        public static ArrayList GetHours()
    //        {
    //            int i;
    //            ArrayList arlHours = new ArrayList();

    //            for (i = 1; i <= 9; i++)
    //            {
    //                arlHours.Add("0" + i.ToString());
    //            }
    //            for (i = 10; i <= 12; i++)
    //            {
    //                arlHours.Add(i);
    //            }

    //            return arlHours;
    //        }
    //        //By Mahatab.
    //        /// <summary>
    //        /// Returns an array of number of days in a month for a year....
    //        /// </summary>
    //        /// <param name="intMonth"></param>
    //        /// <param name="intYear"></param>
    //        /// <returns></returns>
    //        public static ArrayList GetNofDaysInMonth(int intMonth, int intYear)
    //        {
    //            int i;
    //            ArrayList arlDays = new ArrayList();

    //            for (i = 1; i <= 9; i++)
    //            {
    //                arlDays.Add("0" + i.ToString());
    //            }
    //            for (i = 10; i <= DateTime.DaysInMonth(intYear, intMonth); i++)
    //            {
    //                arlDays.Add(i);
    //            }

    //            return arlDays;
    //        }


    //    }
    //}
}

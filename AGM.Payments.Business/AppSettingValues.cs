
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AGM.Payments.Business
{
    /// <summary>
    /// Class for Global application setting and methods
    /// </summary>
    public static class AppSettingValues
    {
        public static Int16 YearsToAddInExpiry
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["YearsToAddInExpiry"]);
            }
        }
        /// <summary>
        /// Return the months from Jan-Dec
        /// </summary>
        public static Dictionary<string, string> Months()
        {
            string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            int start = 1;
            Dictionary<string, string> months = new Dictionary<string, string>();
            foreach (var month in monthNames)
            {
                if (!string.IsNullOrEmpty(month))
                {
                   
                    months.Add(start.ToString(), month.ToString());
                    start++;
                }
            }
            return months;
        }

        /// <summary>
        /// Return list of year from current year to next 50 year.
        /// </summary>
        public static List<string> Years()
        {
            List<string> years = new List<string>();
            int currentYear = DateTime.Now.Year;
            int tillYear = currentYear + YearsToAddInExpiry;
            for (int year = currentYear; year < tillYear; year++)
            {
                years.Add(year.ToString());
            }
            return years;
        }
        /// <summary>
        /// Used to Copy value of each property value to specified type 
        /// </summary>
        /// <typeparam name="TConvert">Type in which you want to copy</typeparam>
        /// <param name="entity">Source type from which you values will be copied</param>
        public static TConvert ConvertTo<TConvert>(this object entity) where TConvert : new()
        {
            var convertProperties = TypeDescriptor.GetProperties(typeof(TConvert)).Cast<PropertyDescriptor>();
            var entityProperties = TypeDescriptor.GetProperties(entity).Cast<PropertyDescriptor>();

            var convert = new TConvert();

            foreach (var entityProperty in entityProperties)
            {
                var property = entityProperty;
                var convertProperty = convertProperties.FirstOrDefault(prop => prop.Name == property.Name);
                if (convertProperty != null)
                {
                    try
                    {
                        convertProperty.SetValue(convert, Convert.ChangeType(entityProperty.GetValue(entity), convertProperty.PropertyType));
                    }
                    catch { }
                }
            }

            return convert;
        }
        /// <summary>
        /// Used to Get IntegrationPartnerID
        /// </summary>
        public static string IntegrationPartnerID
        {
            get
            {
                return ConfigurationManager.AppSettings["IntegrationPartnerID"];
            }
        }
        /// <summary>
        /// Used to Get ApiKey
        /// </summary>
        public static string ApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiKey"];
            }
        }
        /// <summary>
        /// Used to Get AccountID
        /// </summary>
        public static string AccountID
        {
            get
            {
                return ConfigurationManager.AppSettings["AccountID"];
            }
        }
        /// <summary>
        /// Used to Get Properties Url
        /// </summary>
        public static string Properties
        {
            get
            {
                return ConfigurationManager.AppSettings["Properties"];
            }
        }
        /// <summary>
        /// Used to Get Accounts Url
        /// </summary>
        public static string Accounts
        {
            get
            {
                return ConfigurationManager.AppSettings["Accounts"];
            }
        }
        /// <summary>
        /// Used to Get CurrentResident Url
        /// </summary>
        public static string CurrentResident
        {
            get
            {
                return ConfigurationManager.AppSettings["CurrentResident"];
            }
        }
    }
}
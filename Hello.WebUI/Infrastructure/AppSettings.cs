using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Hello.WebUI.Infrastructure
{
    public class AppSettings
    {

        public static string HashKey
        {
            get { return "05c7b93becc6584935a8433aaa979934"; }
        }

        public static int ItemPerPageMobile
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ItemPerPageMobile"]); }
        }

        public static DateTime DefaultDate
        {
            get { return DateTime.Parse("1900-01-01"); }
        }

        public static string DefaultLocation
        {
            get { return ConfigurationManager.AppSettings["DefaultLocation"]; }
        }

        public static string CurrentHost
        {
            get { return ConfigurationManager.AppSettings["CurrentHost"]; }
        }

        public static string HostIndex1
        {
            get { return ConfigurationManager.AppSettings["HostIndex1"]; }
        }

        public static string HostIndex2
        {
            get { return ConfigurationManager.AppSettings["HostIndex2"]; }
        }

        public static string CountryThumb
        {
            get { return ConfigurationManager.AppSettings["CountryThumb"]; }
        }

        public static string MarkerThumb
        {
            get { return ConfigurationManager.AppSettings["MarkerThumb"]; }
        }

        public static string ServiceThumb
        {
            get { return ConfigurationManager.AppSettings["ServiceThumb"]; }
        }

        public static string FeatureThumb
        {
            get { return ConfigurationManager.AppSettings["FeatureThumb"]; }
        }

        public static string FurnitureThumb
        {
            get { return ConfigurationManager.AppSettings["FurnitureThumb"]; }
        }

        public static string NotifyThumb
        {
            get { return ConfigurationManager.AppSettings["NotifyThumb"]; }
        }

        public static string AdvPath
        {
            get { return ConfigurationManager.AppSettings["AdvPath"]; }
        }

        public static string AvatarPath
        {
            get { return ConfigurationManager.AppSettings["AvatarPath"]; }
        }

        public static string ProductPath
        {
            get { return ConfigurationManager.AppSettings["ProductPath"]; }
        }

        public static string VolumeThumb
        {
            get { return ConfigurationManager.AppSettings["VolumeThumb"]; }
        }

        public static string VolumeCover
        {
            get { return ConfigurationManager.AppSettings["VolumeCover"]; }
        }

        public static string VolumeCoverSquare
        {
            get { return ConfigurationManager.AppSettings["VolumeCoverSquare"]; }
        }

        public static string ChapThumb
        {
            get { return ConfigurationManager.AppSettings["ChapThumb"]; }
        }

        public static string ChapImage
        {
            get { return ConfigurationManager.AppSettings["ChapImage"]; }
        }
    }
}
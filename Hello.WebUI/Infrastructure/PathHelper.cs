using Hello.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello.WebUI.Infrastructure
{
    public class PathHelper
    {
        public static string ImageHostIndex(HostIndex index)
        {
            switch (index)
            {
                case HostIndex.HostIndex1:
                    return AppSettings.HostIndex1;
                case HostIndex.HostIndex2:
                    return AppSettings.HostIndex2;
                default:
                    return AppSettings.CurrentHost;
            }
        }

        public static string Thumbnail(string thumb, ThumbType thumbType)
        {
            if (String.IsNullOrEmpty(thumb))
                return "";

            switch (thumbType)
            {
                case ThumbType.Country:
                    return AppSettings.CurrentHost + AppSettings.CountryThumb + thumb;
                case ThumbType.Marker:
                    return AppSettings.CurrentHost + AppSettings.MarkerThumb + thumb;
                case ThumbType.Feature:
                    return AppSettings.CurrentHost + AppSettings.FeatureThumb + thumb;
                case ThumbType.Furniture:
                    return AppSettings.CurrentHost + AppSettings.FurnitureThumb + thumb;
                case ThumbType.Notify:
                    return AppSettings.CurrentHost + AppSettings.NotifyThumb + thumb;
                default:
                    return "";
            }
        }

        public static string Adv(string image)
        {
            if (String.IsNullOrEmpty(image))
                return "";

            return AppSettings.CurrentHost + AppSettings.AdvPath + image;
        }

        public static string Avatar(string avatar, HostIndex index)
        {
            if (String.IsNullOrEmpty(avatar))
                return ImageHostIndex(HostIndex.CurrentHost) + AppSettings.AvatarPath + "no_avatar.png";

            return ImageHostIndex(index) + AppSettings.AvatarPath + avatar;
        }

        public static string ProductImage(string image, HostIndex index)
        {
            if (String.IsNullOrEmpty(image))
                return "";

            return ImageHostIndex(index) + AppSettings.ProductPath + image;
        }

        #region Hello Toon
        public static string ToonVolumeThumb(string thumb)
        {
            if (String.IsNullOrEmpty(thumb))
                return "";

            return AppSettings.CurrentHost + AppSettings.VolumeThumb + thumb;
        }

        public static string ToonVolumeCover(string cover, DeviceType type)
        {
            if (String.IsNullOrEmpty(cover))
                return "";

            if (type == DeviceType.Android || type == DeviceType.IOS)
                return AppSettings.CurrentHost + AppSettings.VolumeCoverSquare + cover;
            else
                return AppSettings.CurrentHost + AppSettings.VolumeCover + cover;

        }

        public static string ToonChapThumb(string thumb)
        {
            if (String.IsNullOrEmpty(thumb))
                return "";

            return AppSettings.CurrentHost + AppSettings.ChapThumb + thumb;
        }

        public static string ToonChapImage(string image)
        {
            if (String.IsNullOrEmpty(image))
                return "";

            return AppSettings.CurrentHost + AppSettings.ChapImage + image;
        }
        #endregion
    }
}
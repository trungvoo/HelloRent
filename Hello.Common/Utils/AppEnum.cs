using System;

namespace Hello.Common.Utils
{
    public enum LanguageType : byte
    {
        Vietnamese = 0,
        English = 1,
        Korean = 2,

        Undefined = 254,
    }

    public enum DeviceType : byte
    {
        PC = 0,
        Android = 1,
        IOS = 2,

        Undefined = 254
    }

    public enum HostIndex : byte
    {
        CurrentHost = 0,
        HostIndex1 = 1,
        HostIndex2 = 2,

        Undefined = 254
    }

    public enum Gender : byte
    {
        Female = 0,
        Male = 1,
        Other = 2,

        Undefined = 254
    }

    public enum Status : byte
    {
        New = 0,
        Activated = 1,
        Disabled = 250,

        Undefined = 254
    }

    public enum ThumbType : byte
    {
        Country = 0,
        Marker = 1,
        Feature = 3,
        Furniture = 4,
        Notify = 5,
    }

    public enum AccountRole : byte
    {
        Member = 0,
        Owner = 1,
        Broker = 2,

        Employee = 180,
        Support = 181,
        Manager = 182,
        Admin = 200,

        Undefined = 254
    }

    public enum AccountType : byte
    {
        HelloRent = 0,
        Facebook = 1,
        Google = 2,

        Undefined = 254
    }

    public enum AccountStatus : byte
    {
        New = 0,
        Activated = 1,
        Logged = 2,

        Locked = 200,
        Disabled = 250,

        Undefined = 254
    }

    public enum PropertyType : byte
    {
        Houses = 0,
        Apartment = 1,
        Offices = 2,

        Undefined = 254
    }

    public enum MarkerType : byte
    {
        Building = 0,
        University = 1,
        Attraction = 2,

        Undefined = 254
    }

    public enum InfoType : byte
    {
        Terms = 0,
        Privacy = 1,

        Undefined = 254
    }

    public enum ProductStatus : byte
    {
        New = 0,
        Activated = 1,
        Certified = 2,
        EndCertified = 3,
        Completed = 4,
        Failed = 5,

        Recertified = 6,

        Disabled = 250,
        Undefined = 254
    }

    public enum ObjectType : byte
    {
        Product = 0,

        Undefined = 254
    }

    public enum WarningType : byte
    {
        WarningCompleted = 0,
        WarningInfo = 1,

        Undefined = 254
    }

    public enum NotifyType : byte
    {
        MainLauncher = 0,

        Undefined = 254
    }

    public enum AdvLevel : byte
    {
        MainBanner = 0,
        Powerlink = 1,

        Undefined = 254
    }

    public enum DefaultLocation : int
    {
        Country = 1, // VietName
        Province = 2, // Ho Chi Minh
        Disttrict = 31, // Quan 1
    }


}
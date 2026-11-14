using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel;



namespace RightPoint.Models
{


    public class EnumModel
    {


        public EnumModel(WeekDay item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }

        public EnumModel(RedundantType item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }

        public EnumModel(Education item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }

        public EnumModel(Ages item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }

        public EnumModel(Occupation item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }

        public EnumModel(FamilyIncome item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }

        public EnumModel(MaritalStatus item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }

        public EnumModel(Gender item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }

        public EnumModel(Option item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }

        public EnumModel(DataRange item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());

        }

        public EnumModel(TimeRange item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }

        public EnumModel(Access item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }

        public EnumModel(Media item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }

        public EnumModel(Broadcast item)
        {
            ID = (byte)item;
            Title = LocalizedResource.ValueOf(item.ToString());
        }




        [Key]
        [Display(Name ="ID")]
        public byte ID { get; private set; }
        [Display(Name ="Title")]
        public String Title { get; private set; }




    }


    [Flags]
    public enum WeekDay : byte
    {
        [Display(Name ="شنبه")]
        Saturday = 1,
        [Display(Name ="یکشنبه")]
        Sunday = 2,
        [Display(Name ="دوشنبه")]
        Monday = 3,
        [Display(Name ="سه‌شنبه")]
        Tuesday = 4,
        [Display(Name ="چهارشنبه")]
        Wednesday = 5,
        [Display(Name ="پنج‌شنبه")]
        Thursday = 6,
        [Display(Name ="جمعه")]
        Friday = 7
    }


    [Flags]
    public enum RedundantType : byte
    {
        Coefficient = 1,
        Percentage = 2

    }


    [Flags]
    public enum Gender : byte
    {
        [Display(Name ="انتخاب نشده")]
        None = 0,
        [Display(Name ="مرد")]
        Male = 1,
        [Display(Name ="زن")]
        Female = 2
    }

    [Flags]
    public enum Education : byte
    {
        [Display(Name ="انتخاب نشده")]
        None = 0,
        [Display(Name ="زیر دیپلم")]
        UnderDiploma = 1,
        [Display(Name ="دیپلم")]
        Diploma = 2,
        [Display(Name ="فوق دیپلم")]
        AD = 3,
        [Display(Name ="کارشناسی")]
        Bachelor = 4,
        [Display(Name ="کارشناسی ارشد و بالاتر")]
        MAandAbove = 5,
        [Display(Name ="دانشجو")]
        Student = 6
    }

    [Flags]
    public enum Ages : byte
    {
        [Display(Name ="انتخاب نشده")]
        None = 0,
        [Display(Name ="زیر ۲۰ سال")]
        Under20 = 1,
        [Display(Name ="۲۰ - ۲۹ سال")]
        Under30 = 2,
        [Display(Name ="۳۰-۳۹ سال")]
        Under40 = 3,
        [Display(Name ="۴۰ - ۴۰ سال")]
        Under50 = 4,
        [Display(Name ="۵۰- ۵۹ سال")]
        Under60 = 5,
        [Display(Name ="بالای ۶۰ سال")]
        Above60 = 6
    }

    [Flags]
    public enum Occupation : byte
    {
        [Display(Name ="انتخاب نشده")]
        None = 0,
        [Display(Name ="آزاد")]
        Free = 1,
        [Display(Name ="کارمند")]
        Employee = 2,
        [Display(Name ="کارگر")]
        Labor = 3,
        [Display(Name ="بی‌کار")]
        Unemployed = 4,
        [Display(Name ="خانه‌دار")]
        Housewife = 5,
        [Display(Name ="دانشجو")]
        Student = 6,
        [Display(Name ="سایر")]
        Other = 7
    }


    [Flags]
    public enum FamilyIncome : byte
    {
        [Display(Name ="انتخاب نشده")]
        None = 0,
        [Display(Name ="زیر ۶۰۰ هزار")]
        Under600000 = 1,
        [Display(Name ="۶۰۰-۸۰۰")]
        Under800000 = 2,
        [Display(Name ="۸۰۰ - ۱ میلیون")]
        Under1000000 = 3,
        [Display(Name ="۱میلیون - ۲میلیون")]
        Under2000000 = 4,
        [Display(Name ="۲میلیون - ۳میلیون")]
        Under3000000 = 5,
        [Display(Name ="۳میلیون- ۴میلیون")]
        Under4000000 = 6,
        [Display(Name ="بالای ۴ میلیون")]
        Above4000000 = 7
    }

    [Flags]
    public enum MaritalStatus : byte
    {
        [Display(Name ="انتخاب نشده")]
        None = 0,
        [Display(Name ="مجرد")]
        Single = 1,
        [Display(Name ="متأهل")]
        Maried = 2
    }


    [Flags]
    public enum Option : byte
    {
        [Display(Name ="بله")]
        Yes = 1,
        [Display(Name ="گاهی اوقات")]
        SomeTimes = 2,
        [Display(Name ="خیر")]
        Noo = 3
    }




    [Flags]
    public enum TimeRange : byte
    {
        [Display(Name ="یک هفته")]
        One = 1,
        [Display(Name ="دو هفته")]
        Two = 2,
        [Display(Name ="سه هفته")]
        Three = 3,
        [Display(Name ="چهار هفته")]
        Four = 4
    }


    [Flags]
    public enum DataRange : byte
    {
        [Display(Name ="کامل")]
        Total = 1,
        [Display(Name ="نصف")]
        Half = 2,
        [Display(Name ="یک سوم")]
        OneThird = 3,
        [Display(Name ="یک چهارم")]
        Quarter = 4,
        [Display(Name ="یک پنجم")]
        OneFifth = 5
    }


    [Flags]
    public enum Access : byte
    {
        [Display(Name ="ادمین")]
        Admin = 1,
        [Display(Name ="مدیر")]
        Manager = 2,
        [Display(Name ="کاربر")]
        User = 3,
        [Display(Name ="مشتری")]
        Client = 4,



    }



    [Flags]
    public enum Media : byte
    {
        [Display(Name ="تلویزیونی")]
        TV = 1,
        [Display(Name ="رادیویی")]
        Radio = 2,
       

    }



    [Flags]
    public enum Broadcast : byte
    {
        [Display(Name ="انتخاب نشده")]
        None = 0,
        [Display(Name ="داخلی")]
        Local = 1,
        [Display(Name ="برون مرزی")]
        International = 2,
        [Display(Name ="استانی")]
        Province = 3,

    }
}
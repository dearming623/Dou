using System;

namespace DouNamespace
{
    public static class UserPlateHelper
    {
        public static int ConvertHourToIndex(int hour)
        {
            switch (hour)
            {
                case 0:
                    return 0;
                case 1:
                case 2:
                    return 1;
                case 3:
                case 4:
                    return 2;
                case 5:
                case 6:
                    return 3;
                case 7:
                case 8:
                    return 4;
                case 9:
                case 10:
                    return 5;
                case 11:
                case 12:
                    return 6;
                case 13:
                case 14:
                    return 7;
                case 15:
                case 16:
                    return 8;
                case 17:
                case 18:
                    return 9;
                case 19:
                case 20:
                    return 10;
                case 21:
                case 22:
                    return 11;
                case 23:
                    return 0;
                default:
                    return 0;
            }
        }

        public static Plate BuildPlate(User user)
        {
            if (user == null) return null;

            DateTime dt = new DateTime(user.Year, user.Month, user.Day);
            ChineseCalendar cc = new ChineseCalendar(dt);

            Plate p = new Plate();
            p.YearSky = cc.YearSky;
            p.YearEarth = cc.YearEarth;
            p.month = cc.ChineseMonth - 1;
            p.day = cc.ChineseDay - 1;
            p.hour = ConvertHourToIndex(user.Hour);
            p.Sex = user.Sex;
            p.Type = 0;
            p.first_name = user.Name ?? string.Empty;
            p.CreatePlate();
            return p;
        }

        public static string GetPalaceEarth(User user, int douIndex)
        {
            Plate p = BuildPlate(user);
            if (p == null || p.zone_list == null) return string.Empty;

            foreach (Zone zone in p.zone_list)
            {
                if (zone.dou == douIndex)
                {
                    return DouSource.EARTH[zone.id];
                }
            }
            return string.Empty;
        }

        public static string GetSolarBirthdayText(User user)
        {
            if (user == null) return string.Empty;
            return string.Format("{0}-{1:D2}-{2:D2} {3:D2}:{4:D2}",
                user.Year, user.Month, user.Day, user.Hour, user.Minute);
        }

        public static string GetLunarBirthdayText(User user)
        {
            if (user == null) return string.Empty;

            try
            {
                DateTime dt = new DateTime(user.Year, user.Month, user.Day);
                ChineseCalendar cc = new ChineseCalendar(dt);
                return cc.ChineseDateString;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}

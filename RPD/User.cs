using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class User
    {

        public static long Insert(TBL_User input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_User.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_User input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_User ret = context.TBL_User.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Access = input.FLD_Access;
                ret.FLD_Cell = input.FLD_Cell;
                ret.FLD_Company_FK = input.FLD_Company_FK;
                ret.FLD_DisableDate = input.FLD_DisableDate;
                ret.FLD_Gender = input.FLD_Gender;
                ret.FLD_Name = input.FLD_Name;
                ret.FLD_Password = input.FLD_Password;
                ret.FLD_Username = input.FLD_Username;
                ret.FLD_FirstVisit = input.FLD_FirstVisit;
                ret.FLD_LastVisit = input.FLD_LastVisit;
                ret.TBL_Company = input.TBL_Company;
                

                foreach (TBL_CampaignAccess item in input.TBL_CampaignAccess)
                {
                    ret.TBL_CampaignAccess.Add(item);
                }
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_User ret = context.TBL_User.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_User.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool Publish(long inputID,bool publish)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Profile ret = context.TBL_Profile.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                ret.FLD_IsEnabled = publish;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool Firstvisit(long inputID, bool status)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_User ret = context.TBL_User.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                ret.FLD_FirstVisit = status;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static VIW_User FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            VIW_User ret = context.VIW_User.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }

        public static VIW_User FindByAthenticate(String user, String pass)
        {
            RightPointEntities context = new RightPointEntities();
            VIW_User ret = context.VIW_User.SingleOrDefault(i => i.FLD_Username == user && i.FLD_Password == pass);
            return ret;
        }
        public static bool IsAthenticate(String user, String pass)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_User ret = context.TBL_User.SingleOrDefault(i => i.FLD_Username == user && i.FLD_Password == pass);
            if (ret != null)
            {
                ret.FLD_LastVisit = DateTime.Now;
                context.SaveChanges();
                RPC.Common.CurrentUser = new RPC.User() { ID = ret.FLD_ID_PK, Username = ret.FLD_Username, Password = ret.FLD_Password,FirstVisit=ret.FLD_FirstVisit };
                if (ret.FLD_Username.Equals(user) && ret.FLD_Password.Equals(pass))
                    return true;
            }
            return false;
        }





        public static IQueryable<VIW_User> FindByCompany(long companyID)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_User.Where(i => i.FLD_Company_FK == companyID).AsQueryable();
        }
        public static IQueryable<VIW_User> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_User.AsQueryable();
        }
        public static IQueryable<VIW_User> SelectRange(int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_User.OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }




        public static IQueryable<VIW_User> FilterByPublish(bool IsEnabled)
        {
            RightPointEntities context = new RightPointEntities();
            if (IsEnabled)

                return context.VIW_User.Where(i => i.FLD_IsEnabled == true).AsQueryable();
            else
                return context.VIW_User.Where(i => ((i.FLD_IsEnabled == false) || (i.FLD_IsEnabled == (bool?)null))).AsQueryable();
        }

        public static IQueryable<VIW_User> FilterBySearch(String keyword)
        {
            RightPointEntities context = new RightPointEntities();

            return context.VIW_User.Where(i => ((i.FLD_Name.Contains(keyword)) || (i.FLD_Title.Contains(keyword)) || (i.FLD_Name.Contains(keyword)))).AsQueryable();

        }

        public static IQueryable<VIW_User> FilterByPublish(bool IsEnabled, int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            if (IsEnabled)

                return context.VIW_User.Where(i => i.FLD_IsEnabled == true).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
            else
                return context.VIW_User.Where(i => ((i.FLD_IsEnabled == false) || (i.FLD_IsEnabled == (bool?)null))).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }





        public static long Count()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_User.Count();
        }

    }
}

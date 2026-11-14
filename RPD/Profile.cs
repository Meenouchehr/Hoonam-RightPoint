using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Profile
    {
        public static long Insert(TBL_Profile input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Profile.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Profile input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Profile ret = context.TBL_Profile.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_CreateDate = input.FLD_CreateDate;
                ret.FLD_Email = input.FLD_Email;
                ret.FLD_IsEnabled = input.FLD_IsEnabled;
                ret.FLD_Phone = input.FLD_Phone;             
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Profile ret = context.TBL_Profile.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Profile.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static VIW_Profile FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            VIW_Profile ret = context.VIW_Profile.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<VIW_Profile> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Profile.AsQueryable();
        }
      

    }
}

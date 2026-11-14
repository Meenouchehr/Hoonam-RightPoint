using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Emails
    {
        public static long Insert(TBL_Emails input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Emails.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Emails input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Emails ret = context.TBL_Emails.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Email = input.FLD_Email;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Emails ret = context.TBL_Emails.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Emails.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Emails FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Emails ret = context.TBL_Emails.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Emails> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Emails.AsQueryable();
        }
    }
}

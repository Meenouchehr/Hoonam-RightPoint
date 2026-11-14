using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class OwnerGroup
    {

        public static long Insert(TBL_OwnerGroup input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_OwnerGroup.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_OwnerGroup input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_OwnerGroup ret = context.TBL_OwnerGroup.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Commnet = input.FLD_Commnet;
                ret.FLD_EnglishComment = input.FLD_EnglishComment;
                ret.FLD_Title = input.FLD_Title;
                ret.FLD_EnglishTitle = input.FLD_EnglishTitle;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_OwnerGroup ret = context.TBL_OwnerGroup.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_OwnerGroup.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_OwnerGroup FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_OwnerGroup ret = context.TBL_OwnerGroup.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_OwnerGroup> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_OwnerGroup.AsQueryable();
        }

    }
}

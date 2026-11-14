using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Foretaste
    {
        public static long Insert(TBL_Foretaste input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Foretaste.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Foretaste input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Foretaste ret = context.TBL_Foretaste.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_ID_PK = input.FLD_ID_PK;
                ret.FLD_EnglishImage = input.FLD_EnglishImage;
                ret.FLD_EnglishTitle = input.FLD_EnglishTitle;
                ret.FLD_Image = input.FLD_Image;
                ret.FLD_Title = input.FLD_Title;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Foretaste ret = context.TBL_Foretaste.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Foretaste.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static TBL_Foretaste FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Foretaste ret = context.TBL_Foretaste.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }

      
        public static IQueryable<TBL_Foretaste> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Foretaste.AsQueryable();
        }
    }
}

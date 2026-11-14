using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Responder
    {
        public static long Insert(TBL_Responder input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Responder.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Responder input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Responder ret = context.TBL_Responder.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Name = input.FLD_Name;
                ret.FLD_Gender = input.FLD_Gender;
                ret.FLD_MaritalStatus = input.FLD_MaritalStatus;
                ret.FLD_Ages = input.FLD_Ages;
                ret.FLD_Cooperation = input.FLD_Cooperation;
                ret.FLD_Education = input.FLD_Education;
                ret.FLD_FamilyIncome = input.FLD_FamilyIncome;
                ret.FLD_Occupation = input.FLD_Occupation;
                ret.FLD_City_FK = input.FLD_City_FK;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Responder ret = context.TBL_Responder.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Responder.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Responder FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Responder ret = context.TBL_Responder.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<TBL_Responder> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Responder.AsQueryable();
        }
    }
}

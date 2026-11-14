using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class CampaignAccess
    {
        public static long Insert(TBL_CampaignAccess input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_CampaignAccess.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_CampaignAccess input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_CampaignAccess ret = context.TBL_CampaignAccess.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Campaign_FK = input.FLD_Campaign_FK;
                ret.FLD_User_FK = input.FLD_User_FK;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_CampaignAccess ret = context.TBL_CampaignAccess.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_CampaignAccess.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static void DeleteByCampaign(long campaign)
        {
            RightPointEntities context = new RightPointEntities();

            context.TBL_CampaignAccess.RemoveRange(context.TBL_CampaignAccess.Where(i => i.FLD_Campaign_FK == campaign));
            context.SaveChanges();
        }

        public static void DeleteByUser(long user)
        {
            RightPointEntities context = new RightPointEntities();

            context.TBL_CampaignAccess.RemoveRange(context.TBL_CampaignAccess.Where(i => i.FLD_User_FK == user));
            context.SaveChanges();
        }

        public static VIW_CampaignAccess FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            VIW_CampaignAccess ret = context.VIW_CampaignAccess.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<VIW_CampaignAccess> FindByCampaign(long campaign)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_CampaignAccess.Where(i => i.FLD_Campaign_FK == campaign).AsQueryable();
        }

        public static IQueryable<VIW_CampaignAccess> FindByUser(long user)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_CampaignAccess.Where(i => i.FLD_User_FK == user).AsQueryable();
        }
        public static IQueryable<VIW_CampaignAccess> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_CampaignAccess.AsQueryable();
        }
    }
}


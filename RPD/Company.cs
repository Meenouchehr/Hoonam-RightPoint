using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Company
    {
        public static long Insert(TBL_Company input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Company.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Company input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Company ret = context.TBL_Company.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Group_FK = input.FLD_Group_FK;
                ret.FLD_Image = input.FLD_Image;
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
            TBL_Company ret = context.TBL_Company.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Company.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }



        public static bool Publish(long inputID, bool publish)
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

        public static VIW_Company FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            VIW_Company ret = context.VIW_Company.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }

        public static IQueryable<VIW_Company> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Company.AsQueryable();
        }


        public static IQueryable<VIW_Company> SelectRange(int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Company.OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }



        public static IQueryable<VIW_Company> FilterByPublish(bool isPublish)
        {
            RightPointEntities context = new RightPointEntities();
            if (isPublish)

                return context.VIW_Company.Where(i => i.FLD_IsEnabled == true).AsQueryable();
            else
                return context.VIW_Company.Where(i => ((i.FLD_IsEnabled == false) || (i.FLD_IsEnabled == (bool?)null))).AsQueryable();
        }

        public static IQueryable<VIW_Company> FilterBySearch(String keyword)
        {
            RightPointEntities context = new RightPointEntities();

            return context.VIW_Company.Where(i => ((i.FLD_GroupName.Contains(keyword)) || (i.FLD_Title.Contains(keyword)))).AsQueryable();

        }

        public static IQueryable<VIW_Company> FilterByPublish(bool isPublish, int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            if (isPublish)

                return context.VIW_Company.Where(i => i.FLD_IsEnabled == true).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
            else
                return context.VIW_Company.Where(i => ((i.FLD_IsEnabled == false) || (i.FLD_IsEnabled == (bool?)null))).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }




        public static long Count()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Company.Count();
        }
    }
}

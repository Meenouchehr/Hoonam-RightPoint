using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPD
{
    public class Channel
    {
        public static long Insert(TBL_Channel input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Channel.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Channel input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Channel ret = context.TBL_Channel.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Broadcast = input.FLD_Broadcast;
                ret.FLD_Media_FK = input.FLD_Media_FK;
                ret.FLD_Title = input.FLD_Title;
                ret.FLD_EnglishTitle = input.FLD_EnglishTitle;
                ret.FLD_Image = input.FLD_Image;
                ret.FLD_Province_FK = input.FLD_Province_FK;
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Channel ret = context.TBL_Channel.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Channel.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static TBL_Channel FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Channel ret = context.TBL_Channel.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }

        public static IQueryable<TBL_Channel> FilterByMedia(byte media)
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Channel.Where(i => i.FLD_Media_FK == media).AsQueryable();
        }

        public static IQueryable<TBL_Channel> FilterByMedia_Broadcast(byte media,byte broadcast)
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Channel.Where(i => i.FLD_Media_FK == media && i.FLD_Broadcast==broadcast).AsQueryable();
        }
        
        
        
        
        public static IQueryable<TBL_Channel> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Channel.AsQueryable();
        }

        public static IQueryable<TBL_Channel> SelectRange(int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Channel.OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }

        public static IQueryable<TBL_Channel> FilterBySearch(String keyword)
        {
            RightPointEntities context = new RightPointEntities();

            return context.TBL_Channel.Where(i => ((i.FLD_Title.Contains(keyword)))).AsQueryable();

        }

       



        public static long Count()
        {
            RightPointEntities context = new RightPointEntities();
            return context.TBL_Channel.Count();
        }
    }


}

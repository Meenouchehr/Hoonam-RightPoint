using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic;

namespace RPD
{
    public class Program
    {

        public static long Insert(TBL_Program input)
        {
            RightPointEntities context = new RightPointEntities();
            context.TBL_Program.Add(input);
            context.SaveChanges();
            return input.FLD_ID_PK;
        }
        public static bool Update(TBL_Program input)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Program ret = context.TBL_Program.SingleOrDefault(i => i.FLD_ID_PK == input.FLD_ID_PK);
            if (ret != null)
            {
                ret.FLD_Channel_FK = input.FLD_Channel_FK;
                ret.FLD_Enable = input.FLD_Enable;
                ret.FLD_EndTime = input.FLD_EndTime;
                ret.FLD_ID_PK = input.FLD_ID_PK;
                ret.FLD_EndTime = input.FLD_EndTime;
                ret.FLD_StartTime = input.FLD_StartTime;
                ret.FLD_PlayTime = input.FLD_PlayTime;
                ret.FLD_Title = input.FLD_Title;
                ret.FLD_EnglishTitle = input.FLD_EnglishTitle;
                ret.FLD_After = input.FLD_After;
                ret.FLD_Before = input.FLD_Before;
                ret.FLD_Middle = input.FLD_Middle;
               
                foreach (TBL_WeekRep weekrep in input.TBL_WeekRep)
                {
                    ret.TBL_WeekRep.Add(weekrep);
                }
                foreach (TBL_Limit limit in input.TBL_Limit)
                {
                    ret.TBL_Limit.Add(limit);
                }

                foreach (TBL_ProgramAccess limit in input.TBL_ProgramAccess)
                {
                    ret.TBL_ProgramAccess.Add(limit);
                }
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public static bool Delete(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Program ret = context.TBL_Program.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                context.TBL_Program.Remove(ret);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool Publish(long inputID, bool publish)
        {
            RightPointEntities context = new RightPointEntities();
            TBL_Program ret = context.TBL_Program.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            if (ret != null)
            {
                ret.FLD_Enable = publish;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static VIW_Program FindByID(long inputID)
        {
            RightPointEntities context = new RightPointEntities();
            VIW_Program ret = context.VIW_Program.SingleOrDefault(i => i.FLD_ID_PK == inputID);
            return ret;
        }
        public static IQueryable<VIW_Program> All()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Program.AsQueryable();
        }


        public static IQueryable<VIW_Program> FilterByDate(DateTime item)
        {
            
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Program.Where(i=> System.Data.Entity.DbFunctions.TruncateTime(i.FLD_StartTime)<=item && System.Data.Entity.DbFunctions.TruncateTime(i.FLD_EndTime)>=item).AsQueryable();
        }



        public static IQueryable<VIW_Program> SelectRange(int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Program.OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }



        public static IQueryable<VIW_Program> FilterByPublish(bool isPublish)
        {
            RightPointEntities context = new RightPointEntities();
            if (isPublish)

                return context.VIW_Program.Where(i => i.FLD_Enable == true).AsQueryable();
            else
                return context.VIW_Program.Where(i => ((i.FLD_Enable == false) || (i.FLD_Enable == (bool?)null))).AsQueryable();
        }

        public static IQueryable<VIW_Program> FilterBySearch(String keyword)
        {
            RightPointEntities context = new RightPointEntities();
            DateTime? date = keyword.ToGerigorian();
            return context.VIW_Program.Where(i => ((i.FLD_ChannelName.Contains(keyword)) || (i.FLD_Title.Contains(keyword) || System.Data.Entity.DbFunctions.TruncateTime(i.FLD_EndTime) >= System.Data.Entity.DbFunctions.TruncateTime(date)))).AsQueryable();

        }

        public static IQueryable<VIW_Program> FilterByChannel(long channel)
        {
            RightPointEntities context = new RightPointEntities();

            return context.VIW_Program.Where(i => i.FLD_Channel_FK == channel);
        }

        public static IQueryable<VIW_Program> FilterByPublish(bool isPublish, int start, int end)
        {
            RightPointEntities context = new RightPointEntities();
            if (isPublish)

                return context.VIW_Program.Where(i => i.FLD_Enable == true).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
            else
                return context.VIW_Program.Where(i => ((i.FLD_Enable == false) || (i.FLD_Enable == (bool?)null))).OrderBy(i => i.FLD_ID_PK).Skip(start).Take(end - start).AsQueryable();
        }



        public static long Count()
        {
            RightPointEntities context = new RightPointEntities();
            return context.VIW_Program.Count();
        }
    }
}

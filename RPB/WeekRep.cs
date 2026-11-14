using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class WeekRep : RPC.Common
    {


        public void Insert(long program, List<RPC.WeekRep> list)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    List<RPC.Result> retList = new List<RPC.Result>();
                    Insert(program, list, retList);

                    scope.Complete();

                    foreach (RPC.Result res in retList)
                    {
                        OnChange(res);
                    }

                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.NotFound });
            }
        }
        public void Insert(long program, List<RPC.WeekRep> list, List<RPC.Result> retList)
        {
            if (retList == null)
                retList = new List<RPC.Result>();
            foreach (RPC.WeekRep rep in list)
            {
                rep.Program = program;
                RPC.Result res = new RPC.Result();
                res.RetID = RPD.WeekRep.Insert(Convert.ToWeekRep(rep));
                res.RetOperation = RPC.Operation.Insert;
                res.RetUpshot = RPC.Upshot.Success;
            }
        }

        public void Update(long program, List<RPC.WeekRep> list)
        {

            try
            {

                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    List<RPC.Result> retList = new List<RPC.Result>();

                    Update(program, list, retList);

                    scope.Complete();

                    foreach (RPC.Result res in retList)
                    {
                        OnChange(res);
                    }

                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Update, RetUpshot = RPC.Upshot.Fail });
            }
        }
        public void Update(long program, List<RPC.WeekRep> list, List<RPC.Result> retList)
        {

            List<RPC.WeekRep> oldList = RPD.WeekRep.FindByProgram(program).Select(Convert.ToWeekRep).ToList();
            if ((oldList == null) || (oldList.Count > 0))
            {
                Insert(program, list, retList);
            }
            else
            {
                foreach (RPC.WeekRep rep in list)
                {
                    bool chk = RPD.WeekRep.Update(Convert.ToWeekRep(rep));
                    if (chk)
                    {
                        retList.Add(new RPC.Result() { RetID = rep.ID, RetOperation = RPC.Operation.Update, RetUpshot = RPC.Upshot.Success });
                        try
                        {
                            oldList.Remove(oldList.SingleOrDefault(i => i.ID == rep.ID));
                        }
                        catch { }
                    }
                    else
                    {
                        RPD.WeekRep.Insert(Convert.ToWeekRep(rep));
                        retList.Add(new RPC.Result() { RetID = rep.ID, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Success });

                    }
                }

                if (oldList.Count > 0)
                {
                    foreach (RPC.WeekRep rep in oldList)
                    {
                        RPD.WeekRep.Delete(rep.ID);
                        retList.Add(new RPC.Result() { RetID = rep.ID, RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.Success });

                    }
                }
            }
        }

        public void Delete(long id)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    bool chk = RPD.WeekRep.Delete(id);
                    scope.Complete();
                    if (chk)
                    {
                        OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.Success });
                    }
                    else
                    {
                        scope.Complete();
                        OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.NotFound });
                    }
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.Fail });
            }
        }
        public RPC.WeekRep FindByID(long id)
        {
            return Convert.ToWeekRep(RPD.WeekRep.FindByID(id));
        }
        public List<RPC.WeekRep> FindByProgram(long programID)
        {
            return RPD.WeekRep.FindByProgram(programID).Select(Convert.ToWeekRep).ToList();
        }
        public List<RPC.WeekRep> All()
        {
            return RPD.WeekRep.All().Select(Convert.ToWeekRep).ToList();

        }





    }
}

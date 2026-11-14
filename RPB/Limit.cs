using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Limit : RPC.Common
    {
        public void Insert(long program, List<RPC.Limit> list)
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

        public void Insert(long program, List<RPC.Limit> list, List<RPC.Result> retList)
        {
            if (retList == null)
                retList = new List<RPC.Result>();
            foreach (RPC.Limit rep in list)
            {
                rep.Program = program;
                RPC.Result res = new RPC.Result();
                res.RetID = RPD.Limit.Insert(Convert.ToLimit(rep));
                res.RetOperation = RPC.Operation.Insert;
                res.RetUpshot = RPC.Upshot.Success;
            }
        }

        public void Update(long program, List<RPC.Limit> list)
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

        public void Update(long program, List<RPC.Limit> list, List<RPC.Result> retList)
        {

            List<RPC.Limit> oldList = RPD.Limit.FindByProgram(program).Select(Convert.ToLimit).ToList();
            if ((oldList == null) || (oldList.Count > 0))
            {
                Insert(program, list, retList);
            }
            else
            {
                foreach (RPC.Limit rep in list)
                {
                    bool chk = RPD.Limit.Update(Convert.ToLimit(rep));
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
                        RPD.Limit.Insert(Convert.ToLimit(rep));
                        retList.Add(new RPC.Result() { RetID = rep.ID, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Success });

                    }
                }

                if (oldList.Count > 0)
                {
                    foreach (RPC.Limit rep in oldList)
                    {
                        RPD.Limit.Delete(rep.ID);
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

                    bool chk = RPD.Limit.Delete(id);
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
        public RPC.Limit FindByID(long id)
        {
            return Convert.ToLimit(RPD.Limit.FindByID(id));
        }

        public List<RPC.Limit> FindByProgram(long programID)
        {
            return RPD.Limit.FindByProgram(programID).Select(Convert.ToLimit).ToList();
        }

        public List<RPC.Limit> FindByTime(DateTime? start, DateTime? end)
        {
            return RPD.Limit.FindByTime(start,end).Select(Convert.ToLimit).ToList();
        }
        public List<RPC.Limit> All()
        {
            return RPD.Limit.All().Select(Convert.ToLimit).ToList();

        }



    }
}

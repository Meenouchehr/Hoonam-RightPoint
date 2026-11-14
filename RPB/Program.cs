using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generic;

namespace RPB
{
    public class Program : RPC.Common
    {

        public void Save(RPC.Program item)
        {
            if (item.ID == 0)
                Insert(item);
            else
                Update(item);
        }

        public void Insert(RPC.Program item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    if ((item.Channel != null) && (item.Channel == 0))
                    {
                        item.Channel = RPD.Channel.Insert(Convert.ToChannel(item));
                    }


                    item.ProgramAccessList.RemoveAll(i => i.Month == null || i.Month == 0);
                    long id = RPD.Program.Insert(Convert.ToProgram(item));

                    scope.Complete();

                    OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Success });
                }
            }
            catch (Exception ex)
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.NotFound });
            }
        }

        public void Update(RPC.Program item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {


                    RPD.Limit.DeleteByProgram(item.ID);

                    RPD.WeekRep.DeleteByProgram(item.ID);


                    RPC.ProgramAccess pa = item.ProgramAccessList.Find(i => i.Month != null && i.Month != 0);

                    if (pa == null)
                        RPD.ProgramAccess.DeleteByProgram(item.ID);
                    else
                    {
                        RPD.ProgramAccess.DeleteByProgram(item.ID, (byte)pa.Month);
                        item.ProgramAccessList.RemoveAll(i => i.Month != pa.Month);
                    }

                    bool chk = RPD.Program.Update(Convert.ToProgram(item));


                    scope.Complete();
                    if (chk)
                    {
                        OnChange(new RPC.Result() { RetID = item.ID, RetOperation = RPC.Operation.Update, RetUpshot = RPC.Upshot.Success });
                    }
                    else
                    {
                        OnChange(new RPC.Result() { RetID = item.ID, RetOperation = RPC.Operation.Update, RetUpshot = RPC.Upshot.NotFound });
                    }
                }
            }
            catch (Exception ex)
            {

                OnChange(new RPC.Result() { RetID = item.ID, RetOperation = RPC.Operation.Update, RetUpshot = RPC.Upshot.Fail });
            }
        }
        public void Delete(long id)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    RPD.Limit.DeleteByProgram(id);
                    RPD.WeekRep.DeleteByProgram(id);
                    bool chk = RPD.Program.Delete(id);
                    scope.Complete();
                    if (chk)
                    {
                        OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.Success });
                    }
                    else
                    {

                        OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.NotFound });
                    }
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.Fail });
            }
        }

        public void Delete(List<RPC.Program> list)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.Program item in list)
                    {
                        RPD.Limit.DeleteByProgram(item.ID);
                        RPD.WeekRep.DeleteByProgram(item.ID);
                        RPD.ProgramAccess.DeleteByProgram(item.ID);
                        RPD.Program.Delete(item.ID);
                    }
                    scope.Complete();
                    OnChange(new RPC.Result() { RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.Success });

                }
            }
            catch (Exception ex)
            {

                int code = ex.HResult;
                if (code == -2146233087)
                    OnChange(new RPC.Result() { RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.RelationError });
                else
                    OnChange(new RPC.Result() { RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.Fail });
            }
        }

        public void Publish(List<RPC.Program> list, bool publish)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.Program item in list)
                    {
                        RPD.Program.Publish(item.ID, publish);
                        // RPD.Profile.Delete(item.ID);

                    }
                    scope.Complete();
                    OnChange(new RPC.Result() { RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.Success });

                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.Fail });
            }
        }


        public RPC.Program FindByID(long id)
        {
            RPC.Program obj = Convert.ToProgram(RPD.Program.FindByID(id));
            obj.ProgramAccessList = RPD.ProgramAccess.FindByProgram(id).Select(Convert.ToProgramAccess).ToList();
            return obj;
        }
        public List<RPC.Program> All()
        {
            return RPD.Program.All().Select(Convert.ToProgram).ToList();

        }

        public List<RPC.Program> FilterByDate(DateTime item)
        {
            return RPD.Program.FilterByDate(item).Select(Convert.ToProgram).ToList();

        }


        public List<RPC.Program> FilterByPublish(bool isPublish)
        {
            return RPD.Program.FilterByPublish(isPublish).Select(Convert.ToProgram).ToList();
        }

        public List<RPC.Program> FilterByPublish(bool isPublish, int start, int no)
        {
            int end = (start + no - 1);
            return RPD.Program.FilterByPublish(isPublish, start, end).Select(Convert.ToProgram).ToList();
        }

        public List<RPC.Program> FilterBySearch(String keyword)
        {
            return RPD.Program.FilterBySearch(keyword).Select(Convert.ToProgram).ToList();

        }

        public List<RPC.Program> FilterByChannel(long channel)
        {
            return RPD.Program.FilterByChannel(channel).Select(Convert.ToProgram).ToList();

        }
        public List<RPC.Program> FindByQuestionnaire(long questionnaireID)
        {
            return RPD.Question.FindByQuestionnaire(questionnaireID).Select(Convert.ToProgram).ToList();
        }


        //public List<RPC.Program> FindByProgram(long id)
        //{
        //    return RPD.Question.FindByProgram(id).Select(Convert.ToProgram).ToList();
        //}

        public List<RPC.Program> SelectRange(int start, int no)
        {

            int end = (start + no - 1);
            return RPD.Program.SelectRange(start, end).Select(Convert.ToProgram).ToList();
        }

        public long Count()
        {

            return RPD.Program.Count();
        }

    }
}

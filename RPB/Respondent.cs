using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Respondent : RPC.Common
    {

        public void Save(RPC.Respondent item)
        {
            if (item.ID == 0)
                Insert(item);
            else
                Update(item);
        }
        public void Insert(RPC.Respondent item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    long id = RPD.Respondent.Insert(Convert.ToRespondent(item));


                    scope.Complete();

                    OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Success });
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Fail });
            }
        }
        public void Update(RPC.Respondent item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    RPD.Answer.DeleteByRespondent(item.ID);
                    RPD.Responder.Update(Convert.ToResponder(item.ResponderProfile));
                    bool chk = RPD.Respondent.Update(Convert.ToRespondent(item));
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
            catch
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
                    RPD.Answer.DeleteByRespondent(id);
                    bool chk = RPD.Respondent.Delete(id);
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

        public void Delete(List<RPC.Respondent> list)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.Respondent item in list)
                    {
                        RPD.Answer.DeleteByRespondent(item.ID);
                        RPD.Respondent.Delete(item.ID);
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

        public void Publish(List<RPC.Respondent> list, bool publish)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.Respondent item in list)
                    {
                        RPD.Respondent.Publish(item.ID, publish);
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

        public void PublishByQuestionnaire(long questionnaire, bool publish)
        {
            try
            {
                List<RPD.VIW_Respondent> list = RPD.Respondent.FilterByQuestionnaire(questionnaire).ToList();
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPD.VIW_Respondent item in list)
                    {
                        RPD.Respondent.Publish(item.FLD_ID_PK, publish);
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

        public RPC.Respondent FindByID(long id)
        {
            return Convert.ToRespondent(RPD.Respondent.FindByID(id));
        }
        public List<RPC.Respondent> All()
        {
            return RPD.Respondent.All().Select(Convert.ToRespondent).ToList();

        }


        public List<RPC.Respondent> FilterByUser(long user)
        {
            return RPD.Respondent.FilterByUser(user).Select(Convert.ToRespondent).ToList();
        }

        public List<RPC.Respondent> FilterByPublish(bool isPublish)
        {
            return RPD.Respondent.FilterByPublish(isPublish).Select(Convert.ToRespondent).ToList();
        }

        public List<RPC.Respondent> FilterByPublish(bool isPublish, int start, int no)
        {
            int end = (start + no - 1);
            return RPD.Respondent.FilterByPublish(isPublish, start, end).Select(Convert.ToRespondent).ToList();
        }

        public List<RPC.Respondent> FilterBySearch(String keyword)
        {
            return RPD.Respondent.FilterBySearch(keyword).Select(Convert.ToRespondent).ToList();

        }

        public List<RPC.Respondent> SelectRange(int start, int no)
        {

            int end = (start + no - 1);
            return RPD.Respondent.SelectRange(start, end).Select(Convert.ToRespondent).ToList();
        }

        public List<RPC.Respondent> SelectRange(long user, int start, int no)
        {

            int end = (start + no - 1);
            return RPD.Respondent.SelectRange(user, start, end).Select(Convert.ToRespondent).ToList();
        }

        public long Count()
        {

            return RPD.Respondent.Count();
        }

        public long Count(bool isPublish)
        {

            return RPD.Respondent.Count(isPublish);
        }

        public long Count(long questionnaire)
        {

            return RPD.Respondent.Count(questionnaire);
        }
    }
}

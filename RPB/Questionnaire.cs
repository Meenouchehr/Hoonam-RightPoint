using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Questionnaire : RPC.Common
    {

        public void Save(RPC.Questionnaire item)
        {
            if (item.ID == 0)
                Insert(item);
            else
                Update(item);
        }
        protected void Insert(RPC.Questionnaire item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    long id = RPD.Questionnaire.Insert(Convert.ToQuestionnaire(item));
                    scope.Complete();

                    OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Success });
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Fail });
            }
        }
        protected void Update(RPC.Questionnaire item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    RPD.Question.DeleteByQuestionnaire(item.ID);
                    bool chk = RPD.Questionnaire.Update(Convert.ToQuestionnaire(item));
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
                    RPD.Question.DeleteByQuestionnaire(id);
                    bool chk = RPD.Questionnaire.Delete(id);
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
        public void Delete(List<RPC.Questionnaire> list)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.Questionnaire item in list)
                    {
                        RPD.Question.DeleteByQuestionnaire(item.ID);
                        RPD.Questionnaire.Delete(item.ID);
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

        public void Publish(List<RPC.Questionnaire> list, bool publish)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.Questionnaire item in list)
                    {
                        RPD.Questionnaire.Publish(item.ID, publish);
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

        public RPC.Questionnaire FindByID(long id)
        {
            return Convert.ToQuestionnaire(RPD.Questionnaire.FindByID(id));
        }
        public List<RPC.Questionnaire> All()
        {
            return RPD.Questionnaire.All().Select(Convert.ToQuestionnaire).ToList();

        }
        public List<RPC.Questionnaire> SelectRange(int start, int no)
        {

            //long count = RPD.Questionnaire.Count();
            
            int end=(start+no-1);
            //if (end>count)
            //{
            //    end = (int)count;
            //}

            return RPD.Questionnaire.SelectRange(start, end).Select(Convert.ToQuestionnaire).ToList();
        }

        public List<RPC.Questionnaire> FilterByPublish(bool isPublish)
        {
            return RPD.Questionnaire.FilterByPublish(isPublish).Select(Convert.ToQuestionnaire).ToList();
        }

        public List<RPC.Questionnaire> FilterByPublish(bool isPublish, int start, int no)
        {
            int end = (start + no - 1);
            return RPD.Questionnaire.FilterByPublish(isPublish, start, end).Select(Convert.ToQuestionnaire).ToList();
        }

        public List<RPC.Questionnaire> FilterBySearch(String keyword)
        {
            return RPD.Questionnaire.FilterBySearch(keyword).Select(Convert.ToQuestionnaire).ToList();

        }

       

        public long Count()
        {

            return RPD.Questionnaire.Count();
        }


        public long Count(bool isPublish)
        {

            return RPD.Questionnaire.Count(isPublish);
        }
    }
}
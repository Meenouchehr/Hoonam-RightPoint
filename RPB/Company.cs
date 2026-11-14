using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Company : RPC.Common
    {


        public void Save(RPC.Company item)
        {
            if (item.ID == 0)
                Insert(item);
            else
                Update(item);
        }

        protected void Insert(RPC.Company item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    long id = RPD.Company.Insert(Convert.ToCompany(item));
                    scope.Complete();

                    OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Success });
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Fail });
            }
        }
        protected void Update(RPC.Company item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    RPD.Profile.Update(Convert.ToProfile(item));
                    bool chk = RPD.Company.Update(Convert.ToCompany(item));
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

                    bool chk = RPD.Company.Delete(id);
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

        public void Delete(List<RPC.Company> list)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.Company item in list)
                    {
                        RPD.Company.Delete(item.ID);
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

        public RPC.Company FindByID(long id)
        {
            return Convert.ToCompany(RPD.Company.FindByID(id));
        }
        public List<RPC.Company> All()
        {
            return RPD.Company.All().Select(Convert.ToCompany).ToList();

        }


        public void Publish(List<RPC.Company> list, bool publish)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.Company item in list)
                    {
                        RPD.Company.Publish(item.ID, publish);
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
        public List<RPC.Company> FilterByPublish(bool isPublish)
        {
            return RPD.Company.FilterByPublish(isPublish).Select(Convert.ToCompany).ToList();
        }

        public List<RPC.Company> FilterByPublish(bool isPublish, int start, int no)
        {
            int end = (start + no - 1);
            return RPD.Company.FilterByPublish(isPublish, start, end).Select(Convert.ToCompany).ToList();
        }

        public List<RPC.Company> FilterBySearch(String keyword)
        {
            return RPD.Company.FilterBySearch(keyword).Select(Convert.ToCompany).ToList();

        }




        public List<RPC.Company> SelectRange(int start, int no)
        {


            int end = (start + no - 1);
            return RPD.Company.SelectRange(start, end).Select(Convert.ToCompany).ToList();
        }

        public long Count()
        {

            return RPD.Company.Count();
        }
    }
}

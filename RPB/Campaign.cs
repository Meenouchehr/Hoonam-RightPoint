using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Campaign : RPC.Common
    {


        public void Save(RPC.Campaign item)
        {
            if (item.ID == 0)
                Insert(item);
            else
                Update(item);
        }

        protected void Insert(RPC.Campaign item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    long id = RPD.Campaign.Insert(Convert.ToCampaign(item));
                    scope.Complete();

                    OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Success });
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Fail });
            }
        }
        protected void Update(RPC.Campaign item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    RPD.CampaignAccess.DeleteByCampaign(item.ID);
                    bool chk = RPD.Campaign.Update(Convert.ToCampaign(item));
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

                    bool chk = RPD.Campaign.Delete(id);
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

        public void Delete(List<RPC.Campaign> list)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.Campaign item in list)
                    {
                        RPD.CampaignAccess.DeleteByCampaign(item.ID);
                        RPD.Campaign.Delete(item.ID);
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
        public RPC.Campaign FindByID(long id)
        {
            RPC.Campaign obj = Convert.ToCampaign(RPD.Campaign.FindByID(id));
            obj.AccessList = RPD.CampaignAccess.FindByCampaign(id).Select(Convert.ToCampaignAccess).ToList();
            return obj;
        }
        public List<RPC.Campaign> All()
        {
            return RPD.Campaign.All().Select(Convert.ToCampaign).ToList();

        }

        public void Publish(List<RPC.Campaign> list, bool publish)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.Campaign item in list)
                    {
                        RPD.Campaign.Publish(item.ID, publish);
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
        public List<RPC.Campaign> FilterByPublish(bool isPublish)
        {
            return RPD.Campaign.FilterByPublish(isPublish).Select(Convert.ToCampaign).ToList();
        }

        public List<RPC.Campaign> FilterByPublish(bool isPublish, int start, int no)
        {
            int end = (start + no - 1);
            return RPD.Campaign.FilterByPublish(isPublish, start, end).Select(Convert.ToCampaign).ToList();
        }

        public List<RPC.Campaign> FilterBySearch(String keyword)
        {
            return RPD.Campaign.FilterBySearch(keyword).Select(Convert.ToCampaign).ToList();

        }



        public List<RPC.Campaign> SelectRange(int start, int no)
        {


            int end = (start + no - 1);
            return RPD.Campaign.SelectRange(start, end).Select(Convert.ToCampaign).ToList();
        }

        public long Count()
        {

            return RPD.Campaign.Count();
        }


        public List<RPC.Campaign> FindByCompany(long companyID)
        {
            return RPD.Campaign.FindByCompany(companyID).Select(Convert.ToCampaign).ToList();
        }

        public List<RPC.CampaignAccess> FilterByUser(long userID)
        {
            return RPD.CampaignAccess.FindByUser(userID).Select(Convert.ToCampaignAccess).ToList();
        }
    }
}

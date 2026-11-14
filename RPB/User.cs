using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class User : RPC.Common
    {
        public bool IsAthenticate(String user, String pass)
        {
            try
            {
                if (RPD.User.IsAthenticate(user, pass))
                {
                   
                    RPC.Common.CurrentUser.CampaignAccessList = RPD.CampaignAccess.FindByUser(RPC.Common.CurrentUser.ID).Select(Convert.ToCampaignAccess).ToList();
                    return true;
                }
                
            }
            catch (Exception ex){  }
            return false;
        }


        public void Save(RPC.User item)
        {
            if (item.ID == 0)
                Insert(item);
            else
                Update(item);
        }
        protected void Insert(RPC.User item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    long id = RPD.User.Insert(Convert.ToUser(item));


                    scope.Complete();

                    OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Success });
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Fail });
            }
        }
        protected void Update(RPC.User item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    RPD.CampaignAccess.DeleteByUser(item.ID);
                    RPD.Profile.Update(Convert.ToProfile(item));
                    bool chk = RPD.User.Update(Convert.ToUser(item));
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

                    bool chk = RPD.User.Delete(id);
                    RPD.Profile.Delete(id);
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

        public void Publish(List<RPC.User> list,bool publish)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.User item in list)
                    {
                        RPD.User.Publish(item.ID, publish);
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

        public bool Firstvisit(long inputID, bool status)
        {
            try
            {
                return RPD.User.Firstvisit(inputID, status);
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public void Delete(List<RPC.User> list)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.User item in list)
                    {
                        RPD.User.Delete(item.ID);
                       // RPD.Profile.Delete(item.ID);
                       
                    }
                    scope.Complete();
                    OnChange(new RPC.Result() { RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.Success });

                }
            }
            catch (Exception ex)
            {

                OnChange(new RPC.Result() { RetOperation = RPC.Operation.Delete, RetUpshot = RPC.Upshot.Fail });
            }
        }
        public RPC.User FindByID(long id)
        {
            RPC.User obj = Convert.ToUser(RPD.User.FindByID(id));
            obj.CampaignAccessList = RPD.CampaignAccess.FindByUser(id).Select(Convert.ToCampaignAccess).ToList();
            return obj;
        }

        public List<RPC.User> FindByCompany(long companyID)
        {
            return RPD.User.FindByCompany(companyID).Select(Convert.ToUser).ToList();
        }
        public List<RPC.User> All()
        {
            return RPD.User.All().Select(Convert.ToUser).ToList();

        }

        public List<RPC.User> FilterByPublish(bool isPublish)
        {
            return RPD.User.FilterByPublish(isPublish).Select(Convert.ToUser).ToList();
        }

        public List<RPC.User> FilterByPublish(bool isPublish, int start, int no)
        {
            int end = (start + no - 1);
            return RPD.User.FilterByPublish(isPublish, start, end).Select(Convert.ToUser).ToList();
        }

        public List<RPC.User> FilterBySearch(String keyword)
        {
            return RPD.User.FilterBySearch(keyword).Select(Convert.ToUser).ToList();

        }



        public List<RPC.User> SelectRange(int start, int no)
        {

            int end = (start + no - 1);
            return RPD.User.SelectRange(start, end).Select(Convert.ToUser).ToList();
        }
        public long Count()
        {

            return RPD.User.Count();
        }
    }
}

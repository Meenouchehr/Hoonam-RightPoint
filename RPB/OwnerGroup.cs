using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class OwnerGroup : RPC.Common
    {
        public void Insert(RPC.OwnerGroup item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    long id = RPD.OwnerGroup.Insert(Convert.ToOwnerGroup(item));
                    scope.Complete();

                    OnChange(new RPC.Result() { RetID=id,RetOperation=RPC.Operation.Insert,RetUpshot=RPC.Upshot.Success});
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.NotFound });
            }
        }
        public void Update(RPC.OwnerGroup item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    bool chk = RPD.OwnerGroup.Update(Convert.ToOwnerGroup(item));
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

                    bool chk = RPD.OwnerGroup.Delete(id);
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
        public RPC.OwnerGroup FindByID(long id)
        {
            return Convert.ToOwnerGroup(RPD.OwnerGroup.FindByID(id));
        }
        public List<RPC.OwnerGroup> All()
        {
            return RPD.OwnerGroup.All().Select(Convert.ToOwnerGroup).ToList();

        }
    }
}

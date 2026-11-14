using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Lottary : RPC.Common
    {
        public void Insert(RPC.Lottary item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    if ((item.Category!=null) && (item.Category==0))
                    {
                        item.Category = RPD.LCategory.Insert(Convert.ToLCategory(item));
                    }
                    
                    long id = RPD.Lottary.Insert(Convert.ToLottary(item));
                    scope.Complete();

                    OnChange(new RPC.Result() { RetID=id,RetOperation=RPC.Operation.Insert,RetUpshot=RPC.Upshot.Success});
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.NotFound });
            }
        }
        public void Update(RPC.Lottary item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    bool chk = RPD.Lottary.Update(Convert.ToLottary(item));
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

                    bool chk = RPD.Lottary.Delete(id);
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
        public RPC.Lottary FindByID(long id)
        {
            return Convert.ToLottary(RPD.Lottary.FindByID(id));
        }


        public List<RPC.Lottary> FilterByMedia(byte media)
        {
            return RPD.Lottary.FilterByMedia(media).Select(Convert.ToLottary).ToList();
        }

        public List<RPC.Lottary> All()
        {
            return RPD.Lottary.All().Select(Convert.ToLottary).ToList();

        }
    }
}

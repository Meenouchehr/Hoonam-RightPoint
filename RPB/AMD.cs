using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPB
{
    public class AMD : RPC.Common
    {
        public void Insert(RPC.AMD item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    long id = RPD.AMD.Insert(Convert.ToAMD(item));
                    scope.Complete();

                    OnChange(new RPC.Result() { RetID=id,RetOperation=RPC.Operation.Insert,RetUpshot=RPC.Upshot.Success});
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Fail });
            }
        }
        public void Update(RPC.AMD item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    bool chk = RPD.AMD.Update(Convert.ToAMD(item));
                    scope.Complete();
                    if (chk)
                    {
                         OnChange(new RPC.Result() { RetID = item.ID, RetOperation = RPC.Operation.Update, RetUpshot = RPC.Upshot.Success });
                    }
                    else
                    {
                        OnChange(new RPC.Result() { RetID = item.ID, RetOperation = RPC.Operation.Update, RetUpshot = RPC.Upshot.NotFound });
                    }
                    scope.Complete();
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
                    bool chk = RPD.AMD.Delete(id);
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
        public RPC.AMD FindByID(long id)
        {
            return Convert.ToAMD(RPD.AMD.FindByID(id));
        }

        public RPC.AMD FindByMediaMonth(byte media,byte month)
        {
            return Convert.ToAMD(RPD.AMD.FindByMediaMonth(media,month));
        }

        public List<RPC.AMD> FilterByMedia(byte media)
        {
            return RPD.AMD.FilterByMedia(media).Select(Convert.ToAMD).ToList();
        }
        public List<RPC.AMD> All()
        {
            return RPD.AMD.All().Select(Convert.ToAMD).ToList();

        }
    }
}

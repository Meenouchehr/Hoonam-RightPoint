using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Position : RPC.Common
    {
        public void Insert(RPC.Position item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    long id = RPD.Position.Insert(Convert.ToPosition(item));
                    scope.Complete();

                    OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Success });
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Fail });
            }
        }
        public void Update(RPC.Position item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    bool chk = RPD.Position.Update(Convert.ToPosition(item));
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
                    bool chk = RPD.Position.Delete(id);
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
        public RPC.Position FindByID(long id)
        {
            return Convert.ToPosition(RPD.Position.FindByID(id));
        }

        public List<RPC.Position> FilterByMedia(byte media)
        {
            return RPD.Position.FilterByMedia(media).Select(Convert.ToPosition).ToList();
        }
        public List<RPC.Position> All()
        {
            return RPD.Position.All().Select(Convert.ToPosition).ToList();

        }
    }
}
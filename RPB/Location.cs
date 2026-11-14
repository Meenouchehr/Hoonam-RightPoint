using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Location : RPC.Common
    {
        public void Insert(RPC.Location item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    long id = RPD.Location.Insert(Convert.ToLocation(item));
                    scope.Complete();

                    OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Success });
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Fail });
            }
        }
        public void Update(RPC.Location item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    bool chk = RPD.Location.Update(Convert.ToLocation(item));
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
                    bool chk = RPD.Location.Delete(id);
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
        public RPC.Location FindByID(long id)
        {
            return Convert.ToLocation(RPD.Location.FindByID(id));
        }

        public RPC.Location FindByMedia(byte media,byte loc)
        {
            return Convert.ToLocation(RPD.Location.FindByMedia(media,loc));
        }

        public List<RPC.Location> FilterByMedia(byte media)
        {
            return RPD.Location.FilterByMedia(media).Select(Convert.ToLocation).ToList();
        }
        public List<RPC.Location> All()
        {
            return RPD.Location.All().Select(Convert.ToLocation).ToList();

        }
    }
}
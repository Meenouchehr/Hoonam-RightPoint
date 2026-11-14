using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPB
{
    public class Channel : RPC.Common
    {

        public void Save(RPC.Channel item)
        {
            if (item.ID == 0)
                Insert(item);
            else
                Update(item);
        }
        protected void Insert(RPC.Channel item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    long id = RPD.Channel.Insert(Convert.ToChannel(item));
                    scope.Complete();

                    OnChange(new RPC.Result() { RetID = id, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Success });
                }
            }
            catch
            {

                OnChange(new RPC.Result() { RetID = 0, RetOperation = RPC.Operation.Insert, RetUpshot = RPC.Upshot.Fail });
            }
        }
        protected void Update(RPC.Channel item)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    bool chk = RPD.Channel.Update(Convert.ToChannel(item));
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

                    bool chk = RPD.Channel.Delete(id);
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

        public void Delete(List<RPC.Channel> list)
        {
            try
            {
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {

                    foreach (RPC.Channel item in list)
                    {
                        RPD.Channel.Delete(item.ID);
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

        public RPC.Channel FindByID(long id)
        {
            return Convert.ToChannel(RPD.Channel.FindByID(id));
        }

        public List<RPC.Channel> FilterByMedia(byte media)
        {
            return RPD.Channel.FilterByMedia(media).Select(Convert.ToChannel).ToList();
        }

        public List<RPC.Channel> FilterByMedia_Broadcast(byte media, byte broadcast)
        {
            return RPD.Channel.FilterByMedia_Broadcast(media,broadcast).Select(Convert.ToChannel).ToList();
        }


        public List<RPC.Channel> All()
        {
            return RPD.Channel.All().Select(Convert.ToChannel).ToList();

        }

        public List<RPC.Channel> FilterBySearch(String keyword)
        {
            return RPD.Channel.FilterBySearch(keyword).Select(Convert.ToChannel).ToList();

        }


        public List<RPC.Channel> SelectRange(int start, int no)
        {


            int end = (start + no - 1);
            return RPD.Channel.SelectRange(start, end).Select(Convert.ToChannel).ToList();
        }

        public long Count()
        {

            return RPD.Channel.Count();
        }

    }
}

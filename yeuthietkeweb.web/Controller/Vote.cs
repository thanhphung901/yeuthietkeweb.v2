using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;

namespace Controller
{
    public class Vote
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        public List<VOTE> Load_vote()
        {
            var list = db.VOTEs.Where(n => n.VOTE_ACTIVE == 1&&n.VOTE_RANK==1).OrderByDescending(n=>n.VOTE_OID).Take(1).ToList();
            return list;
        }
        public IQueryable<VOTE> Votecap2(object vtid)
        {
            int id = Utils.CIntDef(vtid);
            var list = db.VOTEs.Where(n => n.VOTE_ACTIVE == 1 && n.VOTE_PARENT_ID==id);
            return list.ToList().Count>0 ? list : null;
        }
        public bool Update_vote(int id,string ip_address)
        {
            var list = db.VOTEs.Where(n => n.VOTE_ACTIVE == 1&&n.VOTE_OID==id); 
            foreach(var i in list)
            {
                if (ip_address != i.VOTE_IP_ADDRESS)
                {
                    i.VOTE_COUNT++;
                    i.VOTE_IP_ADDRESS = ip_address;
                    db.SubmitChanges();
                    return true;
                }
            }
            return false;
        }
       
    }
}

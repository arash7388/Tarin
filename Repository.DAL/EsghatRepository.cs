﻿using Repository.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class EsghatRepository : BaseRepository<Esghat>
    {
        public List<EsghatHelper> GetAllWithRelations()
        {
            var result = from r in DBContext.Esghats
                         join op in DBContext.Users on r.OperatorId equals op.Id
                         join u in DBContext.Users on r.InsertedUserId equals u.Id
                         select new EsghatHelper
                         {
                             Id = r.Id,
                             Desc = r.Desc,
                             InsertDateTime = r.InsertDateTime,
                             InsertedUserId = r.InsertedUserId,
                             OperatorId = r.OperatorId,
                             OpName = op.FriendlyName,
                         };
            var resultList = result.ToList();

            for (int i = 0; i < resultList.Count; i++)
            {
                var id = resultList[i].Id;
                var allReasons = DBContext.ReworkReasons.ToList();

                resultList[i].EsghatDetails = DBContext.EsghatDetails.Where(a => a.EsghatId == id).ToList();

                foreach(var d in resultList[i].EsghatDetails)
                {
                    d.ReworkReason = allReasons.FirstOrDefault(a => a.Id == d.ReworkReasonId);
                }

                resultList[i].ACode =  string.Join(",", resultList[i].EsghatDetails?.Select(a => a.ACode));
                resultList[i].ReasonName = string.Join(",", resultList[i].EsghatDetails?.Select(a => a.ReworkReason.Name));
            }

            return resultList.ToList();
        }

        public class EsghatHelper : Esghat
        {
            public string OpName { get; set; }
            public string UserName { get; set; }
            public string ReasonName { get; set; }
            public string ACode { get; set; }


        }
    }
}

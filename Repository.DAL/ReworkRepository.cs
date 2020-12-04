﻿using Repository.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class ReworkRepository:BaseRepository<Rework>
    {
        public List<ReworkHelper> GetAllWithRelations()
        {
            var result = from r in DBContext.Reworks
                         join op in DBContext.Users on r.OperatorId equals op.Id
                         join u in DBContext.Users on r.InsertedUserId equals u.Id
                         join reason in DBContext.ReworkReasons on r.ReworkReasonId equals reason.Id
                         select new ReworkHelper
                         {
                             Id = r.Id,
                             ACode = r.ACode,
                             Desc = r.Desc,
                             InsertDateTime = r.InsertDateTime,
                             InsertedUserId = r.InsertedUserId,
                             OperatorId = r.OperatorId,
                             OpName = op.FriendlyName,
                             ReasonName = reason.Name,
                             ReworkReasonId = r.ReworkReasonId
                         };

            return result.ToList();
        }

        public class ReworkHelper : Rework
        {
            public string OpName { get; set; }
            public string UserName { get; set; }
            public string ReasonName { get; set; }


        }
    }
}
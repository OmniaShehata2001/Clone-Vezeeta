﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Dtos.Result
{
    public class ResultDataList<TEntity>
    {
        public List<TEntity> Entities { get; set; }
        public int Count { get; set; }
        public ResultDataList()
        {
            Entities = new List<TEntity>();
        }
    }
}

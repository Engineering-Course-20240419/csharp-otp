﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp_otp;

namespace csharp_otp_2019
{
    public class QueryBudget
    {
        private readonly IBudgetRepo _budgetRepo;

        public QueryBudget(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public int query(DateTime start, DateTime end)
        {
            if (_budgetRepo.GetAll().Count == 0)
            {
                return 0;
            }
            return (end - start).Days + 1;
        }
    }
}

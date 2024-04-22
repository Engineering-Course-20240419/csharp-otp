using System;
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
            return _budgetRepo.GetAll().Sum(budget => 
                getOverlappingDayCount(start, end, budget));
        }

        private int getOverlappingDayCount(DateTime start, DateTime end, Budget budget)
        {

            var realStart = start > budget.GetStart() ? start : budget.GetStart();
            var realEnd = end < budget.GetEnd() ? end : budget.GetEnd();
            if (start > budget.GetEnd() || end < budget.GetStart())
            {
                return 0;
            }
            else
            {
                return (realEnd - realStart).Days + 1;
            }
        }
    }
}

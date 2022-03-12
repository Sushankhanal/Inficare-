using InficareSushan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InficareSushan.Repository
{
     public interface IBankList
    {
        Task<IEnumerable<BankList>> GetBankList();
    }
}

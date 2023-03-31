using System;
using salary.DB;
using salary.Models;
using shraredclasses.Commands;

namespace salary.Logic
{
	public class SalaryService
	{
        public async Task AddBankDetails(CreateBankDetailsRequest createBankDetailsRequest)
        {
           
            var bankDetail = new BankDetail()
            {
                BankAccount = createBankDetailsRequest.BankAccount,
                BankName = createBankDetailsRequest.BankName,
                BankNumber = createBankDetailsRequest.BankNumber,
                EmployeeId = createBankDetailsRequest.EmployeeId
            };

            using(BankDetailContext db = new BankDetailContext())
            {
                await db.AddAsync(bankDetail);
                await db.SaveChangesAsync();
            }
        }
    }
}


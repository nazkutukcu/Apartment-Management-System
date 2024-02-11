using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.BuildingMonthlyBills;
using SiteYonetimSistemi.API.Models.MonthlyBills.DTOs;
using SiteYonetimSistemi.API.Models.MonthlyDues;
using SiteYonetimSistemi.API.Models.MonthlyDues.DTOs;
using SiteYonetimSistemi.API.Models.Payments;
using SiteYonetimSistemi.API.Models.UnitOfWorks;

namespace SiteYonetimSistemi.API.Models.Admin
{
    public class AdminService(IMonthlyDueRepository monthlyDueRepository, IApartmentRepository apartmentRepository,
        IMonthlyBillRepository monthlyBillRepository, IUnitOfWork unitOfWork) : IAdminService
    {
        public ResponseDto<int> AssignMonthlyDuesToApartment(MontlyDueAddRequestDto request)
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                var apartment = apartmentRepository.GetApartmentById(request.ApartmentId);
                if (apartment == null || apartment.IsFull == false)
                {
                    return ResponseDto<int>.Fail("Belirtilen daire bulunamadı veya dolu değil.");
                }

                var monthlyDue = new MonthlyDue
                {
                    ApartmentId = request.ApartmentId,
                    Amount = request.Amount,
                    DueDate = new DateTime(DateTime.Today.Year, request.Month, 1).AddMonths(1), 
                    IsPaid = false,
                    Month = request.Month
                };
                monthlyDueRepository.AddMonthlyDue(monthlyDue);
                unitOfWork.Commit();

                apartmentRepository.Update(apartment);
                unitOfWork.Commit();

                transaction.Commit();
                return ResponseDto<int>.Success(monthlyDue.Id);
            }              
        }

        public ResponseDto<string> AssignMonthlyDuesToAll(MontlyDuesToAllAddRequestDto request)
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                var apartments = apartmentRepository.GetAll().Where(a => a.IsFull== true);

                foreach (var apartment in apartments)
                {
                    var monthlyDue = new MonthlyDue
                    {
                        ApartmentId = apartment.Id,
                        Amount = request.Amount,
                        DueDate = new DateTime(DateTime.Today.Year, request.Month, 1).AddMonths(1),
                        IsPaid = false,
                        Month = request.Month
                    };
                    monthlyDueRepository.AddMonthlyDue(monthlyDue);
                    apartmentRepository.Update(apartment);
                }               
                unitOfWork.Commit();
                transaction.Commit();
                return ResponseDto<string>.Success("Tüm dolu dairelere aidat başarıyla atandı.");
            }
        }              
        
        public ResponseDto<string> AddMonthlyBillToAllApartments(MonthlyBillToAllAddRequestDto request)
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                var apartments = apartmentRepository.GetAll().Where(a => a.IsFull == true);

                foreach (var apartment in apartments)
                {
                    var bill = new MonthlyBill
                    {
                        BillType = request.BillType,
                        Amount = request.Amount,
                        DueDate = new DateTime(DateTime.Today.Year, request.Month, 1).AddMonths(1),
                        IsPaid = false,
                        ApartmentId = apartment.Id,
                        Month = request.Month
                    };
                    monthlyBillRepository.AddBill(bill);
                    apartmentRepository.Update(apartment);
                   
                }
                unitOfWork.Commit();

                transaction.Commit();
                return ResponseDto<string>.Success("Tüm dolu dairelere faturalar başarıyla atandı.");
            }               
        }

        public ResponseDto<decimal> GetMonthlyDebtForApartment(int apartmentId, int month)
        {
            var apartment = apartmentRepository.GetById(apartmentId);
            if (apartment == null)
            {
                return ResponseDto<decimal>.Fail("Belirtilen ID'ye sahip daire bulunamadı");
            }

            var monthlyDues = monthlyDueRepository.GetMonthlyDuesForApartment(apartmentId)
                                                   .Where(md => md.Month == month && !md.IsPaid);
            var monthlyBills = monthlyBillRepository.GetBillsForApartment(apartmentId)
                                                     .Where(mb => mb.Month == month && !mb.IsPaid);

            decimal totalDebt = monthlyDues.Sum(md => md.Amount) + monthlyBills.Sum(mb => mb.Amount);
            return ResponseDto<decimal>.Success(totalDebt);
        }

        public ResponseDto<decimal> GetYearlyDebtForApartment(int apartmentId)
        {
            var apartment = apartmentRepository.GetById(apartmentId);
            if (apartment == null)
            {
                return ResponseDto<decimal>.Fail("Belirtilen ID'ye sahip daire bulunamadı");
            }

            var monthlyDues = monthlyDueRepository.GetMonthlyDuesForApartment(apartmentId)
                                                   .Where(md => !md.IsPaid); 
            var monthlyBills = monthlyBillRepository.GetBillsForApartment(apartmentId)
                                                     .Where(mb => !mb.IsPaid); 

            decimal totalDebt = monthlyDues.Sum(md => md.Amount) + monthlyBills.Sum(mb => mb.Amount);
            return ResponseDto<decimal>.Success(totalDebt);
        }

    }
}
    



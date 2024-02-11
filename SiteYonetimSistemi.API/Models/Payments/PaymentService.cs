using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SiteYonetimSistemi.API.Models.Apartments;
using SiteYonetimSistemi.API.Models.Apartments.DTOs;
using SiteYonetimSistemi.API.Models.BuildingMonthlyBills;
using SiteYonetimSistemi.API.Models.Enums;
using SiteYonetimSistemi.API.Models.MonthlyDues;
using SiteYonetimSistemi.API.Models.Payments.DTOs;
using SiteYonetimSistemi.API.Models.UnitOfWorks;
using SiteYonetimSistemi.API.Models.Users;

namespace SiteYonetimSistemi.API.Models.Payments
{
    public class PaymentService(IPaymentRepository paymentRepository, IApartmentRepository apartmentRepository,
        IMonthlyDueRepository monthlyDueRepository, IMonthlyBillRepository monthlyBillRepository, IUnitOfWork unitOfWork, IMapper mapper) : IPaymentService
    {
     
        public ResponseDto<int> MakePaymentForMonthlyDue(DuePaymentMakeDtoRequest request)
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                var apartment = apartmentRepository.GetById(request.ApartmentId);
                if (apartment == null)
                {
                    return ResponseDto<int>.Fail("Belirtilen daire bulunamadı.");
                }

                var monthlyDue = apartment.MonthlyDues.FirstOrDefault(d => d.Month == request.Month && !d.IsPaid);
                if (monthlyDue == null)
                {
                    return ResponseDto<int>.Fail($"Belirtilen ay için ödenmemiş aidat bulunamadı.");
                }

                var payment = new Payment
                {
                    Amount = monthlyDue.Amount,
                    PaymentDate = DateTime.Now,
                    PaymentType = request.PaymentType,
                    PaymentFor = PaymentFor.Aidat, 
                    ApartmentId = request.ApartmentId,
                    Month = request.Month,
                };

                paymentRepository.Add(payment);
                unitOfWork.Commit();

                monthlyDue.IsPaid = true;
                monthlyDueRepository.Update(monthlyDue);
                unitOfWork.Commit();

                transaction.Commit();

                return ResponseDto<int>.Success(payment.Id);
            }              
        }

    
        public ResponseDto<int> MakePaymentForMonthlyBill(BillPaymentMakeDtoRequest request)
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                var apartment = apartmentRepository.GetById(request.ApartmentId);
                if (apartment == null)
                {
                    return ResponseDto<int>.Fail("Belirtilen daire bulunamadı.");
                }

                var monthlyBill = apartment.MonthlyBills.FirstOrDefault(b => b.Month == request.Month && !b.IsPaid && b.BillType == request.BillType);
                if (monthlyBill == null)
                {
                    return ResponseDto<int>.Fail("Belirtilen ay için ödenmemiş fatura bulunamadı..");
                }

                var payment = new Payment
                {
                    Amount = monthlyBill.Amount,
                    PaymentDate = DateTime.Now,
                    PaymentType = request.PaymentType,
                    PaymentFor = PaymentFor.Fatura,
                    ApartmentId = request.ApartmentId,
                    Month = request.Month,
                };

                paymentRepository.Add(payment);
                unitOfWork.Commit();

                monthlyBill.IsPaid = true;
                monthlyBillRepository.Update(monthlyBill);
                unitOfWork.Commit();

                transaction.Commit();

                return ResponseDto<int>.Success(payment.Id);
            }              
        }

        
        public ResponseDto<List<PaymentDto>> GetPaymentsForApartment(int apartmentId)
        {
            var apartment = apartmentRepository.GetApartmentById(apartmentId);
            if (apartment == null)
            {
                return ResponseDto<List<PaymentDto>>.Fail("Belirtilen daire bulunamadı.");
            }

            var payments = paymentRepository.GetPaymentsByApartmentId(apartmentId);
            if (payments == null || !payments.Any())
            {
                return ResponseDto<List<PaymentDto>>.Fail("Daireye ait ödemeler bulunamadı.");
            }

            var paymentDtos = mapper.Map<List<PaymentDto>>(payments);
            return ResponseDto<List<PaymentDto>>.Success(paymentDtos);
        }


        // Finds apartments with regular payments, defined as making payments for three consecutive months.
        public ResponseDto<List<ApartmentDto>> GetApartmentsWithRegularPayments()
        {
            var apartments = apartmentRepository.GetAll();
            if (apartments == null || !apartments.Any())
            {
                return ResponseDto<List<ApartmentDto>>.Fail("Daireler bulunamadı.");
            }

            var apartmentsWithRegularPayments = new List<ApartmentDto>();
            foreach (var apartment in apartments)
            {
                var payments = paymentRepository.GetPaymentsByApartmentId(apartment.Id);
                if (payments != null && payments.Any())
                {
                    var sortedPayments = payments.OrderBy(p => p.PaymentDate); // Sort payments by date
                    var consecutivePaymentsCount = 0;
                    var lastPaymentMonth = 0;

                    foreach (var payment in sortedPayments)
                    {
                        var paymentMonth = payment.Month;
                        if (paymentMonth == lastPaymentMonth + 1) // Check for consecutive months
                        {
                            consecutivePaymentsCount++;
                            if (consecutivePaymentsCount >= 3) 
                            {
                                apartmentsWithRegularPayments.Add(mapper.Map<ApartmentDto>(apartment));
                                break; 
                            }
                        }
                        else
                        {
                            consecutivePaymentsCount = 1; // Reset consecutive payment series
                        }

                        lastPaymentMonth = paymentMonth;
                    }
                }
            }

            if (apartmentsWithRegularPayments.Any())
            {
                return ResponseDto<List<ApartmentDto>>.Success(apartmentsWithRegularPayments);
            }
            else
            {
                return ResponseDto<List<ApartmentDto>>.Fail("Düzenli ödeme yapan daire bulunamadı.");
            }
        }

    }
}


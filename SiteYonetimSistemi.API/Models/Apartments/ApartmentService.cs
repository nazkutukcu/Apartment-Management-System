using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SiteYonetimSistemi.API.Models.Apartments.DTOs;
using SiteYonetimSistemi.API.Models.Users.DTOs;
using SiteYonetimSistemi.API.Models.Users;
using SiteYonetimSistemi.API.Models.BuildingMonthlyBills;
using SiteYonetimSistemi.API.Models.MonthlyDues;

namespace SiteYonetimSistemi.API.Models.Apartments
{
    public class ApartmentService(IApartmentRepository apartmentRepository, IMapper mapper) : IApartmentService
    {
        public ResponseDto<int> AddApartment(ApartmentSaveDtoRequest request)
        {
            var apartment = new Apartment
            {
                Floor = request.Floor,
                Block = request.Block,
                ApartmentNo = request.ApartmentNo,
                ResidenceStatus = request.ResidenceStatus,
                Type = request.Type,
            };
            apartmentRepository.Add(apartment);
          
            return ResponseDto<int>.Success(apartment.Id);
        }
        public ResponseDto<List<ApartmentDto>> GetAll()
        {
            var apartments = apartmentRepository.GetAll();
            var apartmentDtos = mapper.Map<List<ApartmentDto>>(apartments);
            return ResponseDto<List<ApartmentDto>>.Success(apartmentDtos);
        }

        // Retrieves the monthly unpaid bills and dues for a specific apartment.
        public ResponseDto<UnpaidBillsAndDuesForMonthDto> GetUnpaidBillsAndDuesForMonth(int apartmentId, int month)
        {
            var apartment = apartmentRepository.GetById(apartmentId);
            if (apartment == null)
            {
                return ResponseDto<UnpaidBillsAndDuesForMonthDto>.Fail("Belirtilen daire bulunamadı.");
            }

            var unpaidDues = apartment.MonthlyDues
            .Where(d => d.Month == month && !d.IsPaid)
            .Select(d => new MonthlyDue { Id = d.Id, Amount = d.Amount, DueDate = d.DueDate, Month = d.Month, ApartmentId = d.ApartmentId })
            .ToList();

            var unpaidBills = apartment.MonthlyBills
            .Where(b => b.Month == month && !b.IsPaid)
            .Select(b => new MonthlyBill { Id = b.Id, BillType = b.BillType, Amount = b.Amount, DueDate = b.DueDate, Month = b.Month, ApartmentId = b.ApartmentId })
            .ToList();

            var data = new UnpaidBillsAndDuesForMonthDto { UnpaidBills = unpaidBills, UnpaidDues = unpaidDues };
            return ResponseDto<UnpaidBillsAndDuesForMonthDto>.Success(data);
        }

        // Retrieves the monthly unpaid bills and dues for all apartments in the system.
        public ResponseDto<AllApartmentsUnpaidBillsAndDuesDto> GetAllApartmentsUnpaidBillsAndDuesForMonth(int month)
        {
            var allApartments = apartmentRepository.GetAll(); 
            var allApartmentsUnpaidBillsAndDues = new AllApartmentsUnpaidBillsAndDuesDto();

            foreach (var apartment in allApartments)
            {
                var unpaidDues = apartment.MonthlyDues
                    .Where(d => d.Month == month && !d.IsPaid)
                    .Select(d => new MonthlyDue { Id = d.Id, Amount = d.Amount, DueDate = d.DueDate, Month = d.Month, ApartmentId = d.ApartmentId })
                    .ToList();

                var unpaidBills = apartment.MonthlyBills
                    .Where(b => b.Month == month && !b.IsPaid)
                    .Select(b => new MonthlyBill { Id = b.Id, BillType = b.BillType, Amount = b.Amount, DueDate = b.DueDate, Month = b.Month, ApartmentId = b.ApartmentId })
                    .ToList();

                var apartmentUnpaidBillsAndDues = new UnpaidBillsAndDuesForMonthDto
                {
                    UnpaidBills = unpaidBills,
                    UnpaidDues = unpaidDues
                };

                allApartmentsUnpaidBillsAndDues.Apartments.Add(apartmentUnpaidBillsAndDues);
            }
            return ResponseDto<AllApartmentsUnpaidBillsAndDuesDto>.Success(allApartmentsUnpaidBillsAndDues);
        }

        // Retrieves all bills and dues (paid and unpaid) for a specific apartment and month.
        public ResponseDto<BillsAndDuesForMonthDto> GetAllBillsAndDuesForMonth(int apartmentId, int month)
        {
            var apartment = apartmentRepository.GetById(apartmentId);
            if (apartment == null)
            {
                return ResponseDto<BillsAndDuesForMonthDto>.Fail("Belirtilen daire bulunamadı.");
            }

            var data = new BillsAndDuesForMonthDto
            {
                PaidDues = apartment.MonthlyDues
                                .Where(d => d.Month == month && d.IsPaid)
                                .Select(d => new MonthlyDue { Id = d.Id, Amount = d.Amount, DueDate = d.DueDate, Month = d.Month, ApartmentId = d.ApartmentId })
                                .ToList(),
                UnpaidDues = apartment.MonthlyDues
                                .Where(d => d.Month == month && !d.IsPaid)
                                .Select(d => new MonthlyDue { Id = d.Id, Amount = d.Amount, DueDate = d.DueDate, Month = d.Month, ApartmentId = d.ApartmentId })
                                .ToList(),
                PaidBills = apartment.MonthlyBills
                                .Where(b => b.Month == month && b.IsPaid)
                                .Select(b => new MonthlyBill { Id = b.Id, BillType = b.BillType, Amount = b.Amount, DueDate = b.DueDate, Month = b.Month, ApartmentId = b.ApartmentId })
                                .ToList(),
                UnpaidBills = apartment.MonthlyBills
                                .Where(b => b.Month == month && !b.IsPaid)
                                .Select(b => new MonthlyBill { Id = b.Id, BillType = b.BillType, Amount = b.Amount, DueDate = b.DueDate, Month = b.Month, ApartmentId = b.ApartmentId })
                                .ToList()
            };

            return ResponseDto<BillsAndDuesForMonthDto>.Success(data);
        }

    }
}

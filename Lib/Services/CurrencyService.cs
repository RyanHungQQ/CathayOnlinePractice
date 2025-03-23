using Common.Enums;
using DAL.DbContexts;
using DAL.Entities;
using Lib.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Models.CathayOnlinePractice.Response;
using Models.CathayOnlinePractice.Resqust;

namespace Lib.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly DbEntities _context;

        public CurrencyService(DbEntities context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CurrencyResponseDto>> GetAllCurrenciesAsync()
        {
            var currencies = await _context.Currency.ToListAsync();
            var currencyDTOs = currencies.Select(currency => new CurrencyResponseDto
            {
                Id = currency.Id,
                Code = currency.Code,
                Name = currency.Name,
                CreateDate = currency.CreateDate,
                ModifyDate = currency.ModifyDate
            }).OrderBy(o => o.Code).ToList();
            return currencyDTOs;
        }

        public async Task<APIResponseDto<CurrencyResponseDto>> GetCurrencyByIdAsync(int id)
        {
            var result = new APIResponseDto<CurrencyResponseDto>();
            var currency = await _context.Currency.FindAsync(id);
            if (currency == null)
            {
                result.ResponseEnum = WebApiEnum.APIResponseEnum.NotFound;
                return result;
            }
            result.ResponseEnum = WebApiEnum.APIResponseEnum.Success;
            result.OutData = await GetSingleCurrency(id);
            return result;
        }
        private async Task<CurrencyResponseDto> GetSingleCurrency(int id)
        {
            var currency = await _context.Currency.FindAsync(id);
            if (currency == null)
            {
                return null;
            }
            return new CurrencyResponseDto
            {
                Id = currency.Id,
                Code = currency.Code,
                Name = currency.Name,
                CreateDate = currency.CreateDate,
                ModifyDate = currency.ModifyDate
            };
        }

        public async Task<APIResponseDto<CurrencyResponseDto>> CreateCurrencyAsync(CurrnecyResqustDto currencyDto)
        {
            var result = new APIResponseDto<CurrencyResponseDto>();

            var exist = await _context.Currency.CountAsync(o => o.Code == currencyDto.Code);
            if (exist > 0)
            {
                result.ResponseEnum = WebApiEnum.APIResponseEnum.DataExist;
                return result;
            }

            var currency = new Currency
            {
                Code = currencyDto.Code,
                Name = currencyDto.Name,
            };
            _context.Currency.Add(currency);
            await _context.SaveChangesAsync();

            result.ResponseEnum = WebApiEnum.APIResponseEnum.Success;
            result.OutData = await GetSingleCurrency(currency.Id);
            return result;
        }

        public async Task<APIResponseDto<CurrencyResponseDto>> UpdateCurrencyAsync(int id, CurrnecyResqustDto currencyDto)
        {
            var result = new APIResponseDto<CurrencyResponseDto>();

            var exist = await _context.Currency.CountAsync(o => o.Code == currencyDto.Code && o.Id != id);
            if (exist > 0)
            {
                result.ResponseEnum = WebApiEnum.APIResponseEnum.DataExist;
                return result;
            }

            var existingCurrency = await _context.Currency.FindAsync(id);
            if (existingCurrency == null)
            {
                result.ResponseEnum = WebApiEnum.APIResponseEnum.NotFound;
                return result;
            }

            existingCurrency.Code = currencyDto.Code;
            existingCurrency.Name = currencyDto.Name;

            _context.Entry(existingCurrency).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            result.ResponseEnum = WebApiEnum.APIResponseEnum.Success;
            result.OutData = await GetSingleCurrency(id);
            return result;
        }

        public async Task<APIResponseDto> DeleteCurrencyAsync(int id)
        {
            var result = new APIResponseDto();
            var currency = await _context.Currency.FindAsync(id);
            if (currency == null)
            {
                result.ResponseEnum = WebApiEnum.APIResponseEnum.NotFound;
                return result;
            }

            _context.Currency.Remove(currency);
            await _context.SaveChangesAsync();
            result.ResponseEnum = WebApiEnum.APIResponseEnum.Success;
            return result;
        }
    }
}

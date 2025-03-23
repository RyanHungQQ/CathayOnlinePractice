using AutoMapper;
using Common.Enums;
using Lib.Interfaces.Repositories;
using Lib.Interfaces.Services;
using Models.CathayOnlinePractice.Response;
using Models.CathayOnlinePractice.Resqust;
using Models.Entities;

namespace Lib.Services
{
    public class CurrencyService : BaseEntityService<Currency>, ICurrencyService
    {

        public CurrencyService(
            IMapper mapper,
            IDbEntities dbEntities,
            IRepository<Currency> repository):base(mapper, dbEntities, repository)
        {
        }

        public async Task<IEnumerable<CurrencyResponseDto>> GetAllCurrenciesAsync()
        {
            var currencies = new List<CurrencyResponseDto>();
            currencies = base.GetAll<CurrencyResponseDto>().OrderBy(o => o.Code).ToList();
            return currencies;
        }

        public async Task<APIResponseDto<CurrencyResponseDto>> GetCurrencyByIdAsync(int id)
        {
            var result = new APIResponseDto<CurrencyResponseDto>();
            var currency = base.GetSingle<CurrencyResponseDto>(o => o.Id == id);
            if (currency == null)
            {
                result.ResponseEnum = WebApiEnum.APIResponseEnum.NotFound;
                return result;
            }
            result.ResponseEnum = WebApiEnum.APIResponseEnum.Success;
            result.OutData = currency;
            return result;
        }

        public async Task<APIResponseDto<CurrencyResponseDto>> CreateCurrencyAsync(CurrnecyResqustDto currencyDto)
        {
            var result = new APIResponseDto<CurrencyResponseDto>();

            var exist = base.GetCount(o => o.Code == currencyDto.Code);
            if (exist > 0)
            {
                result.ResponseEnum = WebApiEnum.APIResponseEnum.DataExist;
                return result;
            }
            var currency = currencyDto.ToEntity();
            Repository.Insert(currency);
            DbEntities.SaveChanges();

            result.ResponseEnum = WebApiEnum.APIResponseEnum.Success;
            result.OutData = base.GetSingle<CurrencyResponseDto>(o => o.Id == currency.Id);
            return result;
        }

        public async Task<APIResponseDto<CurrencyResponseDto>> UpdateCurrencyAsync(int id, CurrnecyResqustDto currencyDto)
        {
            var result = new APIResponseDto<CurrencyResponseDto>();

            var existingCurrency = Repository.GetSingle(o => o.Id == id);
            if (existingCurrency == null)
            {
                result.ResponseEnum = WebApiEnum.APIResponseEnum.NotFound;
                return result;
            }

            var exist = base.GetCount(o => o.Code == currencyDto.Code && o.Id != id);
            if (exist > 0)
            {
                result.ResponseEnum = WebApiEnum.APIResponseEnum.DataExist;
                return result;
            }

            existingCurrency.Code = currencyDto.Code;
            existingCurrency.Name = currencyDto.Name;
            base.Update(existingCurrency);

            result.ResponseEnum = WebApiEnum.APIResponseEnum.Success;
            result.OutData = base.GetSingle<CurrencyResponseDto>(o => o.Id == id);
            return result;
        }

        public async Task<APIResponseDto> DeleteCurrencyAsync(int id)
        {
            var result = new APIResponseDto();
            var existingCurrency = base.GetSingle<CurrencyResponseDto>(o => o.Id == id);
            if (existingCurrency == null)
            {
                result.ResponseEnum = WebApiEnum.APIResponseEnum.NotFound;
                return result;
            }
            base.Delete(o => o.Id == id);
            result.ResponseEnum = WebApiEnum.APIResponseEnum.Success;
            return result;
        }
    }
}

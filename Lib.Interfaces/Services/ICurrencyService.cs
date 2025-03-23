using Models.CathayOnlinePractice.Response;
using Models.CathayOnlinePractice.Resqust;
using Models.Entities;

namespace Lib.Interfaces.Services
{
    public interface ICurrencyService: IBaseEntityService<Currency>
    {
        Task<IEnumerable<CurrencyResponseDto>> GetAllCurrenciesAsync();
        Task<APIResponseDto<CurrencyResponseDto>> GetCurrencyByIdAsync(int id);
        Task<APIResponseDto<CurrencyResponseDto>> CreateCurrencyAsync(CurrnecyResqustDto currency);
        Task<APIResponseDto<CurrencyResponseDto>> UpdateCurrencyAsync(int id, CurrnecyResqustDto currency);
        Task<APIResponseDto> DeleteCurrencyAsync(int id);
    }
}

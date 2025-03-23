using Models.CathayOnlinePractice.Response;
using Models.CathayOnlinePractice.Resqust;

namespace Lib.Services.Interface
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyResponseDto>> GetAllCurrenciesAsync();
        Task<APIResponseDto<CurrencyResponseDto>> GetCurrencyByIdAsync(int id);
        Task<APIResponseDto<CurrencyResponseDto>> CreateCurrencyAsync(CurrnecyResqustDto currency);
        Task<APIResponseDto<CurrencyResponseDto>> UpdateCurrencyAsync(int id, CurrnecyResqustDto currency);
        Task<APIResponseDto> DeleteCurrencyAsync(int id);
    }
}

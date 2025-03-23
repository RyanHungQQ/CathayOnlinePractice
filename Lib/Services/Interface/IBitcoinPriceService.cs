using Models.CathayOnlinePractice.Response;
using Models.Dto;

namespace Lib.Services.Interface
{
    public interface IBitcoinPriceService
    {
        Task<ExchangeRateResponseDto> GetExchangeRateData();
        Task<BitcoinPriceDto> GetBitcoinPriceData();
    }
}

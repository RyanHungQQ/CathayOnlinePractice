using Models.CathayOnlinePractice.Response;
using Models.Dto;

namespace Lib.Interfaces.Services
{
    public interface IBitcoinPriceService
    {
        Task<ExchangeRateResponseDto> GetExchangeRateData();
        Task<BitcoinPriceDto> GetBitcoinPriceData();
    }
}

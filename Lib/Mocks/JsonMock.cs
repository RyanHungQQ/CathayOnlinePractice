﻿namespace Lib.Mocks
{
    public static class JsonMock
    {
        public static string GetBitcoinPriceJson()
        {
            return @"
                    {
                        ""time"": {
                            ""updated"": ""Aug 3, 2022 20:25:00 UTC"",
                            ""updatedISO"": ""2022-08-03T20:25:00+00:00"",
                            ""updateduk"": ""Aug 3, 2022 at 21:25 BST""
                        },
                        ""disclaimer"": ""This data was produced from the CoinDesk Bitcoin Price Index (USD). Non-USD currency data converted using hourly conversion rate from openexchangerates.org"",
                        ""chartName"": ""Bitcoin"",
                        ""bpi"": {
                            ""USD"": {
                                ""code"": ""USD"",
                                ""symbol"": ""$"",
                                ""rate"": ""23,342.0112"",
                                ""description"": ""US Dollar"",
                                ""rate_float"": 23342.0112
                            },
                            ""GBP"": {
                                ""code"": ""GBP"",
                                ""symbol"": ""£"",
                                ""rate"": ""19,504.3978"",
                                ""description"": ""British Pound Sterling"",
                                ""rate_float"": 19504.3978
                            },
                            ""EUR"": {
                                ""code"": ""EUR"",
                                ""symbol"": ""€"",
                                ""rate"": ""22,738.5269"",
                                ""description"": ""Euro"",
                                ""rate_float"": 22738.5269
                            }
                        }
                    }
                    ";
        }
    }
}

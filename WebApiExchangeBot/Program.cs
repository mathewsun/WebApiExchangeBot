using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Objects;

using CryptoExchange.Net.Authentication;

using System;
using System.Threading.Tasks;

//using WebApiExchangeBot.ApiBots.Services;

namespace WebApiExchangeBot
{
    public static class RandomHelper
    {
        public static decimal NextDecimal(this Random rnd, decimal from, decimal to)
        {
            byte fromScale = new System.Data.SqlTypes.SqlDecimal(from).Scale;
            byte toScale = new System.Data.SqlTypes.SqlDecimal(to).Scale;

            byte scale = (byte)(fromScale + toScale);
            if (scale > 28)
                scale = 28;

            decimal r = new decimal(rnd.Next(), rnd.Next(), rnd.Next(), false, scale);
            if (Math.Sign(from) == Math.Sign(to) || from == 0 || to == 0)
                return decimal.Remainder(r, to - from) + from;

            bool getFromNegativeRange = (double)from + rnd.NextDouble() * ((double)to - (double)from) < 0;
            return getFromNegativeRange ? decimal.Remainder(r, -from) + from : decimal.Remainder(r, to);
        }
    }

    internal class Program
    {
        public class PairModel
        {
            public string PairName { get; set; }
            public decimal AmountFrom { get; set; }
            public decimal AmountTo { get; set; }
        }

        private List<PairModel> pairs = new()
        {
            new PairModel()
            {
                PairName = "BTCUSDT",
                AmountFrom = 0.000001m,
                AmountTo = 0.00001m
            },
            new PairModel()
            {
                PairName = "ETHUSDT",
                AmountFrom = 0.00001m,
                AmountTo = 0.0001m
            },
            new PairModel()
            {
                PairName = "DOGEUSDT",
                AmountFrom = 0.1m,
                AmountTo = 0.99m
            },
            new PairModel()
            {
                PairName = "LTCUSDT",
                AmountFrom = 0.001m,
                AmountTo = 0.01m
            },
            new PairModel()
            {
                PairName = "DASHUSDT",
                AmountFrom = 0.001m,
                AmountTo = 0.01m
            },
            new PairModel()
            {
                PairName = "ETHBTC",
                AmountFrom = 0.00001m,
                AmountTo = 0.00024m
            },
            new PairModel()
            {
                PairName = "LTCBTC",
                AmountFrom = 0.00001m,
                AmountTo = 0.00024m
            },
            new PairModel()
            {
                PairName = "DASHBTC",
                AmountFrom = 0.00001m,
                AmountTo = 0.0001m
            },
            new PairModel()
            {
                PairName = "BCHUSDT",
                AmountFrom = 0.0001m,
                AmountTo = 0.001m
            },
            new PairModel()
            {
                PairName = "BCHBTC",
                AmountFrom = 0.0001m,
                AmountTo = 0.001m
            },
            new PairModel() {
                PairName = "DOGEBTC",
                AmountFrom = 0.1m,
                AmountTo = 16m
            }
        };

        public class OrderModel
        {
            public string Price { get; set; }
            public string Amount { get; set; }
            public bool IsBuy { get; set; }
            public string BotAuthCode { get; set; }
            public string Pair { get; set; }
        }

        static void Main(string[] args)
        {

            BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions()
            {
                ApiCredentials = new ApiCredentials(
                    "3WVntMaeHayWsLCrXh4pnzvM3xnqjiO89hSrquoJfP0MTfI4bDHRnKDfNXhPKAS2",
                    "tIcwcg1AI3If1NQkyMGltYQtAALKhh1mieEzgCwPw2UnurP5821tdCWtQw2Wmgsc")
            });

            Random rnd;

            rnd = new Random();

            var socketClient = new BinanceSocketClient();

            _ = socketClient.SpotStreams.SubscribeToTradeUpdatesAsync("", data =>
            {
                //Заполнение модели ордера, для добавление в список новых ордеров
                var order = new OrderModel()
                {
                    Price = data.Data.Price.ToString()
                    //Amount = rnd.NextDecimal(pair.AmountFrom, pair.AmountTo)
                    //    .ToString(CultureInfo.InvariantCulture),
                    //IsBuy = !data.Data.BuyerIsMaker,
                    //Pair = pair.PairName,
                    //BotAuthCode = BotAuthCodeConstant.Binance
                };

                //if (newOrders.ContainsKey(pair.PairName))
                //{
                //    newOrders[pair.PairName] = order;
                //}
                //else
                //{
                //    newOrders.Add(pair.PairName, order);
                //}




                Console.WriteLine("Hello, World!");
            });

        }



    }
}
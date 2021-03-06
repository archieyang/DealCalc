﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DealCalc
{
    public class TransactionData
    {
        public readonly DateTime DateTime;
        public readonly double InitialPrice;
        public readonly double FinalPrice;
        public readonly double HighestPrice;
        public readonly double LowestPrice;
        public readonly double TotalDeal;
        public readonly double TotalAmout;
        public readonly double EffectiveDeal;

        private TransactionData(DateTime dateTime, double highestPrice, double lowestPrice, double initialPrice, double finalPrice, double totalDeal, double totalAmount)
        {
            DateTime = dateTime;

            HighestPrice = highestPrice;
            LowestPrice = lowestPrice;
            InitialPrice = initialPrice;
            FinalPrice = finalPrice;

            TotalDeal = totalDeal;
            TotalAmout = totalAmount;
            if (finalPrice == initialPrice || highestPrice == lowestPrice)
            {
                EffectiveDeal = 0;
            } else
            {
                EffectiveDeal = (finalPrice - initialPrice) / (highestPrice - lowestPrice) * totalDeal;
            }
        }

        public override string ToString()
        {
            return DateTime + " -> " + InitialPrice + " -- " + FinalPrice + " -- " + HighestPrice + " -- " + LowestPrice + " --" + TotalDeal + " -- " + TotalAmout;
        }

        public static TransactionData CreateFromString(String rawData, Dictionary<String, int> indexDictionary, ErrorHandlerDelegate errorHandler)
        {
            try
            {
                string[] dataItem = rawData.Split(null);
                if (dataItem.Length != 8)
                {
                    return null;
                }

                string date = dataItem[indexDictionary["日期"]];
                string time = dataItem[indexDictionary["时间"]];

                var builder = new StringBuilder();
                builder.Append(date).Append(" ");
                var counter = 0;
                foreach (var c in time)
                {
                    builder.Append(c);
                    ++counter;
                    if (counter == 2)
                    {
                        builder.Append(':');
                    }
                }

                var dateTime = Convert.ToDateTime(builder.ToString());

                var highestPrice = Convert.ToDouble(dataItem[indexDictionary["最高"]]);
                var lowestPrice = Convert.ToDouble(dataItem[indexDictionary["最低"]]);
                var initialPrice = Convert.ToDouble(dataItem[indexDictionary["开盘"]]);
                var finalPrice = Convert.ToDouble(dataItem[indexDictionary["收盘"]]);

                var totalDeal = Convert.ToDouble(dataItem[indexDictionary["成交量"]]);
                var totalAmout = Convert.ToDouble(dataItem[indexDictionary["成交额"]]);
                return new TransactionData(dateTime: dateTime, highestPrice: highestPrice, lowestPrice: lowestPrice, initialPrice: initialPrice, finalPrice: finalPrice, totalDeal: totalDeal, totalAmount: totalAmout);
            }
            catch (Exception e)
            {
                errorHandler?.Invoke("导入表单失败：" + e.Message);
                return null;               
            }
        }
    }
}

using HairScheduler.Communication.Enums;
using HairScheduler.Domain.GeneratorPdf;

namespace HairScheduler.Domain.ExtesionsTypeAndCategory;

public static class CategoryAndType
{       


    public static string CategoryStringToString(this HaircutCategory haircutCategory)
    {
       

        return haircutCategory switch
        {
            HaircutCategory.Kid => ResourceGeneratorPdfMessages.KID,
            HaircutCategory.Adult => ResourceGeneratorPdfMessages.ADULT,
            _ => string.Empty,
        };
    }


    public static string paymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourceGeneratorPdfMessages.CASH,
            PaymentType.CreditCard => ResourceGeneratorPdfMessages.CREDIT_CARD,
            PaymentType.DebitCard => ResourceGeneratorPdfMessages.DEBIT_CARD,
            PaymentType.EletronicTransfer => ResourceGeneratorPdfMessages.ELETRONIC_TRANSFER,
            _ => string.Empty,

        };
    }
}
